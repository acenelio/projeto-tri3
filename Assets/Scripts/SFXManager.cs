using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public static SFXManager instance;

	public GameObject coinParticles;
	public GameObject killParticles;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
	}

	public void ShowCoinParticles(GameObject obj) {
		Instantiate(coinParticles, obj.transform.position, Quaternion.identity);
	}

	public void ShowKillParticles(GameObject obj) {
		Instantiate(killParticles, obj.transform.position, Quaternion.identity);
	}
}
