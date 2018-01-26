using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementTag : MonoBehaviour {

	[SerializeField] private Element m_Element;

	public Element getElement() {
		return m_Element;
	}

}
