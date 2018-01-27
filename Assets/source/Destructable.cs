using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	[SerializeField] protected Element m_RequiredElement;
	[SerializeField] protected float m_RequiredMinimalVelocity = 0f;
	[SerializeField] protected GameObject m_Corpse;
	[SerializeField] protected AudioClip m_SoundEffect;
	private GameplaySystem m_GameplaySys;

	private void Awake() {
		m_GameplaySys = GameObject.FindObjectOfType<GameplaySystem> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (m_RequiredMinimalVelocity > 0 && collision.relativeVelocity.magnitude < m_RequiredMinimalVelocity)
			return;

		var elementTag = collision.gameObject.GetComponent<PlayerElementTag> ();

		if (matchesElement(collision.gameObject)) {
			if (m_SoundEffect != null) {
				m_GameplaySys.PlaySoundEffect (m_SoundEffect);
			}

			if(m_Corpse!=null) {
				GameObject.Instantiate (m_Corpse, transform.position, transform.rotation);
			}

			Destroy(gameObject);
		}
	}

	private bool matchesElement(GameObject go) {
		var elementTag = go.GetComponent<PlayerElementTag> ();
		return elementTag != null && elementTag.getElement () == m_RequiredElement;
	}

}
