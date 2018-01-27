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
	[SerializeField] protected AudioClip soundEffectBreak;

	private AudioSource[] audioSources;

	private bool gameWon = false;

	void Start() {
		audioSources = GetComponents<AudioSource> ();
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
	public void PlayBreakSoundEffect() {
		PlaySoundEffect (soundEffectBreak);
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

	void LateUpdate () {
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
			PlaySoundEffect (soundEffectWin);
			// TODO: show overlay, play sound, redirect to next level
		}
	}
}
