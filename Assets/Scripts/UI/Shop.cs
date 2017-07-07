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
	public Text ghostAmount,ghostLevel;
	public Text ghostBuyPrice,ghostUpgradePrice;
	PowerupController powerups;
	public PlayerStats playerStats;
	void Start(){
		powerups = FindObjectOfType<PowerupController>();
	}

	void Update () {
		if (isOpen) {
			coinCounter.text = playerStats.totalCoins.ToString();
			magnetAmount.text = "Owned \n"+powerups.magnetAmount;
			magnetLevel.text = "Current Level \n" + powerups.magnetUpgradeLevel;
			magnetBuyPrice.text = "25";
			magnetUpgradePrice.text = ((int)Mathf.Pow (powerups.magnetUpgradeLevel, 3) + 50).ToString();

			ghostAmount.text = "Owned \n"+powerups.ghostAmount;
			ghostLevel.text = "Current Level \n" + powerups.ghostUpgradeLevel;
			ghostBuyPrice.text = "25";
			ghostUpgradePrice.text = ((int)Mathf.Pow (powerups.ghostUpgradeLevel, 3) + 50).ToString();
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
			powerups.ghostAmount++;
			playerStats.totalCoins -= 25;
		}
	}

	public void UpgradeMagnet(){
		int price = (int)Mathf.Pow (powerups.magnetUpgradeLevel, 3) + 50;
		if (playerStats.totalCoins >= price) {
			powerups.magnetUpgradeLevel++;
			playerStats.totalCoins -= price;
		}
	}

	public void UpgradeGhost(){
		int price = (int)Mathf.Pow (powerups.ghostUpgradeLevel, 3) + 50;
		if (playerStats.totalCoins >= price) {
			powerups.ghostUpgradeLevel++;
			playerStats.totalCoins -= price;
		}
	}

	public void Toggle(){
		isOpen = !isOpen;
		mainWindow.SetActive (isOpen);
		playerStats.SaveStats ();
	}

}
