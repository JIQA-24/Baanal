using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletProp;
    public Transform firePoint;
    public bool canShoot = true;

    private void Update() {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float rotationY = 0f;
        if( mousePos.x < transform.position.x){
            rotationY = 180f;
        }

        transform.eulerAngles = new Vector3(transform.rotation.x, rotationY, transform.rotation.z);

        if(Input.GetButtonDown("Fire1") && canShoot){
            Shoot();
        }
    }

    private void Shoot() {
        GameObject bullet = Instantiate(bulletProp, firePoint.transform.position, firePoint.transform.rotation);
        Destroy(bullet, 2f);
    }
}
