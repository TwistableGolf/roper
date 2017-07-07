﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour {
	public int coinCount;
	public bool adsDisabled = false;
	public int totalCoins;
	public int score;
	public int highScore;
	PlayScreen playScreen;
	public Text coinCounter,pointCounter,bonusText;
	private PlayerController player;
	private PowerupController powerups;
	public float volumeLevel = 1f;
	void Start(){
		playScreen = FindObjectOfType<PlayScreen> ();
		player = FindObjectOfType<PlayerController> ();
		powerups = FindObjectOfType<PowerupController> ();
		string loadString = null;
		loadString = PlayerPrefs.GetString ("SaveString");
		if (loadString == null || loadString == "") {
			print ("No existing save");
			SaveStats ();
		} else {
			
			string[] saveArray = loadString.Split (',');
			if (saveArray.Length != 7) {
				print ("Save in incorrect format");
				SaveStats ();
				return;
			}
			print (loadString);
			totalCoins = System.Int32.Parse (saveArray [0]);
			highScore = System.Int32.Parse (saveArray [1]);
			adsDisabled = bool.Parse (saveArray [2]);
			powerups.magnetAmount = System.Int32.Parse (saveArray [3]);
			powerups.magnetUpgradeLevel = System.Int32.Parse (saveArray [4]);
			powerups.ghostAmount = System.Int32.Parse (saveArray [5]);
			powerups.ghostUpgradeLevel = System.Int32.Parse (saveArray [6]);
			SaveStats ();
		}
	}

	public void SaveStats(){
		string saveString = 
			totalCoins + "," +
			highScore + "," +
			adsDisabled.ToString () + "," +
			powerups.magnetAmount + "," +
			powerups.magnetUpgradeLevel + "," +
			powerups.ghostAmount + "," +
			powerups.ghostUpgradeLevel;
		
		PlayerPrefs.SetString ("SaveString", saveString);
		PlayerPrefs.Save ();
	}

	void Update(){
		if (playScreen.isPlaying)
			AudioListener.volume = volumeLevel;
		else
			AudioListener.volume = 0f;
		coinCounter.text = "Coins  " + (totalCoins + coinCount);
		pointCounter.text = score + "  M";
		if (player.scoreBonus)
			bonusText.enabled = true;
		else
			bonusText.enabled = false;
	}
}
