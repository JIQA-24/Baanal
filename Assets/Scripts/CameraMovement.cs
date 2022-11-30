using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private bool isTutorialActive;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName == "Tutorial")
        {
            isTutorialActive = true;
        }
        else
        {
            isTutorialActive = false;
        }
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
        newPosition = thingToFollow.transform.position + new Vector3(0, 2, -20);
        if (!isTutorialActive)
        {
            //if (newPosition.x <= -13 || newPosition.x >= 13)
            //{
            //    transform.position = new Vector3(lastPosition.x, newPosition.y, -20);
            //}
            //else
            //{
            //    transform.position = newPosition;
            //}
        }
        else
        {
            transform.position = newPosition;
        }

    }
}
