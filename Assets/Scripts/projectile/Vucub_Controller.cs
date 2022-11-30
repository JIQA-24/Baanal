using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vucub_Controller : MonoBehaviour
{
    Animator animator;
    public Animator Boss;
    private float timeSpent1 = 0f;
    private float timeSpent2 = 0f;
    private float timeSpent3 = 0f;
    private float timeSpent4 = 0f;
    private float timeSpent5 = 0f;
    private float totalTime = 0f;
    private float percentage1 = 0f;
    private float percentage2 = 0f;
    private float percentage3 = 0f;
    private float percentage4 = 0f;
    private float percentage5 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Boss.GetBool("Feather 1")) == true)
        {
            timeSpent1 += Time.deltaTime;
        }

        if((Boss.GetBool("Feather 2")) == true)
        {
            timeSpent2 += Time.deltaTime;
        }

        if((Boss.GetBool("Feather 3")) == true)
        {
            timeSpent3 += Time.deltaTime;
        }

        if((Boss.GetBool("Feather 4")) == true)
        {
            timeSpent4 += Time.deltaTime;
        }

        if((Boss.GetBool("Feather 5")) == true)
        {
            timeSpent5 += Time.deltaTime;
        }
        totalTime = (timeSpent1 + timeSpent2 + timeSpent3 + timeSpent4 + timeSpent5);
        //Debug.Log("Tiempo total: " + totalTime);
        percentage1 =  timeSpent1 *(totalTime/100);
        //Debug.Log("Porcentaje: " + percentage1);
    }
}
