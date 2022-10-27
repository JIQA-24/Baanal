using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_launcher : MonoBehaviour
{
    Animator animator;

    public Animator Boss;
    
    //projectile to spawn
    public GameObject projectile;

    //where to spawn the projectile
    public Transform spawnLocation;

    //rotation of projectile on Spawn
    public Quaternion spawnRotation;

    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawned += Time.deltaTime;

        if((Boss.GetBool("Feather")) == true){
            if(timeSinceSpawned >= spawnTime) {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                timeSinceSpawned = 0f;
            }
        }            
    }
}
 