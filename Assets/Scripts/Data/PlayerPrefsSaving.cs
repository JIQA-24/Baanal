using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPrefsSaving : MonoBehaviour
{
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerData();
    }

    private void CreatePlayerData()
    {
        playerData = new PlayerData(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SaveData();

        if (Input.GetKeyUp(KeyCode.Escape))
            LoadData();
    }

    public void SaveData() {
        //PlayerPrefs.SetFloat("sliderVolume", GameObject.GetComponent.("VolumeSlider<Slider>").value());
        //PlayerPrefs.SetFloat("health", playerData.health);

    } 

    public void LoadData() 
    {
        playerData = new PlayerData(PlayerPrefs.GetFloat("sliderVolume"));

        Debug.Log(playerData.ToString());
    }
}
