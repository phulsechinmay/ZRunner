using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

	public bool actionButton;
	public float absVelX;
	public float absVelY;
	public float standingThreshold = 1f;
	public bool standing;

	private Rigidbody2D body2d;

	private void Awake()
	{
		body2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		actionButton = Input.anyKeyDown;
	}

	private void FixedUpdate()
	{
		absVelX = Mathf.Abs(body2d.velocity.x);
		absVelY = Mathf.Abs(body2d.velocity.y);

		standing = absVelY <= standingThreshold;
	}
}
