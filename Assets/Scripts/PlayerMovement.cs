using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerMovement : MonoBehaviourPunCallbacks
{
	private ReferenceScript reference;
	private UI_Inventory uiInventory;
	[SerializeField] private Shooter shooter;
	private PauseMenu pauseMenu;

	public CharacterController2D controller;
	public Animator animator;
	private IEnumerator coroutine;
	private Health dead;

	private float moveSpeed = 50f;


	float horizontalMove = 0f;

	public bool doubleActive = false;
	private int doubleCounter = 0;

	bool jump = false;

	bool crouch = false;

	private Vector2 dashingDir;
	private bool canDash = true;
	private bool isDashing;
	private float dashingPower = 15f;
	private float dashingTime = 0.15f;
	[SerializeField] private TrailRenderer tr;

	private GameObject currentOneWayPlatform;
	[SerializeField] private CapsuleCollider2D playerCollider;

	private Inventory inventory;

	public int id;
	public Player photonPlayer;

	private void Awake()
    {
		reference = GameObject.Find("ReferenceObject").GetComponent<ReferenceScript>();
		uiInventory = reference.GetInventoryMenu();
		pauseMenu = reference.GetPauseMenu();
		inventory = new Inventory();
		uiInventory.SetInventory(inventory);
		dead = GetComponent<Health>();
	}


    void Update(){
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			if(isDashing || PauseMenu.gameIsPaused || controller.m_Rigidbody2D.isKinematic)
			{
				return;
			}

			if (shooter.isLocked || PauseMenu.inventoryPause)
			{
				moveSpeed--;
				if(moveSpeed > 0)
				{
					horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
				}
				else
				{
					moveSpeed = 0;
					horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
				}
				animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
				return;
			} else
			{
				horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;
				animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
			}

			
			
			if(Input.GetButtonDown("Jump") && !crouch){
				animator.SetBool("IsJumping", true);
				jump = true;
			}

			if(Input.GetButtonDown("Crouch")){
				crouch = true;
			} else if(Input.GetButtonUp("Crouch")){
				crouch = false;
			}
			if(crouch && Input.GetButtonDown("Jump"))
			{
				if(currentOneWayPlatform != null)
				{
					StartCoroutine(DisableCollisionPlatform());
				}
			}

			if (doubleActive)
			{
				if(doubleCounter < 2)
				{
					canDash = true;
				}
				if (CharacterController2D.m_Grounded)
				{
					canDash = true;
					doubleCounter = 0;
				}
			}
			if (CharacterController2D.m_Grounded) {
				canDash = true;
			}


			if(Input.GetButtonDown("Dash") && canDash)
			{
				isDashing = true;
				canDash = false;
				tr.emitting = true;
				dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
				if(dashingDir == Vector2.zero)
				{
					dashingDir = new Vector2(transform.localScale.x, 0);
				}
				if (doubleActive)
				{
					doubleCounter++;
				}
				//Add stopping dash
				animator.SetBool("IsDashing", true);
				StartCoroutine(Dash());
				SoundManager.PlaySound(SoundManager.Sound.Dash); //reproduce el sonido del dash
			}

			if (isDashing)
			{
				controller.m_Rigidbody2D.velocity = dashingDir.normalized * dashingPower;
				return;
			}
		}
	}

	public void OnLand()
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			animator.SetBool("IsJumping", false);
		}
	}


	private void FixedUpdate() {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			if(isDashing || PauseMenu.gameIsPaused){
				return;
			}
			controller.Move(horizontalMove, crouch, jump);
			jump = false;
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();

			if(itemWorld != null)
			{
				if(coroutine != null)
				{
					StopCoroutine(coroutine);
				}
				coroutine = PickUpAnimation();
				StartCoroutine(coroutine);
				inventory.AddItem(itemWorld.GetItem());
				itemWorld.DestroySelf();
			}
			if (collision.gameObject.tag == "OneWayPlatform")
			{
				currentOneWayPlatform = collision.gameObject;
			}
			if(collision.gameObject.tag == "Boss")
			{
				dead.TakeDamage(1f);
			}
		}
    }

	private IEnumerator PickUpAnimation()
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			animator.SetBool("Interact", true);
			yield return new WaitForSeconds(0.5f);
			animator.SetBool("Interact", false);
		}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			if (collision.gameObject.tag == "OneWayPlatform")
			{
				currentOneWayPlatform = null;
			}
		}
	}

	private IEnumerator DisableCollisionPlatform()
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
			Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
			yield return new WaitForSeconds(0.25f);
			Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
		}
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			if(other.gameObject.tag == "Respawn"){
				SceneManager.LoadScene("Prototype");
			}
			if (other.gameObject.tag == "PuertaTutorial")
			{
				pauseMenu.DeadMenu();
			}
		}
	}

    private IEnumerator Dash()
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			float originalGravity = controller.m_Rigidbody2D.gravityScale;
			controller.m_Rigidbody2D.gravityScale = 0f;
			yield return new WaitForSeconds(dashingTime);
			tr.emitting = false;
			isDashing = false;
			controller.m_Rigidbody2D.gravityScale = originalGravity;
			animator.SetBool("IsDashing", false);
		}
		//yield return new WaitForSeconds(dashingCooldown);
		//canDash = true;

		//canDash = false;
		//isDashing = true;
		//tr.emitting = true;
		//if(horizontalMove < 0){
		//	controller.m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
		//}
		//if(horizontalMove > 0){
		//	controller.m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
		//}
		//yield return new WaitForSeconds(dashingTime);
		//tr.emitting = false;
		//isDashing = false;
		//yield return new WaitForSeconds(dashingCooldown);
		//canDash = true;
	}

	public void AddBoost(float boost)
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			float plus = moveSpeed * boost;
			moveSpeed += plus;
		}
    }
	public void RemoveBoost()
	{
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			moveSpeed = 50f;
		}
	}

	public void DoubleOn()
    {
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			doubleActive = true;
			doubleCounter = 0;
		}
    }

	public void DoubleOff()
	{
		if(!controller.m_Rigidbody2D.isKinematic)
		{
			doubleActive = false;
			doubleCounter = 0;
		}
	}

[PunRPC]
	public void Prueba(Player player)
    {
		photonPlayer = player;
		id = player.ActorNumber;
		_GameController.instance.players[id - 1] = this;
		this.GetComponent<Shooter>().Prueba1(player);
		this.GetComponent<Health>().Prueba2(player);
		this.GetComponent<CharacterController2D>().Prueba3(player);
		if (!photonView.IsMine) // Verificar si el movimiento es del usuario actual
		{
			controller.m_Rigidbody2D.isKinematic = true;
		}
	}
}