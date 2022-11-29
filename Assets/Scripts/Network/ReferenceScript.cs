using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReferenceScript : MonoBehaviour
{
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] UI_Inventory _Inventory;
    [SerializeField] GunChangeUI gunUI;
    [SerializeField] Image[] hearts;
    [SerializeField] CameraShake cameraShake;

    public PauseMenu GetPauseMenu()
    {
        return pauseMenu;
    }
    public UI_Inventory GetInventoryMenu()
    {
        return _Inventory;
    }
    public GunChangeUI GetGunMenu()
    {
        return gunUI;
    }
    public Image[] GetHeartList()
    {
        return hearts;
    }

    public CameraShake GetCameraShake()
    {
        return cameraShake;
    }
}