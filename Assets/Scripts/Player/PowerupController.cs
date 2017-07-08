using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerupController : MonoBehaviour {

	public GameObject powerupTimers;
	public GameObject baseTimer;

	public GameObject magnetPowerupPrefab, ghostPowerupPrefab;
	public GameObject magnetShopPrefab, ghostShopPrefab;

	//[HideInInspector]
	public int magnetAmount, ghostAmount, reviveAmount;
	//[HideInInspector]
	public int magnetUpgradeLevel = 1, ghostUpgradeLevel = 1;

	private GameObject currentMagnetPowerup, currentGhostPowerup;

	public bool canUseMagnet = true, canUseGhost = true;
	private float magnetTimer = 0.0f, ghostTimer = 0.0f;

	public Coroutine currentMagnetCoroutine, currentGhostCoroutine;

	private GameObject currentMagnetTimer, currentGhostTimer;

	private Image powerupImage_magnet, powerupImage_ghost;
	private Text powerupCounter_magnet, powerupCounter_ghost;
	public Text powerupCounter_revive;

	private bool setUp;

	public GameObject reviveButton;
	private PlayerController player;
	void Start(){
		player = FindObjectOfType<PlayerController> ();
		setUp = false;
	}

	void Update () {
		if (powerupImage_magnet != null) {
			if (canUseMagnet && magnetAmount > 0)
				powerupImage_magnet.color = new Color (1, 1, 1, 1);
			else
				powerupImage_magnet.color = new Color (1, 1, 1, 0.5f);
		}

		if (powerupCounter_magnet != null) {
			powerupCounter_magnet.text = magnetAmount.ToString();
		}
		if (powerupImage_ghost != null) {
			if (canUseGhost && ghostAmount > 0)
				powerupImage_ghost.color = new Color (1, 1, 1, 1);
			else
				powerupImage_ghost.color = new Color (1, 1, 1, 0.5f);
		}

		if (powerupCounter_ghost != null) {
			powerupCounter_ghost.text = ghostAmount.ToString();
		}

		powerupCounter_revive.text = reviveAmount.ToString ();

		if (reviveAmount > 0 && !player.hasRevived) {
			reviveButton.SetActive (true);
		} else {
			reviveButton.SetActive (false);
		}

	}

	public void SetupPowerupBar(){
		if (setUp)
			return;
		setUp = true;
		#region Magnet
		GameObject cur = null;
		cur = (GameObject)Instantiate (magnetPowerupPrefab, transform);
		cur.transform.localScale = Vector3.one;
		currentMagnetPowerup = cur;
		powerupImage_magnet = currentMagnetPowerup.GetComponent<Image> ();
		powerupCounter_magnet = currentMagnetPowerup.GetComponentInChildren<Text> ();
		cur.GetComponent<Button> ().onClick.AddListener (() => {
			if(canUseMagnet && currentMagnetCoroutine == null && magnetAmount > 0)
				currentMagnetCoroutine = (Coroutine)StartCoroutine(MagnetPowerup());
		});
		EventTrigger trigger = cur.GetComponent<EventTrigger> ();
		EventTrigger.Entry entry1 = new EventTrigger.Entry ();
		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry1.eventID = EventTriggerType.PointerEnter;
		entry1.callback.AddListener ((eventdata) => {
			DisablePlayerHook();
		});
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener ((eventdata) => {
			EnablePlayerHook();
		});
		trigger.triggers.Add (entry1);
		trigger.triggers.Add (entry2);
		#endregion
		#region Ghost
		cur = null;
		cur = (GameObject)Instantiate (ghostPowerupPrefab, transform);
		cur.transform.localScale = Vector3.one;
		currentGhostPowerup = cur;
		powerupImage_ghost = currentGhostPowerup.GetComponent<Image> ();
		powerupCounter_ghost = currentGhostPowerup.GetComponentInChildren<Text> ();
		cur.GetComponent<Button> ().onClick.AddListener (() => {
			if(canUseGhost && currentGhostCoroutine == null && ghostAmount > 0)
				currentGhostCoroutine = (Coroutine)StartCoroutine(EnableGhost());
		});
		trigger = cur.GetComponent<EventTrigger> ();
		entry1 = new EventTrigger.Entry ();
		entry2 = new EventTrigger.Entry ();
		entry1.eventID = EventTriggerType.PointerEnter;
		entry1.callback.AddListener ((eventdata) => {
			DisablePlayerHook();
		});
		entry2.eventID = EventTriggerType.PointerExit;
		entry2.callback.AddListener ((eventdata) => {
			EnablePlayerHook();
		});
		trigger.triggers.Add (entry1);
		trigger.triggers.Add (entry2);
		#endregion
	}

	IEnumerator MagnetPowerup(){
		if (FindObjectOfType<PlayerController> ().dead)
			yield break;
		magnetAmount--;
		canUseMagnet = false;
		FindObjectOfType<PlayerController> ().coinMagnetEnabled = true;
		currentMagnetPowerup.GetComponent<Image> ().color = new Color (1,1,1,0.5f);
		float timeToReach = ((magnetUpgradeLevel + 1) * 5);
		currentMagnetTimer = (GameObject)Instantiate (baseTimer, powerupTimers.transform.position,Quaternion.identity,powerupTimers.transform);
		currentMagnetTimer.GetComponentInChildren<Image> ().sprite = magnetPowerupPrefab.GetComponentInChildren<Image> ().sprite;
		Text timerText = currentMagnetTimer.GetComponent<Text> ();
		while (magnetTimer < timeToReach) {
			magnetTimer += Time.deltaTime;
			float displayTime = timeToReach - magnetTimer;
			timerText.text = displayTime.ToString("0.0");
			yield return new WaitForEndOfFrame ();
		}
		FindObjectOfType<PlayerController> ().coinMagnetEnabled = false;
		Destroy (currentMagnetTimer);
	}

	public void DisableMagnet(bool onDeath){
		if (currentMagnetCoroutine != null) {
			StopCoroutine (currentMagnetCoroutine);
			currentMagnetCoroutine = null;
		}
		FindObjectOfType<PlayerController> ().coinMagnetEnabled = false;
		Destroy (currentMagnetTimer);
		magnetTimer = 0.0f;
		if (onDeath) {
			canUseMagnet = true;
			currentMagnetPowerup.GetComponent<Image> ().color = new Color (1,1,1,1f);
		}
	}

	IEnumerator EnableGhost(){
		if (FindObjectOfType<PlayerController> ().dead)
			yield break;
		ghostAmount--;
		canUseGhost = false;
		FindObjectOfType<PlayerController> ().ghostEnabled = true;
		currentGhostPowerup.GetComponent<Image> ().color = new Color (1,1,1,0.5f);
		float timeToReach = ((ghostUpgradeLevel + 1) * 5);
		currentGhostTimer = (GameObject)Instantiate (baseTimer, powerupTimers.transform.position,Quaternion.identity,powerupTimers.transform);
		currentGhostTimer.GetComponentInChildren<Image> ().sprite = ghostPowerupPrefab.GetComponentInChildren<Image> ().sprite;
		Text timerText = currentGhostTimer.GetComponent<Text> ();
		while (ghostTimer < timeToReach) {
			ghostTimer += Time.deltaTime;
			float displayTime = timeToReach - ghostTimer;
			timerText.text = displayTime.ToString("0.0");
			yield return new WaitForEndOfFrame ();
		}
		FindObjectOfType<PlayerController> ().ghostEnabled = false;
		Destroy (currentGhostTimer);
	}

	public void DisableGhost (bool onDeath){
		if (currentGhostCoroutine != null) {
			StopCoroutine (currentGhostCoroutine);
			currentGhostCoroutine = null;
		}
		FindObjectOfType<PlayerController> ().ghostEnabled = false;
		Destroy (currentGhostTimer);
		ghostTimer = 0.0f;

		if (onDeath) {
			canUseGhost = true;
			currentGhostPowerup.GetComponent<Image> ().color = new Color (1,1,1,1f);
		}
	}

	public void Revive (){
		reviveAmount--;
		FindObjectOfType<PlayerController> ().Revive();
	}

	public void DisablePlayerHook(){
		if(FindObjectOfType<PlayScreen>().isPlaying)
			FindObjectOfType<PlayerController> ().canShoot = false;

	}

	public void EnablePlayerHook(){
		if(FindObjectOfType<PlayScreen>().isPlaying)
			FindObjectOfType<PlayerController> ().canShoot = true;

	}

}
