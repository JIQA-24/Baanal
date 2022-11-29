using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 20f;
    public float arrowDamage = 20f;
    private float force = 3f;
    private Vector2 direction;
    Rigidbody2D rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        direction = transform.right * arrowSpeed;
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "DetectionZones")
        {
            Destroy(gameObject);
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(arrowDamage, direction, force);
            }
            if (collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<BossHealthSystem>().BossTakeDamage(arrowDamage);
            }
        }
    }
}
