﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

	[SerializeField] private string m_Player1Tag = "Player1";
	[SerializeField] private string m_Player2Tag = "Player2";
	[SerializeField] private bool m_DieOnCollision = false;
	private GameplaySystem m_GameplaySys;
	private Rigidbody2D m_Body;

	private void Awake() {
		m_GameplaySys = GameObject.FindObjectOfType<GameplaySystem> ();
	}
	void Start() {
		m_Body = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.Distance (collision.otherCollider).distance > 1.0f || (m_DieOnCollision && m_Body!=null && m_Body.velocity.magnitude>0.1f)) {
			kill ();
		}
	}

	public void kill() {
		if (m_GameplaySys != null) {
			m_GameplaySys.PlayDeathSoundEffect ();
		}

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
