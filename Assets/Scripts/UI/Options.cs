using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
	public Slider volumeSlider;
	public PlayerStats playerStats;
	public GameObject mainWindow;
	bool isOpen = false;

	void Update () {
		if(playerStats.isActiveAndEnabled)
			playerStats.volumeLevel = volumeSlider.value;
	}

	public void Toggle(){
		isOpen = !isOpen;
		mainWindow.SetActive(isOpen);
	}

}
