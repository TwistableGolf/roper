using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	bool destroyOnCoin = true;
	private PlayScreen playScreen;
	private PlayerController player;
	void Start(){
		playScreen = FindObjectOfType<PlayScreen> ();
		player = playScreen.player.GetComponent<PlayerController> ();
		Invoke ("Flip", .1f);
	}

	void Flip(){
		destroyOnCoin = false;
	}

	void Update(){
		if (player.ghostEnabled) {
			if (Vector2.Distance (transform.position, player.transform.position) < 2) {
				if (FindObjectOfType<PlayerController> ().scoreBonus) {

					player.GetComponent<PlayerStats> ().coinCount += 2;
				} else {

					player.GetComponent<PlayerStats> ().coinCount++;
				}
				player.GetComponent<AudioSource> ().Play ();
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.tag != "Player") {
			if (col.tag != "Hook") {
				if (destroyOnCoin) {
					if (col.tag == "Coin") {
						Destroy (this.gameObject);
					}
				} else if (col.tag != "Coin") {
					Destroy (this.gameObject);
				}
					
			}
		} else if(!FindObjectOfType<PlayerController>().dead) {
			if (FindObjectOfType<PlayerController> ().scoreBonus) {
				
				col.gameObject.GetComponent<PlayerStats> ().coinCount += 2;
			} else {

				col.gameObject.GetComponent<PlayerStats> ().coinCount++;
			}
			col.gameObject.GetComponent<AudioSource> ().Play ();
			Destroy (this.gameObject);
		}
	}
}
