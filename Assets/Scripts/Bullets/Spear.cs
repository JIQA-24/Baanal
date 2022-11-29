using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed = 10f;
    private float spearDamage = 20f;
    private int bodiesPassed = 0;
    private float force = 3f;
    private Vector2 direction;

    Rigidbody2D rigidBody;
    Collider2D spearCollider;
    void Start()
    {
        spearCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        direction = transform.right * spearSpeed;
        rigidBody.AddForce(direction, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "DetectionZones")
        {
            if (collision.gameObject.tag == "Enemy" && bodiesPassed < 2)
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(spearDamage, direction, force);
                bodiesPassed += 1;
                spearDamage += 10;
            }
            if (collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<BossHealthSystem>().BossTakeDamage(spearDamage);
                bodiesPassed += 1;
                spearDamage += 10;
            }
            if (collision.gameObject.tag == "Stage" || collision.gameObject.tag == "OneWayPlatform")
            {
                Destroy(gameObject);
            }
        }
        if (bodiesPassed >= 2)
        {
            Destroy(gameObject);
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(spearDamage, direction, force);
            }
            if (collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<BossHealthSystem>().BossTakeDamage(spearDamage);
            }
        }
    }
}
