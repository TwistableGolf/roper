using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
	public Slider volumeSlider;
	private PlayerStats playerStats;
	public GameObject mainWindow;
	bool isOpen = false;
	// Use this for initialization
	void Start () {
		playerStats = FindObjectOfType<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerStats.volumeLevel = volumeSlider.value;
	}

	public void Toggle(){
		isOpen = !isOpen;
		mainWindow.SetActive(isOpen);
	}

}
