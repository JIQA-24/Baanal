using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
        newPosition = thingToFollow.transform.position + new Vector3 (0,2,-20);
        if(newPosition.x <= -13 || newPosition.x >= 13)
        {
            transform.position = new Vector3(lastPosition.x, newPosition.y, -20);
        }
        //if(newPosition.y > 0)
        //{
        //    transform.position = new Vector3(newPosition.x, newPosition.y - 2, -20);
        //}
        else
        {
            transform.position = newPosition;
        }

    }
}
