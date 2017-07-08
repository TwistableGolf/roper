using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : MonoBehaviour {
	public float countDownTime = 10f;
	private PlayScreen playScreen;
	private PlayerController playerController;
	void Start(){
		playScreen = FindObjectOfType<PlayScreen> ();
		playerController = playScreen.player.GetComponent<PlayerController> ();
	}

	void Update(){
		if (playerController.ghostEnabled) {
			if (Vector2.Distance (transform.position, playerController.transform.position) < 2) {
				
				this.transform.parent = null;
				StartCoroutine (ScoreBonusCountdown(playerController.GetComponent<PlayerController>()));
				GetComponent<TextMesh> ().text = "";
				GetComponent<BoxCollider2D> ().enabled = false;

			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag != "Player") {
			if (col.tag != "Hook") {
				Destroy (this.gameObject);
			}
		} else {
			
			this.transform.parent = null;
			StartCoroutine (ScoreBonusCountdown(col.gameObject.GetComponent<PlayerController>()));
			GetComponent<TextMesh> ().text = "";
			GetComponent<BoxCollider2D> ().enabled = false;
		}
	}

	IEnumerator ScoreBonusCountdown(PlayerController player){
		player.scoreBonus = true;
		float timeTaken = 0f;
		while (timeTaken < countDownTime){
			timeTaken += Time.deltaTime;
			if (player.scoreBonus == false) {
				player.scoreBonus = true;
			}
			if (playerController.dead)
				break;
			yield return new WaitForEndOfFrame ();
		}
		player.scoreBonus = false;
		Destroy (this.gameObject);
	}

}
