using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed = 10f;
    private float spearDamage = 20f;
    private int bodiesPassed = 0;

    Rigidbody2D rigidBody;
    Collider2D spearCollider;
    void Start()
    {
        spearCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        Vector2 force = transform.right * spearSpeed;
        rigidBody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            if(other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(spearDamage);
            }
            if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossHealthSystem>().BossTakeDamage(spearDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(spearDamage);
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
            spearCollider.isTrigger = false;
        }
    }
}
