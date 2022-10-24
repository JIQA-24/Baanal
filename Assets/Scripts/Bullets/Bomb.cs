using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombSpeed = 5f;
    public float bombDamage = 50f;
    public Vector3 launchOffSet;
    private float force = 3f;
    private Vector2 direction;

    void Start()
    {
        direction = transform.right + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(direction * bombSpeed, ForceMode2D.Impulse);
        transform.Translate(launchOffSet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.PlaySound(SoundManager.Sound.Avispero); //reproduce audio de golpe del avispero
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(bombDamage, direction, force);
            }
        }
    }
}
