using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 40f;
    public float bulletDamage = 10f;
    private float force = 3f;
    private Vector2 direction;

    Rigidbody2D rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        direction = transform.up * bulletSpeed;
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            SoundManager.PlaySound(SoundManager.Sound.RegularShotImpact);
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(bulletDamage, direction, force);
            }
        }
    }
}
