using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator animator;
	private PlayerState playerState;

	// Use this for initialization
	private void Awake()
	{
		animator = GetComponent<Animator>();
		playerState = GetComponent<PlayerState>();
	}

	// Update is called once per frame
	void Update () {
		var running = true;

		if (playerState.absVelX > 0 && playerState.absVelY < playerState.standingThreshold)
			running = false;

		animator.SetBool("Running", running);
	}
}
