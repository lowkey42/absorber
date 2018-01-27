using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbtionTrigger : MonoBehaviour {

	[SerializeField] private string m_Player1Tag = "Player1";
	[SerializeField] private string m_Player2Tag = "Player2";
	[SerializeField] protected List<Element> m_SourceElement;
	[SerializeField] protected Element m_TargetElement;
	[SerializeField] protected GameObject[] m_NewPrefab;

	void OnTriggerEnter2D(Collider2D other) {
		handle (other.gameObject, m_NewPrefab, m_TargetElement);
	}
	void OnTriggerExit2D(Collider2D other) {
		revert(other.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		handle(collision.gameObject, m_NewPrefab, m_TargetElement);
	}
	void OnCollisionExit2D(Collision2D collision) {
		revert(collision.gameObject);
	}

	private void revert(GameObject go) {
		if (go.tag == m_Player1Tag || go.tag == m_Player2Tag) {
			var changeFirstPlayer = go.tag == m_Player2Tag;

			var playerGos = GameObject.FindGameObjectsWithTag (changeFirstPlayer ? m_Player1Tag : m_Player2Tag);

			foreach (GameObject otherPlayer in playerGos) {
				var elementTag = otherPlayer.GetComponent<PlayerElementTag> ();
				if (elementTag != null) {
					elementTag.RevertToNoneIn (0.5f);
				}
			}
		}
	}

	private void handle(GameObject go, GameObject[] prefabs, Element element) {
		if (go.tag == m_Player1Tag || go.tag == m_Player2Tag) {
			var changeFirstPlayer = go.tag == m_Player2Tag;

			var playerGos = GameObject.FindGameObjectsWithTag (changeFirstPlayer ? m_Player1Tag : m_Player2Tag);
			var newPrefab = prefabs[changeFirstPlayer ? 0 : 1];

			foreach (GameObject otherPlayer in playerGos) {
				changePlayer (otherPlayer, newPrefab, element);
			}
		}
	}

	private void changePlayer(GameObject go, GameObject prefab, Element element) {
		var elementTag = go.GetComponent<PlayerElementTag> ();
		if (elementTag == null)
			return;

		elementTag.CancleNoneRevert();

		if (go.activeSelf && elementTag != null && elementTag.getElement () != element && (element==Element.none || m_SourceElement.Contains(elementTag.getElement()))) {
			Util.replacePlayer (go, prefab);
		}
	}

}
