using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killing : MonoBehaviour {

	[SerializeField] protected List<Element> m_Element;

	void OnCollisionEnter2D(Collision2D collision) {
		tryKill (collision.gameObject);
	}
	void OnTriggerEnter2D(Collider2D collider) {
		tryKill (collider.gameObject);
	}

	private void tryKill(GameObject go) {
		var killable = go.GetComponent<Killable> ();
		var elementTag = go.GetComponent<PlayerElementTag> ();

		if (killable != null && elementTag != null && m_Element.Contains (elementTag.getElement ())) {
			killable.kill ();
		}
	}

}
