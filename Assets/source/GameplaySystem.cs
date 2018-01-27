using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour {

	private bool gameWon = false;

	void LateUpdate () {
		if (gameWon)
			return;


		var won = true;
		foreach (TargetArea area in GameObject.FindObjectsOfType<TargetArea> ()) {
			won = won && area.isActivated();
		}

		if (won) {
			gameWon = true;
			Time.timeScale = 0;
			Debug.Log ("Level done!");
			// TODO: show overlay, play sound, redirect to next level
		}
	}
}
