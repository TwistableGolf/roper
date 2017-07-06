using System.Collections;
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
			SaveStats (false);
		} else {
			
			string[] saveArray = loadString.Split (',');
			if (saveArray.Length != 6) {
				print ("Save in incorrect format");
				SaveStats (false);
				return;
			}
			print (loadString);
			totalCoins = System.Int32.Parse (saveArray [0]);
			highScore = System.Int32.Parse (saveArray [1]);
			adsDisabled = bool.Parse (saveArray [2]);
			powerups.magnetAmount = System.Int32.Parse (saveArray [3]);
			powerups.magnetUpgradeLevel = System.Int32.Parse (saveArray [4]);
			volumeLevel = System.Int32.Parse (saveArray [5]);
			SaveStats (false);
		}
	}

	public void SaveStats(bool repeat = true){
		string saveString = 
			totalCoins + "," +
			highScore + "," +
			adsDisabled.ToString () + "," +
			powerups.magnetAmount + "," +
			powerups.magnetUpgradeLevel + "," +
			volumeLevel;
		
		PlayerPrefs.SetString ("SaveString", saveString);
		PlayerPrefs.Save ();
	}

	private void SaveCaller(){
		SaveStats ();
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
