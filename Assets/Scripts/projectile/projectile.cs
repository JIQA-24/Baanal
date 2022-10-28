using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    private Transform player;
    [SerializeField] private float damage;

    //Constant speed o f the projectile
    public float moveSpeed = 1f;

    //Time until projectile expires
    public float timeToLive = 5f;
    private float timeSinceSpawned = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime; 

        timeSinceSpawned += Time.deltaTime;

        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
