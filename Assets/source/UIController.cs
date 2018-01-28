using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public Sprite[] sprites;

	public Sprite[] winSwirlSprites;

	public Image titleImage;
	public GameObject titleGo;

	public Image winSwirlImage;
	public Image winImage;
	public Image winBackground;
	public GameObject winGo;

	public bool showTitleScreen = true;

	private bool titleScreenDone = false;

	void Start() {
		StartCoroutine (playTitleAnimation());
	}

	public void ShowWinScreen() {
		StartCoroutine (playWinAnimation());
	}

	private IEnumerator playWinAnimation() {
		while (!titleScreenDone) {
			yield return new WaitForSecondsRealtime (1f);
		}

		// show swirl and fade in end screen
		for (int i = 0; i < winSwirlSprites.Length; i++) {
			winSwirlImage.sprite = winSwirlSprites [i];
			var c = winSwirlImage.color;
			c.a = Mathf.Min (1f, ((float)i) / winSwirlSprites.Length * 10);
			winSwirlImage.color = c;

			var c2 = winBackground.color;
			c2.a = Mathf.Min (1f, ((float)i) / winSwirlSprites.Length * 10);
			winBackground.color = c2;

			var c3 = winImage.color;
			var c3FadeBegin = (int)winSwirlSprites.Length * 0.75f;
			c3.a = Mathf.Min (1f, (i - c3FadeBegin) / (winSwirlSprites.Length-c3FadeBegin));
			winImage.color = c3;

			yield return new WaitForSecondsRealtime (2f / winSwirlSprites.Length);

			winGo.SetActive (true);
		}

		var c4 = winImage.color;
		c4.a = 1;
		winImage.color = c4;

		yield return new WaitForSecondsRealtime (4f);

		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	private IEnumerator playTitleAnimation() {
		if (showTitleScreen) {
			for (int i = 0; i < sprites.Length; i++) {
				titleImage.sprite = sprites [i];
				yield return new WaitForSecondsRealtime (2f / sprites.Length);
			}

			yield return new WaitForSecondsRealtime (1f);
		}

		titleGo.SetActive (false);
		titleScreenDone = true;
	}

}
