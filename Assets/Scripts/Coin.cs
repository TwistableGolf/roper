using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	bool destroyOnCoin = true;
	void Start(){
		Invoke ("Flip", 1f);
	}

	void Flip(){
		destroyOnCoin = false;
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
