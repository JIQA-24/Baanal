using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public static bool gameIsPaused = false;
    public static bool inventoryPause = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenu;
    public GameObject optionsMenu;
    public GameObject deadMenu;
    public GameObject pauseMenuFirstButton, optionsMenuFirstButton, deadMenuFirstButton, optionsClosedButton, inventoryMenuFirstButton, endScreenButton;
    public GameObject endScreen;
    public GameObject endScreenText;
    public GameObject DontDestroyOnLoad;
    [SerializeField] private PlayerPrefsSaving PrefsSaving;
    [SerializeField] private Health ifDead;
    [SerializeField] private PlayerMovement player;


    private void Start()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        deadMenu.SetActive(false);
        endScreen.SetActive(false);
    }
    void Update()
    {
        if (ifDead.GetDead())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            PrefsSaving.SaveData();
            if (gameIsPaused || inventoryPause){
                Resume();
            } else{
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPause)
            {
                Resume();
            }
            else
            {
                InventoryPause();
                SoundManager.PlaySound(SoundManager.Sound.OpenInventory);
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        inventoryPause = false;
        PrefsSaving.SaveData();
        player.RemoveBoost();
    }

    public void OptionsMenuOpen()
    {
        optionsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsMenuFirstButton);
    }

    public void OptionsMenuClosed()
    {
        optionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    public void MenuButton(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        PrefsSaving.SaveData();
        Destroy(DontDestroyOnLoad);
        SceneManager.LoadScene("Menu");
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
    }
    void InventoryPause()
    {
        inventoryMenu.SetActive(true);
        //Time.timeScale = 0f;
        inventoryPause = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(inventoryMenuFirstButton);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeadMenu()
    {
        pauseMenuUI.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        deadMenu.SetActive(true);
        Time.timeScale = 0f;
        ifDead.dead = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(deadMenuFirstButton);
    }

    public void EndScreen()
    {
        pauseMenuUI.SetActive(false);
        inventoryMenu.SetActive(false);
        optionsMenu.SetActive(false);
        deadMenu.SetActive(false);
        StartCoroutine(EndScreenActivables());

    }

    private IEnumerator EndScreenActivables()
    {
        endScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        endScreenButton.SetActive(true);
        endScreenText.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(endScreenButton);


    }

    public void SoundMenu()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIButton);
    }

    public void SoundMenuAccept()
    {
        SoundManager.PlaySound(SoundManager.Sound.UIButtonAccept);
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
