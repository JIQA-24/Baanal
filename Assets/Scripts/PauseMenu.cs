using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public bool isDead;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenu;
    [SerializeField] private PlayerPrefsSaving PrefsSaving;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape)){
            PrefsSaving.SaveData();
            if (gameIsPaused){
                Resume();
            } else{
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                InventoryPause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        PrefsSaving.SaveData();
    }

    public void MenuButton(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        PrefsSaving.SaveData();
        SceneManager.LoadScene("Menu");
    }

    public void DeadMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    void InventoryPause()
    {
        inventoryMenu.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
