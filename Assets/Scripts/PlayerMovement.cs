using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;

	public float moveSpeed = 100f;

	float horizontalMove = 0f;

	bool jump = false;

	bool crouch = false;

	private bool canDash = true;
	private bool isDashing;
	private float dashingPower = 40f;
	private float dashingTime = 0.3f;
	private float dashingCooldown = 1f;
	[SerializeField] private TrailRenderer tr;

	void Update(){
		if(isDashing){
			return;
		}
		horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
		if(Input.GetButtonDown("Jump")){
			jump = true;
		}
		if(Input.GetButtonDown("Crouch")){
			crouch = true;
		} else if(Input.GetButtonUp("Crouch")){
			crouch = false;
		}
		if(Input.GetKeyDown(KeyCode.LeftShift) && canDash){
			StartCoroutine(Dash());
		}
	}

	private void FixedUpdate() {
		if(isDashing){
			return;
		}
		controller.Move(horizontalMove, crouch, jump);
		jump = false;
	}

	// private void OnCollisionEnter2D(Collision2D other) {
	// 	if(other.gameObject.tag == "Enemy"){
	// 		SceneManager.LoadScene("Prototype");
	// 	}
	// }

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Respawn"){
			SceneManager.LoadScene("Prototype");
		}
	}

	private IEnumerator Dash(){
		canDash = false;
		isDashing = true;
		float originalGravity = controller.m_Rigidbody2D.gravityScale;
		controller.m_Rigidbody2D.gravityScale = 0f;
		if(horizontalMove < 0){
			controller.m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
		}
		if(horizontalMove > 0){
			controller.m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
		}
		tr.emitting = true;
		yield return new WaitForSeconds(dashingTime);
		tr.emitting = false;
		controller.m_Rigidbody2D.gravityScale = originalGravity;
		isDashing = false;
		yield return new WaitForSeconds(dashingCooldown);
		canDash = true;
	}
}