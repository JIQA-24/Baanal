using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class projectile_launcher : MonoBehaviourPunCallbacks
{
    Animator animator;
    public Animator Boss;
    public GameObject projectile;//projectile to spawn
    //public Transform[] spawnLocation;//where to spawn the projectile
    int Zone;
    public Transform spawnLocation_left;
    public Transform spawnLocation_right;
    public Quaternion spawnRotation1;//rotation of projectile on Spawn
    public Quaternion spawnRotation2;
    public float spawnTime = 0.5f;
    private float timeSinceSpawned = 0f;
    public DetectionZone detectionZone;
    public int Feather_num = 0;

    public int id;
    public Player photonPlayer;
    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(spawnLocation[Zone].position);   
    }

    // Update is called once per frame
    void Update()
    {
        if(detectionZone.detectedObjs.Count > 0){
            Boss.SetBool("Feather "+ Feather_num, true);
        }
        else{
            Boss.SetBool("Feather "+ Feather_num, false);
        }
        timeSinceSpawned += Time.deltaTime;

        if((Boss.GetBool("Feather "+ Feather_num)) == true){
            if(timeSinceSpawned >= spawnTime) {
                Zone = Random.Range(1,3);
                if(Zone == 1)
                {
                    Instantiate(projectile, spawnLocation_left.position, spawnRotation1);
                    //photonView.RPC("start_mult", RpcTarget.All,collision.gameObject.GetComponent<PhotonView>().ViewID);
                }
                else
                {
                    Instantiate(projectile, spawnLocation_right.position, spawnRotation2);
                    //photonView.RPC("start_mult", RpcTarget.All,collision.gameObject.GetComponent<PhotonView>().ViewID);
                }
                //Instantiate(projectile, spawnLocation[Zone].position, spawnRotation);
                timeSinceSpawned = 0f;
            }
        }            
    }

    [PunRPC]
    void start_mult(int _id)
    {
        //GameObject obj = PhotonNetwork.GetPhotonView(_id).gameObject;
    }
}
 