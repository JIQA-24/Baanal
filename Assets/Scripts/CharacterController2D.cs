using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController2D : MonoBehaviourPunCallbacks
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private CapsuleCollider2D m_PlayerCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	static public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	public Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	private GameObject gun1;
	private GameObject gun2;
	private GameObject gun3;



	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	public int id;
	public Player photonPlayer;
	public CharacterController2D controller;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		gun1 = GameObject.Find("Slingshot");
		gun2 = GameObject.Find("FirePointBow");
		gun3 = GameObject.Find("Spear");
		SoundManager.Initialize();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		
		// slingshot.GetComponent<Gun>().enabled = false;
		// this.gameObject.GetComponent<Shooter>().enabled = false;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_PlayerCollider != null)
                {
					m_PlayerCollider.offset = new Vector2(0.005519296f, -0.6f);
					m_PlayerCollider.size = new Vector2(0.5682022f, 1.3f);
				}
				
			} else
			{
				// Enable the collider when not crouching
				if (m_PlayerCollider != null)
                {
					m_PlayerCollider.offset = new Vector2(0.005519296f, 0f);
					m_PlayerCollider.size = new Vector2(0.5682022f, 2.5f);

				}

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			if (m_Grounded & move != 0) {
				SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
			}
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			//SoundManager.PlaySound(SoundManager.Sound.FootstepGrass); //reproduce el sonido del salto
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			SoundManager.PlaySound(SoundManager.Sound.Jump); //reproduce el sonido del salto
		}
	}

	public void CheckFlip(float _move)
    {
		if(_move > 0 && !m_FacingRight)
        {
			Flip();
        } else if(_move < 0 && m_FacingRight)
        {
			Flip();
        }
    }
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

  //      Vector3 gun1Scale = gun1.transform.localScale;
  //      gun1Scale.x *= -1;
  //      gun1.transform.localScale = gun1Scale;
  //      Vector3 gun2Scale = gun2.transform.localScale;
  //      gun2Scale.x *= -1;
  //      gun2.transform.localScale = gun2Scale;
		//Vector3 gun3Scale = gun3.transform.localScale;
		//gun3Scale.x *= -1;
		//gun3.transform.localScale = gun3Scale;
    }

	[PunRPC]
	public void Prueba3( Player player)
	{
		photonPlayer = player;
		id = player.ActorNumber;
		//_GameController.instance.players[id - 1] = this.GetComponent<PlayerMovement>();    

		if (!photonView.IsMine) // Verificar si el movimiento es del usuario actual
        {
            controller.m_Rigidbody2D.isKinematic = true;
        }
	}

}