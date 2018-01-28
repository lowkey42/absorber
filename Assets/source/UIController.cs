using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Sprite[] sprites;

	public Sprite[] winSwirlSprites;
	public Sprite[] winSprites;

	public Image titleImage;
	public GameObject titleGo;

	public Image winSwirlImage;
	public Image winImage;
	public GameObject winGo;

	public bool showTitleScreen = true;

	private bool titleScreenDone = false;

	void Start() {
		StartCoroutine (playTitleAnimation());
	}

	public void ShowWinScreen() {
		StartCoroutine (playWinAnimation());
	}

	public IEnumerator playWinAnimation() {
		while (!titleScreenDone) {
			yield return new WaitForSeconds (1f);
		}

		// show swirl and fade in end screen
		for (int i = 0; i < winSwirlSprites.Length; i++) {
			winSwirlImage.sprite = winSwirlSprites [i];
			var c = winSwirlImage.color;
			c.a = Mathf.Min (1f, ((float)i) / winSwirlSprites.Length * 10);
			winSwirlImage.color = c;

			// TODO
			var c2 = winImage.color;
			var c2FadeBegin = (int)winSwirlSprites.Length * 0.75f;
			c2.a = Mathf.Min (1f, (i - c2FadeBegin) / c2FadeBegin);
			winImage.color = c2;
			yield return new WaitForSeconds (0.2f / winSwirlSprites.Length);
		}

		yield return new WaitForSeconds (2f);

		// TODO: reload scene
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
		titleScreenDone = true;
	}

}
