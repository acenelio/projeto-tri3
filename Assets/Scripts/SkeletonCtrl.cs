using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCtrl : MonoBehaviour {

	public float speed = 2f;
	public Vector2 direction = new Vector2(-1f, 0f);
	// Use this for initialization
	Rigidbody2D rb;
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void Move() {
		rb.velocity = speed * direction.normalized;
	}
}
