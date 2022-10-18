using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenu;

    void Update()
    {
        if(Input.GetButtonDown("Pause")){
            if(gameIsPaused){
                Resume();
            } else{
                Pause();
            }
        }
        if (Input.GetButtonDown("Inventory"))
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
    }

    public void MenuButton(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("Menu");

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
