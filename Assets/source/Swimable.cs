using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimable : MonoBehaviour {

	[SerializeField] protected List<Element> m_Element;

	void OnTriggerEnter2D(Collider2D other) {
		setFly (other.gameObject, true);
	}
	void OnTriggerExit2D(Collider2D other) {
		setFly (other.gameObject, false);
	}

	private void setFly(GameObject go, bool b) {
		var elementTag = go.GetComponent<PlayerElementTag> ();
		if (elementTag == null || !m_Element.Contains(elementTag.getElement()))
			return;

		var movement = go.GetComponent<PlayerMovement> ();
		movement.SetFly (b);

		var body = go.GetComponent<Rigidbody2D> ();
		if (body != null) {
			body.gravityScale = b ? 0.2f : 1f;
		}
	}

}
