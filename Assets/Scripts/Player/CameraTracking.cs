using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
	public Transform player;
	public float dampTime = 0.15f;
	void LateUpdate(){
		if (player.gameObject.activeSelf)
			transform.position = new Vector3 (player.position.x + 5, 0, -10);
		else
			transform.position = new Vector3 (transform.position.x+(Time.deltaTime*3), transform.position.y, transform.position.z);
	}

}
