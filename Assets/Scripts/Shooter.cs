using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        inventory = uiInventory.GetInventory();
    }


    private void Start() {
        CheckFireArm();
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
        equipedGun = inventory.GetEquipedList();

        bool isCD = change.isChangeCooldown;

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //float rotationY = 0f;
        //if( mousePos.x < transform.position.x){
        //    rotationY = 180f;
        //}

        //transform.eulerAngles = new Vector3(transform.rotation.x, rotationY, transform.rotation.z);

        if(Input.GetKeyDown(KeyCode.R) && !isCD){
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
                canShoot = true;
                SpearShot();
                break;
            case 1:
                animator.SetBool("BowShot", true);
                if(coroutine != null){
                    StopCoroutine(coroutine);
                }
                coroutine = BowShot();
                StartCoroutine(coroutine);
                
                break;
            case 0:
                canShoot = true;
                RegularShot();
                break;
        }
    }

    private void RegularShot() {
        GameObject bullet = Instantiate(bulletProp, firePointSlingshot.transform.position, firePointSlingshot.transform.rotation);
        Destroy(bullet, 1f);
    }

    private void BombThrow()
    {
        GameObject bomb = Instantiate(bombProp, firePointSlingshot.transform.position, firePointSlingshot.transform.rotation);
        Destroy(bomb, 1f);
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

    private void SpearShot()
    {
        GameObject spear = Instantiate(spearProp, firePointSpear.transform.position, firePointSpear.transform.rotation);
        Destroy(spear, 2f);
    }
}
