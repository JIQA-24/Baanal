using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (0,3,-20);
    }
}
