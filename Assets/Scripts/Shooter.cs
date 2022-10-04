using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletProp;
    public GameObject arrowProp;
    public GameObject bombProp;
    public Transform firePointSlingshot;
    public Transform firePointBow;
    public bool canShoot = true;
    public int fireArm = 0;
    private IEnumerator coroutine;
    public GunChangeUI change;


    

    private void Start() {
        CheckFireArm();
    }

    private void Update() {
        if(PauseMenu.gameIsPaused){
            return;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotationY = 0f;
        if( mousePos.x < transform.position.x){
            rotationY = 180f;
        }

        transform.eulerAngles = new Vector3(transform.rotation.x, rotationY, transform.rotation.z);

        if(Input.GetButtonDown("next_mask") && fireArm < 1 && !change.isChangeCooldown){
            fireArm += 1;
            change.ChangeUI(fireArm);
            CheckFireArm();
        }
        if(Input.GetButtonDown("previous_mask") && fireArm > 0 && !change.isChangeCooldown){
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
            case 1:
                slingshot.GetComponent<SpriteRenderer>().enabled = false;
                bow.GetComponent<SpriteRenderer>().enabled = true;
                maskEquipable.GetComponent<SpriteRenderer>().enabled = true;
                break;
            default:
                slingshot.GetComponent<SpriteRenderer>().enabled = true;
                bow.GetComponent<SpriteRenderer>().enabled = false;
                maskEquipable.GetComponent<SpriteRenderer>().enabled = false;
                break;
        }
    }

    private void CheckShot(){
        switch(fireArm){
            case 1:
                if(coroutine != null){
                    StopCoroutine(coroutine);
                }
                coroutine = BowShot();
                StartCoroutine(coroutine);
                break;
            default:
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
