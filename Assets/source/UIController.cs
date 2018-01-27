using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Sprite[] sprites;

	public Image titleImage;

	public GameObject titleGo;

	public bool showTitleScreen = true;

	void Start() {
		StartCoroutine (playTitleAnimation());
	}

	public IEnumerator playTitleAnimation() {
		if (showTitleScreen) {
			for (int i = 0; i < sprites.Length; i++) {
				titleImage.sprite = sprites [i];
				yield return new WaitForSeconds (2f / sprites.Length);
			}

			yield return new WaitForSeconds (1f);
		}

		titleGo.SetActive (false);
	}

}
