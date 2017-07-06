using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomness : MonoBehaviour {
	public GameObject[] coinFormations;
	void Start () {
		if (name == "Tile 0")
			return;
		GameObject[] spikes = FindObjectOfType<LevelGenerator> ().spikes;
		int rand = Random.Range (0, 4);
		GameObject cur = null;
		float random = 0;

		switch (rand) {

		case 0:
			//no spikes
			break;

		case 1:
			//top spike
			cur = (GameObject)Instantiate (spikes[Random.Range(0,spikes.Length)], new Vector3 (Random.Range ((float)transform.position.x - 5, (float)transform.position.x + 5), 9.5f,-1), Quaternion.Euler (180, 0, 0), transform);
			random = Random.Range(6f,7.5f);
			cur.transform.localScale = new Vector3 (random, random, 1);
			break;

		case 2:
			//bottom spike
			cur = (GameObject)Instantiate (spikes[Random.Range(0,spikes.Length)], new Vector3 (Random.Range ((float)transform.position.x - 5, (float)transform.position.x + 5), -9.5f,-1), Quaternion.identity, transform);
			random = Random.Range(6f,7.5f);
			cur.transform.localScale = new Vector3 (random, random, 1);
			break;

		case 3:
			//both spikes
			cur = (GameObject)Instantiate (spikes[Random.Range(0,spikes.Length)], new Vector3 (Random.Range ((float)transform.position.x - 5, (float)transform.position.x + 5), -9.5f,-1), Quaternion.identity, transform);
			random = Random.Range(6f,7.5f);
			cur.transform.localScale = new Vector3 (random, random, 1);

			cur = (GameObject)Instantiate (spikes[Random.Range(0,spikes.Length)], new Vector3 (Random.Range ((float)transform.position.x - 5, (float)transform.position.x + 5), 9.5f,-1), Quaternion.Euler (180, 0, 0), transform);
			random = Random.Range(6f,7.5f);
			cur.transform.localScale = new Vector3 (random, random, 1);
			break;

		default:
		break;
		}
		GameObject coin = FindObjectOfType<LevelGenerator> ().coin;
		rand = Random.Range (0, 3);
		for (int i = 0; i < rand; i++) {
			Instantiate (coinFormations[Random.Range(0,coinFormations.Length-1)], new Vector3 (Random.Range ((float)transform.position.x - 15, (float)transform.position.x + 15), Random.Range ((float)-8, (float)8),-1), Quaternion.identity,transform);
		}
		GameObject bonus = FindObjectOfType<LevelGenerator> ().bonusPrefab;
		rand = Random.Range (0, 11);
		if (rand == 0) {
			Instantiate (bonus, new Vector3 (Random.Range ((float)transform.position.x - 15, (float)transform.position.x + 15), Random.Range ((float)-8, (float)8),-1), Quaternion.identity,transform);
		}
	}
}
