using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

	public static void replacePlayer(GameObject go, GameObject prefab) {
		GameObject newGo = (GameObject) GameObject.Instantiate(prefab, go.transform.position, go.transform.rotation);
		newGo.GetComponent<PlayerMovementController> ().CopyState (go.GetComponent<PlayerMovementController>());
		newGo.GetComponent<PlayerMovement> ().CopyState (go.GetComponent<PlayerMovement>());
		go.SetActive (false);
		GameObject.Destroy (go);
	}

}
