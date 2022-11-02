using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombSpeed = 5f;
    public float bombDamage = 50f;
    public Vector3 launchOffSet;


    void Start()
    {
        var direction = transform.right + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(direction * bombSpeed, ForceMode2D.Impulse);
        transform.Translate(launchOffSet);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SoundManager.PlaySound(SoundManager.Sound.Avispero); //reproduce audio de golpe del avispero
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyFollowPlayer>().TakeDamage(bombDamage);
            }
            if (other.gameObject.tag == "Boss")
            {
                other.gameObject.GetComponent<BossHealthSystem>().BossTakeDamage(bombDamage);
            }
        }
    }
}
