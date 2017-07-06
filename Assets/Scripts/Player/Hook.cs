using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "Player")
			return;
		FindObjectOfType<PlayerController> ().SetHook ();
	}
}
