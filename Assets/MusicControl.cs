using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {
	public GameObject cross;
	public bool musicEnabled = true;
	public AudioSource music;
	public PlayerStats stats;

	void Start(){
		string loadString = null;
		loadString = PlayerPrefs.GetString ("SaveString");
		if (loadString == null || loadString == "") {
			print ("No existing save");
		} else {

			string[] saveArray = loadString.Split (',');
			if (saveArray.Length != 8) {
				print ("Save in incorrect format");
				return;
			}
			print (loadString);
			musicEnabled = bool.Parse(saveArray [7]);
		}
	}

	void Update () {
		if (musicEnabled) {
			cross.SetActive (false);
			stats.soundEnabled = true;
		} else {
			cross.SetActive (true);
			stats.soundEnabled = false;
		}
		if (musicEnabled) {
			AudioListener.volume = 1f;
		} else {
			AudioListener.volume = 0f;
		}
	}

	public void Toggle(){
		musicEnabled = !musicEnabled;
	}
}
