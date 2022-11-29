using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shooter : MonoBehaviourPunCallbacks
{
    [SerializeField] private Health ifDead;
    private ReferenceScript reference;
    private UI_Inventory uiInventory;
    [SerializeField] private PlayerMovement player;
    public Inventory inventory;
    public GameObject bulletProp;
    public GameObject arrowProp;
    public GameObject bombProp;
    public GameObject spearProp;
    public Transform firePointSlingshot;
    public Transform firePointBow;
    public Transform firePointSpear;
    public bool canShoot = true;
    public int fireArm = 0;
    private IEnumerator coroutine;
    private GunChangeUI change;
    List<Item> equipedGun;
    public Animator animator;

    public CharacterController2D controller;
    private Quaternion fireSlingRot;
    private Quaternion fireGeneralRot;
    private Vector3 playerPos;
    private Vector2 aimDir;
    public bool isLocked = false;
    public int id;
    public Player photonPlayer;

    private void Awake()
    {
        reference = GameObject.Find("ReferenceObject").GetComponent<ReferenceScript>();
        uiInventory = reference.GetInventoryMenu();
        change = reference.GetGunMenu();
        inventory = uiInventory.GetInventory();
    }


    private void Start() {
        reference = GameObject.Find("ReferenceObject").GetComponent<ReferenceScript>();
        uiInventory = reference.GetInventoryMenu();
        change = reference.GetGunMenu();
        fireSlingRot = firePointSlingshot.rotation;
        fireGeneralRot = firePointBow.rotation;
        CheckFireArm();
        inventory = uiInventory.GetInventory();
    }
    public void changeOfInventory()
    {
        if (change.isChangeCooldown)
        {
            change.maskReturnOnCooldown = true;
            change.maskReturnCooldownVariable = change.maskReturnCooldown;
            change.Restart();
        }
        if (!change.maskReturnOnCooldown)
        {
            change.ChangeUI(equipedGun[0].weaponChangeNum);
        }
        CheckFireArm();
    }

    private void Update() {
        if(PauseMenu.gameIsPaused || ifDead.dead || PauseMenu.inventoryPause || controller.m_Rigidbody2D.isKinematic)
        {
            return;
        }
        aimDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        ChangeAimDir(aimDir);
        playerPos = GetComponent<Transform>().position;
        equipedGun = inventory.GetEquipedList();

        bool isCD = change.isChangeCooldown;

        if (Input.GetKeyDown(KeyCode.C))
        {
            isLocked = !isLocked;
            if (!isLocked)
            {
                player.RemoveBoost();
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (change.maskReturnOnCooldown)
            {
                change.ButtonPressed();
            }
            else if (fireArm != 0 && isCD)
            {
                if (!change.maskReturnOnCooldown)
                {
                    change.Restart();
                }
                fireArm = 0;
                change.maskReturnOnCooldown = true;
                CheckFireArm();
            }
            else if (fireArm == equipedGun[0].weaponChangeNum)
            {
                fireArm = 0;
            }
            else
            {
                fireArm = equipedGun[0].weaponChangeNum;
            }

            if (equipedGun[0].itemType != Item.ItemType.UnequipedMask)
            {
                if (!change.maskReturnOnCooldown)
                {
                    change.ChangeUI(equipedGun[0].weaponChangeNum);
                }
                CheckFireArm();
                change.ActivateMask();
                if (fireArm != 0 && !change.maskReturnOnCooldown)
                {
                    change.ChangeUI(equipedGun[0].weaponChangeNum);
                    CheckFireArm();
                }
                else
                {
                    fireArm = 0;
                }

            }

        }


        if (Input.GetButton("Fire1") && canShoot){
            canShoot = false;
            CheckShot();
        }
        if (Input.GetButtonDown("Fire2") && !change.isBombsCooldown && change.numberOfBombs > 0)
        {
            change.ChangeBomb();
            BombThrow();
        }
    }

    public List<Item> GetEquippedItems()
    {
        return equipedGun;
    }

    private void CheckFireArm(){
        Transform maskEquipable = transform.Find("MaskEquipable");
        switch(fireArm){
            default:
            case 2:
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                maskEquipable.GetComponent<SpriteRenderer>().sprite = equipedGun[0].GetSprite();
                break;
            case 1:
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                maskEquipable.GetComponent<SpriteRenderer>().sprite = equipedGun[0].GetSprite();
                break;
            case 0:
                maskEquipable.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    private void CheckShot(){
        switch(fireArm){
            default:
            case 2:
                animator.SetBool("SpearShot", true);
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = SpearShot();
                StartCoroutine(coroutine);
                SoundManager.PlaySound(SoundManager.Sound.SpearShot);
                break;
            case 1:
                animator.SetBool("BowShot", true);
                if(coroutine != null){
                    StopCoroutine(coroutine);
                }
                coroutine = BowShot();
                StartCoroutine(coroutine);
                SoundManager.PlaySound(SoundManager.Sound.BowShot);
                break;
            case 0:
                animator.SetBool("SlingShot", true);
                if(coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                coroutine = RegularShot();
                StartCoroutine(coroutine);
                SoundManager.PlaySound(SoundManager.Sound.RegularShot);
                break;
        }
    }

    private IEnumerator RegularShot() {

        GameObject bullet = Instantiate(bulletProp, firePointSlingshot.transform.position, firePointSlingshot.transform.rotation);
        Destroy(bullet, 1f);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("SlingShot", false);
        canShoot = true;
    }

    private void BombThrow()
    {
        GameObject bomb = Instantiate(bombProp, firePointBow.transform.position, firePointBow.transform.rotation);
        //Destroy(bomb, 5f);
    }

    private IEnumerator BowShot() {
        int length = 2;
        for (int i = 0; i <= length; i++)
        {
            GameObject arrow = Instantiate(arrowProp, firePointBow.transform.position, firePointBow.transform.rotation);
            Destroy(arrow, 1f);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("BowShot", false);
        yield return new WaitForSeconds(0.3f);
        canShoot = true;
    }

    private IEnumerator SpearShot()
    { 
        GameObject spear = Instantiate(spearProp, firePointSpear.transform.position, firePointSpear.transform.rotation);
        Destroy(spear, 2f);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("SpearShot", false);
        canShoot = true;
    }

    public void ChangeAimDir(Vector3 _aimDir)
    {
        switch (_aimDir)
        {
            default:
            case Vector3 n when (n.x == 0 && n.y == 0):
                if (controller.m_FacingRight)
                {
                    firePointSlingshot.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                    firePointBow.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                    firePointSpear.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                    firePointSlingshot.rotation = fireSlingRot;
                    firePointBow.rotation = fireGeneralRot;
                    firePointSpear.rotation = fireGeneralRot;
                }
                else if (!controller.m_FacingRight)
                {
                    firePointSlingshot.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                    firePointBow.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                    firePointSpear.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                    firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 90f));
                    firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 180f));
                    firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 180f));
                }

                break;
            case Vector3 n when (n.x > 0 && n.y == 0):
                firePointSlingshot.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x + 0.6f, playerPos.y, playerPos.z);
                firePointSlingshot.rotation = fireSlingRot;
                firePointBow.rotation = fireGeneralRot;
                firePointSpear.rotation = fireGeneralRot;
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x < 0 && n.y == 0):
                firePointSlingshot.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x - 0.6f, playerPos.y, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 90f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 180f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 180f));
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x < 0 && n.y > 0):
                firePointSlingshot.position = new Vector3(playerPos.x - 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x - 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x - 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 45f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 135f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 135f));
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x < 0 && n.y < 0):
                firePointSlingshot.position = new Vector3(playerPos.x - 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x - 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x - 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 135f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 225f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 225f));
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x > 0 && n.y > 0):
                firePointSlingshot.position = new Vector3(playerPos.x + 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x + 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x + 0.60f, playerPos.y + 0.8f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 315f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 45f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 45f));
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x > 0 && n.y < 0):
                firePointSlingshot.position = new Vector3(playerPos.x + 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x + 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x + 0.60f, playerPos.y - 0.8f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 225f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 315f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 315f));
                controller.CheckFlip(n.x);
                break;
            case Vector3 n when (n.x == 0 && n.y > 0):
                firePointSlingshot.position = new Vector3(playerPos.x, playerPos.y + 1.2f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x, playerPos.y + 1.2f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x, playerPos.y + 1.2f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 90f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 90f));
                break;
            case Vector3 n when (n.x == 0 && n.y < 0):
                firePointSlingshot.position = new Vector3(playerPos.x, playerPos.y - 1.2f, playerPos.z);
                firePointBow.position = new Vector3(playerPos.x, playerPos.y - 1.2f, playerPos.z);
                firePointSpear.position = new Vector3(playerPos.x, playerPos.y - 1.2f, playerPos.z);
                firePointSlingshot.rotation = Quaternion.Euler(new Vector3(fireSlingRot.x, fireSlingRot.y, fireSlingRot.z + 180f));
                firePointBow.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 270f));
                firePointSpear.rotation = Quaternion.Euler(new Vector3(fireGeneralRot.x, fireGeneralRot.y, fireGeneralRot.z + 270f));
                break;
        }
    }
    [PunRPC]
    public void Prueba1(Player player)
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
