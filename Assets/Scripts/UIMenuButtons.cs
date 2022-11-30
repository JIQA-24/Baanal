using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIMenuButtons : MonoBehaviour
{
    [SerializeField] private PlayerPrefsSaving PrefsSaving;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject codeMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject codeDeploy;
    public GameObject mainMenuStartButton;
    public GameObject inputTextFieldCodeMenu;
    public GameObject volumeSlider;
    public GameObject copyCodeButton;
    private bool menuActive;

    private void Start()
    {
        startMenu.SetActive(true);
        mainMenu.SetActive(false);
        codeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        codeDeploy.SetActive(false);
        menuActive = false;
        //EnterMainMenu();
    }

    void Update()
    {
        if (Input.GetButton("Submit")&&(!menuActive))
        {
            EnterMainMenu();
            menuActive = true;
        }
    }

    public void EnterMainMenu()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
        codeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        codeDeploy.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuStartButton);
    }

    public void EnterCodeMenu()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        codeMenu.SetActive(true);
        optionsMenu.SetActive(false);
        codeDeploy.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(inputTextFieldCodeMenu);
    }

    public void EnterOptionsMenu()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        codeMenu.SetActive(false);
        optionsMenu.SetActive(true);
        codeDeploy.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(volumeSlider);
    }

    public void CodeDeployMenu()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        codeMenu.SetActive(false);
        optionsMenu.SetActive(false);
        codeDeploy.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(copyCodeButton);
    }
    
    public void StartGame()
    {
        PrefsSaving.SaveData();
        SceneManager.LoadScene("SceneCharger");
    }
    public void StartTutorial()
    {
        PrefsSaving.SaveData();
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        PrefsSaving.SaveData();
        Application.Quit();
    }

    public void FullScreenMode()
    {
        if(Screen.fullScreen)
        {
            // Toggle fullscreen
            Screen.fullScreen = false;
            Debug.Log("Fullscreen desactivado UwU");
        } else 
        {
            Screen.fullScreen = true;
            Debug.Log("Fullscreen activado UwU");
        }
    }
}
