using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolCtrl : MonoBehaviour {

	public Transform pos1, pos2;
	public float speed = 2f;
	public float waitTime = 3f;
	Vector3 nextPos;

	Animator anim;
	SpriteRenderer sr;

	void Start () {
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		nextPos = pos1.position;
		StartCoroutine(Move());
	}
	
	IEnumerator Move() {
		while (true) {
			if (transform.position == pos1.position) {
				nextPos = pos2.position;
				anim.SetInteger("State", 1);
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State", 0);
				sr.flipX = !sr.flipX;
			}
			if (transform.position == pos2.position) {
				nextPos = pos1.position;
				anim.SetInteger("State", 1);
				yield return new WaitForSeconds(waitTime);
				anim.SetInteger("State", 0);
				sr.flipX = !sr.flipX;
			}
			transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
			yield return null;
		} 
	}

	void OnDrawGizmos() {
		Gizmos.DrawLine(pos1.position, pos2.position);
	}
}
