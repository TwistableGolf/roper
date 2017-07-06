using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
	public Transform player;
	public float dampTime = 0.15f;
	void LateUpdate(){
		transform.position = new Vector3 (player.position.x+5, 0, -10);
	}

}
