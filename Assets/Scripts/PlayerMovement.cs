using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private UI_Inventory uiInventory;

	public CharacterController2D controller;
	public Animator animator;

	public float moveSpeed = 100f;

	float horizontalMove = 0f;

	bool jump = false;

	bool crouch = false;

	private Vector2 dashingDir;
	private bool canDash = true;
	private bool isDashing;
	private float dashingPower = 15f;
	private float dashingTime = 0.15f;
	//private float dashingCooldown = 1f;
	[SerializeField] private TrailRenderer tr;

	private GameObject currentOneWayPlatform;
	[SerializeField] private CapsuleCollider2D playerCollider;

	private Inventory inventory;

    private void Awake()
    {
		inventory = new Inventory();
		uiInventory.SetInventory(inventory);

		ItemWorld.SpawnItemWorld(new Vector3(20, 20), new Item { itemType = Item.ItemType.ChaacMask, weaponChangeNum = 1});
		ItemWorld.SpawnItemWorld(new Vector3(-20, 20), new Item { itemType = Item.ItemType.JaguarMask, weaponChangeNum = 2});
	}

	//private void UseItem(Item item)
 //   {
 //       switch (item.itemType)
 //       {
	//		default:
	//		case Item.ItemType.ChaacMask:
	//			inventory.RemoveItem(item);
	//			break;
	//		case Item.ItemType.JaguarMask:
	//			inventory.RemoveItem(item);
	//			break;
	//		case Item.ItemType.UnequipedMask:
	//			inventory.RemoveItem(item);
	//			break;
	//		case Item.ItemType.UnequipedTalisman:
	//			inventory.RemoveItem(item);
	//			break;
	//	}
 //   }


    void Update(){
		if(isDashing || PauseMenu.gameIsPaused){
			return;
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.fixedDeltaTime;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

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

		if (CharacterController2D.m_Grounded) {
			canDash = true;
		}


		if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
		{
			isDashing = true;
			canDash = false;
			tr.emitting = true;
			dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			if(dashingDir == Vector2.zero)
            {
				dashingDir = new Vector2(transform.localScale.x, 0);
            }
			//Add stopping dash
			animator.SetBool("IsDashing", true);
			StartCoroutine(Dash());
		}

        if (isDashing)
        {
			controller.m_Rigidbody2D.velocity = dashingDir.normalized * dashingPower;
			return;
        }

		


	}

	public void OnLand()
    {
		animator.SetBool("IsJumping", false);
	}


	private void FixedUpdate() {
		if(isDashing || PauseMenu.gameIsPaused){
			return;
		}
		controller.Move(horizontalMove, crouch, jump);
		jump = false;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		ItemWorld itemWorld = collision.gameObject.GetComponent<ItemWorld>();

		if(itemWorld != null)
        {
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
        }
        if (collision.gameObject.tag == "OneWayPlatform")

		{
			currentOneWayPlatform = collision.gameObject;
		}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "OneWayPlatform")
		{
			currentOneWayPlatform = null;
		}
	}

	private IEnumerator DisableCollisionPlatform()
    {
		BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
		yield return new WaitForSeconds(0.25f);
		Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Respawn"){
			SceneManager.LoadScene("Prototype");
		}
	}

    private IEnumerator Dash()
    {
        float originalGravity = controller.m_Rigidbody2D.gravityScale;
        controller.m_Rigidbody2D.gravityScale = 0f;
        yield return new WaitForSeconds(dashingTime);
		tr.emitting = false;
		isDashing = false;
        controller.m_Rigidbody2D.gravityScale = originalGravity;
		animator.SetBool("IsDashing", false);

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

}