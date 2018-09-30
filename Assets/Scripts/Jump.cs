using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	public float jumpSpeed = 240f;
	public float forwardSpeed = 20f;

	private Rigidbody2D body2d;
	private PlayerState playerState;

	// Use this for initialization
	void Start () {
		body2d = GetComponent<Rigidbody2D>();
		playerState = GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerState.standing){
			if(playerState.actionButton){
				body2d.velocity = new Vector2((transform.position.x < 0) ? forwardSpeed : 0, jumpSpeed);
			}
		}
	}
}
