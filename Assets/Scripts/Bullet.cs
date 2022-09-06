using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public float bulletDamage = 10f;

    Rigidbody2D rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        Vector2 force = transform.up * bulletSpeed;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
