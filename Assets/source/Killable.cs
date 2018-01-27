﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

	[SerializeField] private string m_Player1Tag = "Player1";
	[SerializeField] private string m_Player2Tag = "Player2";


	public void kill() {
		foreach (GameObject go in GameObject.FindGameObjectsWithTag(m_Player1Tag)) {
			killPlayer (go);
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag(m_Player2Tag)) {
			killPlayer (go);
		}
	}

	private void killPlayer(GameObject go) {
		if (!go.activeSelf)
			return;

		// TODO: play animation

		foreach (SpawnPoint spawnPoint in GameObject.FindObjectsOfType<SpawnPoint>()) {
			if (spawnPoint.isActive () && spawnPoint.getTag()==go.tag) {
				spawnPoint.Spawn ();
				go.SetActive (false);
				Destroy (go);
				return;
			}
		}

		foreach (SpawnPoint spawnPoint in GameObject.FindObjectsOfType<SpawnPoint>()) {
			if (spawnPoint.isInitial() && spawnPoint.getTag()==go.tag) {
				spawnPoint.Spawn ();
				go.SetActive (false);
				Destroy (go);
				return;
			}
		}
	}

}
