using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySystem : MonoBehaviour {

	[SerializeField] protected AudioClip soundEffectAbsorbNone;
	[SerializeField] protected AudioClip soundEffectAbsoreElectricity;
	[SerializeField] protected AudioClip soundEffectAbsorbFire;
	[SerializeField] protected AudioClip soundEffectAbsorbStone;
	[SerializeField] protected AudioClip soundEffectAbsorbWater;
	[SerializeField] protected AudioClip soundEffectAbsorbWind;

	[SerializeField] protected AudioClip soundEffectJump;
	[SerializeField] protected AudioClip soundEffectDeath;
	[SerializeField] protected AudioClip soundEffectWin;

	private AudioSource[] audioSources;
	private UIController uiController;

	private bool gameWon = false;

	void Start() {
		audioSources = GetComponents<AudioSource> ();
		uiController = GameObject.FindObjectOfType<UIController> ();
	}

	public void PlayAbsorbSoundEffect(Element element) {
		switch (element) {
			case Element.none:
				PlaySoundEffect (soundEffectAbsorbNone);
				break;
			case Element.electricity:
				PlaySoundEffect (soundEffectAbsoreElectricity);
				break;
			case Element.fire:
				PlaySoundEffect (soundEffectAbsorbFire);
				break;
			case Element.stone:
				PlaySoundEffect (soundEffectAbsorbStone);
				break;
			case Element.water:
				PlaySoundEffect (soundEffectAbsorbWater);
				break;
			case Element.wind:
				PlaySoundEffect (soundEffectAbsorbWind);
				break;
		}
	}

	public void PlayJumpSoundEffect() {
		PlaySoundEffect (soundEffectJump);
	}
	public void PlayDeathSoundEffect() {
		PlaySoundEffect (soundEffectDeath);
	}

	public void PlaySoundEffect(AudioClip clip) {
		if (clip == null)
			return;

		foreach(AudioSource src in audioSources) {
			if (!src.isPlaying) {
				src.PlayOneShot (clip);
				return;
			}
		}
	}

	private IEnumerator PlaySoundEffectIn(AudioClip clip, float time) {
		if (clip != null) {
			yield return new WaitForSecondsRealtime (time);

			foreach (AudioSource src in audioSources) {
				if (!src.isPlaying) {
					src.PlayOneShot (clip);
					break;
				}
			}
		}
	}

	void Update () {
		if (gameWon)
			return;


		var won = true;
		foreach (TargetArea area in GameObject.FindObjectsOfType<TargetArea> ()) {
			won = won && area.isActivated();
		}

		if (won) {
			gameWon = true;
			Time.timeScale = 0;
			Debug.Log ("Level done!");

			foreach (AudioSource src in audioSources) {
				if (src.isPlaying) {
					StartCoroutine(FadeOut (src, 1f));
				}
			}

			StartCoroutine(PlaySoundEffectIn(soundEffectWin, 0.5f));

			if (uiController != null) {
				uiController.ShowWinScreen ();
			}
		}
	}

	public static IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
		float startVolume = audioSource.volume;

		while (audioSource.volume > 0) {
			audioSource.volume -= startVolume * Time.unscaledTime / FadeTime;

			yield return null;
		}

		audioSource.Stop ();
		audioSource.volume = startVolume;
	}

}
