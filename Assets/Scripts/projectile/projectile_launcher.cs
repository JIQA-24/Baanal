using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_launcher : MonoBehaviour
{
    Animator animator;
    public Animator Boss;
    public GameObject projectile;//projectile to spawn
    //public Transform[] spawnLocation;//where to spawn the projectile
    int Zone;
    public Transform spawnLocation_left;
    public Transform spawnLocation_right;
    public Quaternion spawnRotation;//rotation of projectile on Spawn
    public float spawnTime = 0.5f;
    private float timeSinceSpawned = 0f;
    public DetectionZone detectionZone;
    public int Feather_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        Zone = 0;
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
                //Instantiate(projectile, spawnLocation[Zone].position, spawnRotation);
                Instantiate(projectile, spawnLocation_left.position, spawnRotation);
                timeSinceSpawned = 0f;
            }
        }            
    }
}
 