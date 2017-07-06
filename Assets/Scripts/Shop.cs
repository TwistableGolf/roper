using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour {
	public GameObject mainWindow;
	public bool isOpen = false;

	public Text coinCounter;
	public Text magnetAmount,magnetLevel;
	public Text magnetBuyPrice,magnetUpgradePrice;
	PowerupController powerups;
	PlayerStats playerStats;
	void Start(){
		powerups = FindObjectOfType<PowerupController>();
		playerStats = FindObjectOfType<PlayerStats> ();
	}

	void Update () {
		if (isOpen) {
			magnetAmount.text = "Owned \n"+powerups.magnetAmount;
			magnetLevel.text = "Current Level \n" + powerups.magnetUpgradeLevel;
			coinCounter.text = playerStats.totalCoins.ToString();
			magnetBuyPrice.text = "25";
			magnetUpgradePrice.text = ((int)Mathf.Pow (powerups.magnetUpgradeLevel, 3)).ToString();
		}
			
	}

	public void BuyMagnet(){
		if (playerStats.totalCoins >= 25) {
			powerups.magnetAmount++;
			playerStats.totalCoins -= 25;
		}
	}

	public void BuyGhost(){
		if (playerStats.totalCoins >= 25) {
			powerups.magnetAmount++;
			playerStats.totalCoins -= 25;
		}
	}

	public void UpgradeMagnet(){
		int price = (int)Mathf.Pow (powerups.magnetUpgradeLevel, 3);
		if (playerStats.totalCoins >= price) {
			powerups.magnetUpgradeLevel++;
			playerStats.totalCoins -= price;
		}
	}

	public void UpgradeGhost(){
		int price = (int)Mathf.Pow (powerups.ghostUpgradeLevel, 3);
		if (playerStats.totalCoins >= price) {
			powerups.ghostUpgradeLevel++;
			playerStats.totalCoins -= price;
		}
	}

	public void Toggle(){
		isOpen = !isOpen;
		mainWindow.SetActive (isOpen);
	}

}
