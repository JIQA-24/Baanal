using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CacaoBehavior : MonoBehaviour
{
    public int coinValue;
    private CacaoScript cacaoScript;

    private void Start()
    {
        cacaoScript = GameObject.Find("ItemAssets").GetComponent<CacaoScript>();
        Physics2D.IgnoreLayerCollision(7, 10, true);
        Physics2D.IgnoreLayerCollision(10, 11, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "OneWayPlatform" || collision.tag == "Floor")
        {
            //GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            cacaoScript.cacaoTotal += coinValue;
        }
    }
}
