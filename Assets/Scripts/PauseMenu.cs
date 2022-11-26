using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool inventoryPause = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenu;
    public GameObject optionsMenu;
    public GameObject deadMenu;
    public GameObject pauseMenuFirstButton, optionsMenuFirstButton, deadMenuFirstButton, optionsClosedButton, inventoryMenuFirstButton;
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

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(deadMenuFirstButton);
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
