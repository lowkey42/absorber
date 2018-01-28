using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class UIController : MonoBehaviour {

	public Sprite[] sprites;
	public Sprite tutorialSprite;

	public Sprite[] winSwirlSprites;

	public Image titleImage;
	public Image titleBackground;
	public GameObject titleGo;

	public Image winSwirlImage;
	public Image winImage;
	public Image winBackground;
	public GameObject winGo;

	public bool showTitleScreen = true;

	private bool titleScreenDone = false;

	void Start() {
		StartCoroutine (playTitleAnimation());
		Time.timeScale = 0;
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

			if (tutorialSprite!=null) {
				titleBackground.color = new Color (77f/255f,77f/255f,77f/255f);
				titleImage.sprite = tutorialSprite;

				while (!CrossPlatformInputManager.GetButtonDown ("Vertical_P1") && !CrossPlatformInputManager.GetButtonDown ("Vertical_P2") && !CrossPlatformInputManager.GetButtonDown ("Horizontal_P1") && !CrossPlatformInputManager.GetButtonDown ("Horizontal_P2")) {
					yield return new WaitForSecondsRealtime (0.01f);
				}
			}
		}

		titleGo.SetActive (false);
		titleScreenDone = true;
		Time.timeScale = 1;
	}

}
