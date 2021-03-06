﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public GameObject[] spikes;
	public GameObject tile;
	public GameObject coin;
	public GameObject bonusPrefab;
	public int mapScale;
	public int renderSizeBack,renderSizeFront;
	public Transform player;
	public List<GameObject> tiles;
	// Use this for initialization
	void Start () {
		tiles = new List<GameObject> ();
	}

	void Update(){
		GenerateTiles ();
	}

	public void GenerateTiles(){
		float curX = 0;

		if (player.gameObject.activeSelf) {
			curX = (player.position.x / mapScale);	
		} else {
			curX = (Camera.main.transform.position.x / mapScale);
		}

		int minX = Mathf.FloorToInt (curX - renderSizeBack);
		int maxX = Mathf.CeilToInt (curX + renderSizeFront);
		List<GameObject> used = new List<GameObject> ();
		for (int i = minX; i < maxX; i++) {
			string index = "Tile " + i;
			if (!tiles.Exists (x => x.name == index)) {
				var go = (GameObject)Instantiate (tile, new Vector3 (i * (mapScale), 0, 0), Quaternion.identity, transform);
				go.name = index;
				tiles.Add (go);
				used.Add (go);
			} else {
				used.Add (tiles.Find (x => x.name == index));
			}
		}
		var diff = tiles.Except (used);
		foreach (var item in diff.ToList()) {
			tiles.Remove (item);
			Destroy (item);
		}
	}

	public void Reset(){
		foreach (var item in tiles) {
			Destroy (item);
		}
		tiles = new List<GameObject> ();
		player.GetComponent<PlayerController> ().Die ();

	}
}
