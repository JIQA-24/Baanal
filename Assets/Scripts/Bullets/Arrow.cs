using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 20f;
    public float arrowDamage = 20f;

    Rigidbody2D rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * arrowSpeed;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(arrowDamage);
            }
        }
    }
}
