using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;
    [SerializeField] _GameController gameController;
    private Vector3 newPosition;
    private Vector3 lastPosition;
    private bool isTutorialActive;
    private Camera uiCamera;
    private bool isMultiplayerActive;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        uiCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
        if (gameController.GetPlayersInGame() <= 0)
        {
            isMultiplayerActive = false;
            thingToFollow.SetActive(true);
            uiCamera.orthographicSize = 5;
        }
        else
        {
            isMultiplayerActive = true;
            thingToFollow.SetActive(false);
            uiCamera.orthographicSize = 15;
        }
        lastPosition = transform.position;
        if (!isTutorialActive && !isMultiplayerActive)
        {
            newPosition = thingToFollow.transform.position + new Vector3(-1, -6, -20);
            if (newPosition.x <= -13 || newPosition.x >= 13)
            {
                transform.position = new Vector3(lastPosition.x, newPosition.y, -20);
            }
            else
            {
                transform.position = newPosition;
            }
        }
        else
        {
            newPosition = thingToFollow.transform.position + new Vector3(3, 2, -20);
            transform.position = newPosition;
        }

    }
}
