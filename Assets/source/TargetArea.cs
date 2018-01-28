using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour {

	[SerializeField] private string m_Player1Tag = "Player1";
	[SerializeField] private string m_Player2Tag = "Player2";
	[SerializeField] private bool m_BothPlayersRequired = false;

	private bool player1InArea = false;
	private bool player2InArea = false;

	public bool isActivated() {
		if (m_BothPlayersRequired)
			return player1InArea && player2InArea;
		else
			return player1InArea || player2InArea;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == m_Player1Tag) {
			player1InArea = true;
		}
		if (collider.gameObject.tag == m_Player2Tag) {
			player2InArea = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.tag == m_Player1Tag) {
			player1InArea = false;
		}
		if (collider.gameObject.tag == m_Player2Tag) {
			player2InArea = false;
		}
	}

}
