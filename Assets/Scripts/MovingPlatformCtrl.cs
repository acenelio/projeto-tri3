using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCtrl : MonoBehaviour {

	public Transform pos1, pos2;
	public float speed = 2f;
	public float waitTime = 0.5f;
	Vector3 nextPos;

	void Start () {
		nextPos = pos1.position;
		StartCoroutine(Move());
	}
	
	IEnumerator Move() {
		while (true) {
			if (transform.position == pos1.position) {
				nextPos = pos2.position;
				yield return new WaitForSeconds(waitTime);
			}
			if (transform.position == pos2.position) {
				nextPos = pos1.position;
				yield return new WaitForSeconds(waitTime);
			}
			transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
			yield return null;
		}
	}

	void OnDrawGizmos() {
		Gizmos.DrawLine(pos1.position, pos2.position);
	}
}
