using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	[SerializeField] private string m_PlayerTag = "Player1";
	[SerializeField] private bool m_Initial = false;
	[SerializeField] protected GameObject m_NonePrefab;
	private bool m_Active = false;

	void Start() {
		if (m_Initial) {
			Spawn();
		}
	}

	public bool isActive() {
		return m_Active;
	}
	public bool isInitial() {
		return m_Initial;
	}
	public string getTag() {
		return m_PlayerTag;
	}

	public void Spawn() {
		Instantiate (m_NonePrefab, transform.position, transform.rotation);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != m_PlayerTag)
			return;

		if (!m_Active) {
			m_Active = true;

			// disable other points
			foreach (SpawnPoint spawnPoint in GameObject.FindObjectsOfType<SpawnPoint>()) {
				if (spawnPoint != this && spawnPoint.m_PlayerTag==m_PlayerTag) {
					m_Active = false;
				}
			}

			// TODO: change sprite/animation
		}
	}

}
