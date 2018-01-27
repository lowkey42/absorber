using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementTag : MonoBehaviour {

	[SerializeField] protected Element m_Element;
	[SerializeField] protected GameObject m_NonePrefab;
	private float m_ToNoneInSeconds = -1;

	public Element getElement() {
		return m_Element;
	}

	public void RevertToNoneIn(float seconds) {
		m_ToNoneInSeconds = seconds;
	}
	public void CancleNoneRevert() {
		m_ToNoneInSeconds = -1;
	}

	void Update() {
		if (m_ToNoneInSeconds > 0) {
			m_ToNoneInSeconds -= Time.deltaTime;

			if (m_ToNoneInSeconds <= 0) {
				Util.replacePlayer (gameObject, m_NonePrefab);
			}
		}
	}


}
