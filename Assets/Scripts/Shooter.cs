using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Health ifDead;
    [SerializeField] private UI_Inventory uiInventory;
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
    public GunChangeUI change;
    List<Item> equipedGun;
    public Animator animator;

    public CharacterController2D controller;
    private Quaternion fireSlingRot;
    private Quaternion fireGeneralRot;
    private Vector3 playerPos;
    private Vector2 aimDir;
    public bool isLocked = false;

    private void Awake()
    {
        inventory = uiInventory.GetInventory();
    }


    private void Start() {
        fireSlingRot = firePointSlingshot.rotation;
        fireGeneralRot = firePointBow.rotation;
        CheckFireArm();
        inventory = uiInventory.GetInventory();
    }
    public void changeOfInventory()
    {
        change.ChangeUI(fireArm);
        CheckFireArm();
    }

    private void Update() {
        if(PauseMenu.gameIsPaused || ifDead.dead){
            return;
        }
        aimDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        ChangeAimDir(aimDir);
        playerPos = GetComponent<Transform>().position;
        equipedGun = inventory.GetEquipedList();

        bool isCD = change.isChangeCooldown;

        if (Input.GetButtonDown("previous_mask"))
        {
            isLocked = !isLocked;
        }


        if (Input.GetButtonDown("next_mask") && !isCD){
            if(fireArm == equipedGun[0].weaponChangeNum)
            {
                fireArm = 0;
            }
            else
            {
                fireArm = equipedGun[0].weaponChangeNum;
            }

            if(equipedGun[0].itemType != Item.ItemType.UnequipedMask)
            {
                change.ChangeUI(fireArm);
                CheckFireArm();
            }
            
        }


        if(Input.GetButtonDown("Fire1") && canShoot){
            canShoot = false;
            CheckShot();
        }
        if (Input.GetButtonDown("Fire2") && !change.isBombsCooldown && change.numberOfBombs > 0)
        {
            change.ChangeBomb();
            BombThrow();
        }
    }

    private void CheckFireArm(){
        Transform slingshot = transform.Find("Slingshot");
        Transform spear = transform.Find("Spear");
        Transform maskEquipable = transform.Find("MaskEquipable");
        switch(fireArm){
            default:
            case 2:
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                maskEquipable.GetComponent<SpriteRenderer>().sprite = equipedGun[0].GetSprite();
                spear.GetComponent<SpriteRenderer>().enabled = true;
                slingshot.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case 1:
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                slingshot.GetComponent<SpriteRenderer>().enabled = false;
                spear.GetComponent<SpriteRenderer>().enabled = false;
                maskEquipable.GetComponent<SpriteRenderer>().sprite = equipedGun[0].GetSprite();
                break;
            case 0:
                slingshot.GetComponent<SpriteRenderer>().enabled = true;
                spear.GetComponent<SpriteRenderer>().enabled = false;
                maskEquipable.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    private void CheckShot(){
        switch(fireArm){
            default:
            case 2:
                if (coroutine != null)
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
                canShoot = true;
                RegularShot();
                SoundManager.PlaySound(SoundManager.Sound.RegularShot);
                break;
        }
    }

    private void RegularShot() {
        GameObject bullet = Instantiate(bulletProp, firePointSlingshot.transform.position, firePointSlingshot.transform.rotation);
        Destroy(bullet, 1f);
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
        yield return new WaitForSeconds(1f);
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
}
