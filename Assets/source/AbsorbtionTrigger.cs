using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbtionTrigger : MonoBehaviour {

	[SerializeField] private string m_Player1Tag = "Player1";
	[SerializeField] private string m_Player2Tag = "Player2";
	[SerializeField] protected Element m_TargetElement;
	[SerializeField] protected GameObject[] m_NewPrefab;
	[SerializeField] protected GameObject[] m_NonePrefab;

	void OnTriggerEnter2D(Collider2D other) {
		handle (other.gameObject, m_NewPrefab, m_TargetElement);
	}
	void OnTriggerExit2D(Collider2D other) {
		handle (other.gameObject, m_NonePrefab, Element.none);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		handle(collision.gameObject, m_NewPrefab, m_TargetElement);
	}
	void OnCollisionExit2D(Collision2D collision) {
		handle(collision.gameObject, m_NonePrefab, Element.none);
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
		if (elementTag != null && elementTag.getElement () != element) {
			GameObject newGo = (GameObject) Instantiate(prefab, go.transform.position, go.transform.rotation);
			newGo.GetComponent<PlayerMovementController> ().CopyState (go.GetComponent<PlayerMovementController>());
			newGo.GetComponent<PlayerMovement> ().CopyState (go.GetComponent<PlayerMovement>());
			Destroy (go);
		}
	}

}
