using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public GameObject hook;
	public float hookShotSpeed;
	private GameObject currentHook;
	public LineRenderer lineRenderer;
	public DistanceJoint2D playerHookJoint;
	private Rigidbody2D rigid;
	public bool canShoot = true;
	public bool canPlay = false;
	public bool scoreBonus = false;
	PlayerStats stats;
	public int oldX = 0;
	public int roundCounter = 0;
	public int roundsToAd;
	public bool dead;
	public AudioSource music;
	public GameObject hookPosition;
	public SpriteRenderer magnetSprite;
	SpriteRenderer render;
	public Sprite hookingSprite,fallingSprite,deathSprite;
	public bool coinMagnetEnabled = false, ghostEnabled = false;
	public int coinMagnetSize = 5;

	public BoxCollider2D boxCol;
	public PolygonCollider2D polyCol;

	public bool hasRevived = false;
	// Use this for initialization

	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		stats = FindObjectOfType<PlayerStats> ();
		render = GetComponent<SpriteRenderer> ();
		#if UNITY_ADS
		Advertisement.Initialize ("1279490");
		#endif
	}
	
	// Update is called once per frame
	void Update () {

		if (canPlay) {
			//bool touchHeld = false;
			if (Application.isMobilePlatform) {
				if (Input.touches.Length > 0) {
					if (Input.touches [0].phase == TouchPhase.Moved || Input.touches [0].phase == TouchPhase.Stationary) {
						//touchHeld = true;
					}
				}
			}
			if (!dead) {
				if (Application.isMobilePlatform) {
					if (Input.touches.Length > 0) {
						if ((Input.touches [0].phase == TouchPhase.Began) && canShoot) {
							render.sprite = hookingSprite;
						}

						if (Input.touches [0].phase == TouchPhase.Ended) {
							render.sprite = fallingSprite;
						}
					}
				} else {
					if ((Input.GetMouseButtonDown(0)) && canShoot) {
						render.sprite = hookingSprite;
					}

					if (Input.GetMouseButtonUp (0)) {
						render.sprite = fallingSprite;
					}
				}

				/*
				if ((Input.GetMouseButton (0) || touchHeld) && canShoot) {
					render.sprite = hookingSprite;
				} else {
					render.sprite = fallingSprite;
				}
				*/
			} else {
				render.sprite = deathSprite;
			}


			if (currentHook != null) {
			
				lineRenderer.enabled = true;
				Vector3[] positions = new Vector3[2];
				positions [0] = hookPosition.transform.position;
				positions [1] = currentHook.transform.position - (currentHook.transform.up * 0.5f);
				lineRenderer.numPositions = 2;
				lineRenderer.SetPositions (positions);

				transform.up = currentHook.transform.position - transform.position;



			} else {
				lineRenderer.enabled = false;
				if (!dead)
					transform.up = (transform.position + Vector3.up) - transform.position;

			}
			if (rigid.bodyType == RigidbodyType2D.Dynamic) {
				rigid.AddForce (Vector2.right * Time.deltaTime * 50);
			}
			#region windowsControls
			if (!Application.isMobilePlatform) {
				if (Input.GetMouseButtonDown (0) && canShoot && !dead) {
				
					if (currentHook != null) {
						Destroy (currentHook);
						Destroy (playerHookJoint);
					}

					Vector2 dir = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					GameObject cur = (GameObject)Instantiate (hook, transform.position + ((transform.up + transform.right) / 2), Quaternion.identity);
					cur.transform.up = ((Vector3)dir - cur.transform.position);
					Rigidbody2D rigid2d = cur.GetComponent<Rigidbody2D> ();
					Physics2D.IgnoreCollision (cur.GetComponent<BoxCollider2D> (), GetComponent<BoxCollider2D> ());
					rigid2d.AddForce ((dir - (Vector2)cur.transform.position).normalized * (hookShotSpeed * 100));
					currentHook = cur;
					if (rigid.bodyType == RigidbodyType2D.Static) {
						rigid.bodyType = RigidbodyType2D.Dynamic;
					}

				}

				if (Input.GetMouseButtonUp (0)) {
					Destroy (currentHook);
					Destroy (playerHookJoint);
					if (GetComponent<DistanceJoint2D> () != null) {
						Destroy (GetComponent<DistanceJoint2D> ());
					}
				}

			}
			#endregion

			#region touchControls
			if (Application.isMobilePlatform) {
				if (Input.touches.Length > 0) {
					if (Input.touches [0].phase == TouchPhase.Began && canShoot && !dead) {
						if (currentHook != null) {
							Destroy (currentHook);
							Destroy (playerHookJoint);
						}
						Vector2 dir = Camera.main.ScreenToWorldPoint (Input.mousePosition);
						GameObject cur = (GameObject)Instantiate (hook, transform.position + ((transform.up + transform.right) / 2), Quaternion.identity);
						cur.transform.up = ((Vector3)dir - cur.transform.position);
						Rigidbody2D rigid2d = cur.GetComponent<Rigidbody2D> ();
						Physics2D.IgnoreCollision (cur.GetComponent<BoxCollider2D> (), GetComponent<BoxCollider2D> ());
						rigid2d.AddForce ((dir - (Vector2)cur.transform.position).normalized * (hookShotSpeed * 100));
						currentHook = cur;
						if (rigid.bodyType == RigidbodyType2D.Static) {
							rigid.bodyType = RigidbodyType2D.Dynamic;
						}
					}
					if (Input.touches [0].phase == TouchPhase.Ended) {
						Destroy (currentHook);
						Destroy (playerHookJoint);
						if (GetComponent<DistanceJoint2D> () != null) {
							Destroy (GetComponent<DistanceJoint2D> ());
						}
					}
				}
			}
			#endregion
			if (!dead) {
				int dif = Mathf.RoundToInt (transform.position.x - oldX);
				if (dif >= 5) {
					stats.score += 1;
					oldX = Mathf.RoundToInt (transform.position.x);
				}
			}
		}
	}

	void FixedUpdate(){
		if (!coinMagnetEnabled) {
			magnetSprite.enabled = false;
		}
		if (!ghostEnabled) {
			render.color = new Color (1, 1, 1, 1f);
		}
		if (!dead && canPlay) {
			if (coinMagnetEnabled) {
				magnetSprite.enabled = true;
				magnetSprite.size = new Vector2 (coinMagnetSize * 1.7f, coinMagnetSize * 1.7f);
				Collider2D[] cols = Physics2D.OverlapCircleAll (this.transform.position,5f);
				foreach (var item in cols) {
					if (item.tag == "Coin") {
						item.transform.position = Vector2.MoveTowards (item.transform.position, this.transform.position,0.1f);
					}
				}
			}
			if (ghostEnabled) {
				render.color = new Color (1, 1, 1, 0.5f);
				boxCol.enabled = false;
				if (transform.position.y > 7.7f || transform.position.y < -7.7f) {
					Die ();
				}
			} else {
				render.color = new Color (1, 1, 1, 1);
				boxCol.enabled = true;

			}
		}
	}

	public void SetHook(){
		
		playerHookJoint = gameObject.AddComponent<DistanceJoint2D> ();
		playerHookJoint.connectedAnchor = currentHook.transform.position;
		playerHookJoint.enableCollision = true;
		playerHookJoint.maxDistanceOnly = true;
		playerHookJoint.anchor = new Vector2 (.5f, .5f);
		GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		currentHook.GetComponent<Rigidbody2D> ().bodyType = RigidbodyType2D.Static;
	}

	public void Die(){
		Destroy (currentHook);
		Destroy (playerHookJoint);
		if(GetComponent<DistanceJoint2D>() != null){
			Destroy(GetComponent<DistanceJoint2D>());
		}
		rigid.constraints = RigidbodyConstraints2D.None;
		if(rigid.bodyType != RigidbodyType2D.Dynamic)
			rigid.bodyType = RigidbodyType2D.Dynamic;
		GetComponent<BoxCollider2D> ().enabled = false;
		GetComponent<PolygonCollider2D> ().enabled = true;
		if (stats.highScore < stats.score) {
			stats.highScore = stats.score;
		}
		FindObjectOfType<DeathScreen>().DeathScreenPopup(stats.score,stats.coinCount,stats.highScore);
		stats.totalCoins += stats.coinCount;


		dead = true;
		scoreBonus = false;

		stats.SaveStats ();
		roundCounter++;
		if (roundCounter == roundsToAd) {
			roundCounter = 0;
			#if UNITY_ADS
			if(Application.isMobilePlatform)
			if (Advertisement.IsReady ()) {
				Advertisement.Show ();
			}
			#endif
		}
	}
	public void Reset(){
		stats.coinCount = 0;
		oldX = 0;

		stats.score = 0;
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		rigid.bodyType = RigidbodyType2D.Static;
		FindObjectOfType<PowerupController> ().DisableMagnet (true);
		FindObjectOfType<PowerupController> ().DisableGhost (true);
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<PolygonCollider2D> ().enabled = false;
		hasRevived = false;
	}

	public void Revive(){
		transform.position = new Vector3 (transform.position.x, 0);
		transform.rotation = Quaternion.identity;
		rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
		rigid.bodyType = RigidbodyType2D.Static;
		FindObjectOfType<PowerupController> ().DisableMagnet (true);
		FindObjectOfType<PowerupController> ().DisableGhost (true);
		GetComponent<BoxCollider2D> ().enabled = true;
		GetComponent<PolygonCollider2D> ().enabled = false;
		FindObjectOfType<DeathScreen> ().deathScreen.SetActive (false);
		hasRevived = true;
		dead = false;
		canShoot = true;
	}

	public void Toggle(){
		canShoot = !canShoot;
	}
	void OnCollisionEnter2D(Collision2D col){
		if(!dead)
			Die ();
	}
}
