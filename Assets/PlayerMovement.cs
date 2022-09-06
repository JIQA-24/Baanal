using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float moveSpeed = 100f;

	float horizontalMove = 0f;

	bool jump = false;

	bool crouch = false;

	void Update(){
		horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
		if(Input.GetButtonDown("Jump")){
			jump = true;
		}
		if(Input.GetButtonDown("Crouch")){
			crouch = true;
		} else if(Input.GetButtonUp("Crouch")){
			crouch = false;
		}
	}

	private void FixedUpdate() {
		controller.Move(horizontalMove, crouch, jump);
		jump = false;
	}
}