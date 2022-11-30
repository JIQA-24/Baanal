using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class projectile : MonoBehaviourPunCallbacks
{
    
    private Transform player;
    [SerializeField] private float damage;

    //Constant speed o f the projectile
    public float moveSpeed = 1f;

    //Time until projectile expires
    public float timeToLive = 5f;
    private float timeSinceSpawned = 0f;

    public int id;
    public Player photonPlayer;
    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreLayerCollision(7,10,true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime; 

        timeSinceSpawned += Time.deltaTime;

        if(timeSinceSpawned > timeToLive){
            Destroy(gameObject);
        }
        //photonView.RPC("start_mult", RpcTarget.All,gameObject.GetComponent<PhotonView>().ViewID);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    [PunRPC]
    void start_mult(int _id)
    {
        //GameObject obj = PhotonNetwork.GetPhotonView(_id).gameObject;
    }
}
