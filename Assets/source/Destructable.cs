using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	[SerializeField] protected Element m_RequiredElement;
	[SerializeField] protected float m_RequiredMinimalVelocity = 0f;

	void OnCollisionEnter2D(Collision2D collision) {
		if (m_RequiredMinimalVelocity > 0 && collision.relativeVelocity.magnitude < m_RequiredMinimalVelocity)
			return;

		var elementTag = collision.gameObject.GetComponent<PlayerElementTag> ();

		if (matchesElement(collision.gameObject)) {
			// TODO: play animation
			Destroy(gameObject);
		}
	}

	private bool matchesElement(GameObject go) {
		var elementTag = go.GetComponent<PlayerElementTag> ();
		return elementTag != null && elementTag.getElement () == m_RequiredElement;
	}

}
