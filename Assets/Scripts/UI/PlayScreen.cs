using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class PlayScreen : MonoBehaviour {
	public GameObject thisScreen, gameUI;
	public bool isPlaying = false;
	void Start(){

		//Probably doesn't do anything

		FindObjectsOfType<Text> ().ToList ().ForEach (x => {
			x.font.material.mainTexture.filterMode = FilterMode.Point;
		});

	}

	public void Play(){
		gameUI.SetActive (true);
		thisScreen.SetActive (false);
		isPlaying = true;
		FindObjectOfType<PlayerController> ().canPlay = true;
		FindObjectOfType<PowerupController> ().SetupPowerupBar ();
		FindObjectOfType<PlayerController> ().music.Play ();
	}

	public void Back(){
		gameUI.SetActive (false);
		thisScreen.SetActive (true);
		isPlaying = false;
		FindObjectOfType<PlayerController> ().canPlay = true;
		//FindObjectOfType<PowerupController> ();
	}

}
