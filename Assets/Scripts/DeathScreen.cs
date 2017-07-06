using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathScreen : MonoBehaviour {
	public Text currentDistance, highDistance, coinCounter;
	public GameObject newHighscoreText;
	public GameObject deathScreen;
	public void DeathScreenPopup(int distance, int coinsGained, int highScore){
		currentDistance.text = distance.ToString ()+"  M";
		highDistance.text = highScore.ToString ()+"  M";
		coinCounter.text = coinsGained.ToString ();
		if (distance == highScore)
			newHighscoreText.SetActive (true);
		else
			newHighscoreText.SetActive (false);
		deathScreen.SetActive (true);
		FindObjectOfType<PlayerController> ().canShoot = false;
	}
	public void DeathScreenOff(){
		deathScreen.SetActive (false);
		FindObjectOfType<PlayerController> ().canShoot = true;
		FindObjectOfType<PlayerController> ().dead = false;
		FindObjectOfType<PlayerController> ().Reset ();
	}

}
