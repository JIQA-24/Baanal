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
    public Transform firePointSlingshot;
    public Transform firePointBow;
    public bool canShoot = true;
    public int fireArm = 0;
    private IEnumerator coroutine;
    public GunChangeUI change;

    private void Awake()
    {
        inventory = uiInventory.GetInventory();
    }


    private void Start() {
        CheckFireArm();
    }

    private void Update() {
        if(PauseMenu.gameIsPaused || ifDead.dead){
            return;
        }
        List<Item> equipedGun = inventory.GetEquipedList();

        bool isCD = change.isChangeCooldown;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotationY = 0f;
        if( mousePos.x < transform.position.x){
            rotationY = 180f;
        }

        transform.eulerAngles = new Vector3(transform.rotation.x, rotationY, transform.rotation.z);

        if(Input.GetKeyDown(KeyCode.R) && !isCD){
            fireArm += 1;
            Debug.Log(equipedGun[0].itemType);
            change.ChangeUI(fireArm);
            CheckFireArm();
        }
        if(Input.GetKeyDown(KeyCode.Q) && fireArm > 0 && !isCD){
            fireArm -= 1;
            change.ChangeUI(fireArm);
            CheckFireArm();
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
        Transform bow = transform.Find("Bow");
        Transform maskEquipable = transform.Find("MaskEquipable");
        switch(fireArm){
            default:
            case 2:
                break;
            case 1:
                slingshot.GetComponent<SpriteRenderer>().enabled = false;
                bow.GetComponent<SpriteRenderer>().enabled = true;
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 0:
                slingshot.GetComponent<SpriteRenderer>().enabled = true;
                bow.GetComponent<SpriteRenderer>().enabled = false;
                maskEquipable.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    private void CheckShot(){
        switch(fireArm){
            default:
            case 1:
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
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    
    }
}
