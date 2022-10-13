using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vucub_taco_quest : MonoBehaviour
{
    public int velocidad;
    public Transform[] escalas;
    private Transform player;
    int numEscala;
    float dist;
    Animator animator;
    Transform enemy;  

    // Start is called before the first frame update
    void Start()
    {
        numEscala = 0;
        transform.LookAt(escalas[numEscala].position); 
        animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void incrementarEscala()
    {
        numEscala++;
        if(numEscala >= escalas.Length)
            numEscala=0;
        transform.LookAt(escalas[numEscala].position);
    }



    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(transform.position,escalas[numEscala].position);
        if(dist < 0.5f)
            incrementarEscala();
        if((animator.GetBool("Reposition_left")) == true)
        {
            Patrullaje();
        }
        else
            Patrullaje();    
    }

    void Patrullaje()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, velocidad * Time.deltaTime);
        //transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
    }
}
