using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerPrefsSaving : MonoBehaviour
{
    private PlayerData playerData;
    //public Slider Test;
    [SerializeField] private Slider Test;
    //[SerializeField] private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerData();
        LoadData();
    }

    private void CreatePlayerData()
    {
        playerData = new PlayerData(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))//Cambiar este para que se guarden los valores
            SaveData();

        if (Input.GetKeyUp(KeyCode.L))//Cambiar este para que se carguen los valores
            LoadData();
    }

    public void SaveData() {
        //Test = GameObject.Find("UICanvas/PauseMenu/VolumeSlider").GetComponent<Slider>();
        PlayerPrefs.SetFloat("sliderVolume", Test.value);
        //PlayerPrefs.SetFloat("health", playerData.health);
    }

    public void LoadData()
    {
        // Test = GameObject.Find("UICanvas/PauseMenu/VolumeSlider").GetComponent<Slider>();
        if (PlayerPrefs.GetFloat("sliderVolume") == null)  {
            Test.value = 1;
        }
        else {
            Test.value = PlayerPrefs.GetFloat("sliderVolume");
            AudioAssets.i.ChangeMasterVolume(PlayerPrefs.GetFloat("sliderVolume"));
        }
        
        //Test.value = PlayerPrefs.GetFloat("sliderVolume");
        //playerData = new PlayerData(PlayerPrefs.GetFloat("sliderVolume"));

        //Debug.Log(playerData.ToString());
    }
}
