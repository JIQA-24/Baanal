using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private float Speed;
    private bool RestoreTime;
    // Start is called before the first frame update
    public GameObject ImpactEffect;
    private CameraShake cameraShake;
    private ReferenceScript reference;
    private Animator Anim;

    private void Start()
    {
        reference = GameObject.Find("ReferenceObject").GetComponent<ReferenceScript>();
        cameraShake = reference.GetCameraShake();
        RestoreTime = false;
        //Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (RestoreTime)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * Speed;
            }
            else
            {
                Time.timeScale = 1f;
                RestoreTime = false;
                //Anim.SetBool("Damaged", false);
            }
        }
    }

    public void StopTime(float ChangeTime, int RestoreSpeed, float Delay)
    {
        Speed = RestoreSpeed;

        if (Delay > 0)
        {
            StopCoroutine(StartTimeAgain(Delay));
            StartCoroutine(StartTimeAgain(Delay));
        }
        else
        {
            RestoreTime = true;
        }

        Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        StartCoroutine(cameraShake.Shake(.03f, .2f));
        //Anim.SetBool("Damaged", true);

        Time.timeScale = ChangeTime;
    }

    IEnumerator StartTimeAgain(float amt)
    {
        yield return new WaitForSecondsRealtime(amt);
        RestoreTime = true;
    }
}
