using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public AudioSFX audioSFX;
	public AudioPlayer audioPlayer;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	public void PlayCoinPickupSound(GameObject obj) {
		AudioSource.PlayClipAtPoint(audioSFX.coinPickup, obj.transform.position);
	}

	public void PlayJumpSound(GameObject obj) {
		AudioSource.PlayClipAtPoint(audioPlayer.jump, obj.transform.position);
	}

	public void PlayFailSound(GameObject obj) {
		AudioSource.PlayClipAtPoint(audioSFX.fail, obj.transform.position);
	}

	public void PlayLaserSound(GameObject obj) {
		AudioSource.PlayClipAtPoint(audioSFX.laser, obj.transform.position);
	}

	public void PlaySquishSound(GameObject obj) {
		AudioSource.PlayClipAtPoint(audioSFX.squish, obj.transform.position);
	}

	public void PlayStarSound(GameObject obj) {
		Vector3 vet = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
		AudioSource.PlayClipAtPoint(audioSFX.star, vet);
	}

	public void PlayLevelCompleteSound(GameObject obj) {
		Vector3 vet = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
		AudioSource.PlayClipAtPoint(audioSFX.levelComplete, vet);
	}
}
