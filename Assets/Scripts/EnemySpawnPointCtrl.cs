using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointCtrl : MonoBehaviour {

	public GameObject prefab;
	public float objSpeed = 2f;
	public float waitTime = 5f;
	public Vector2[] directions;


	void Start () {
		StartCoroutine(Spawn());
	}


	IEnumerator Spawn() {
		while (true) {
			for (int i=0; i<directions.Length; i++) {
				GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
				obj.GetComponent<SkeletonCtrl>().direction = directions[i];
				obj.GetComponent<SkeletonCtrl>().speed = objSpeed;
			}
			yield return new WaitForSeconds(waitTime);
		}
	}
}
