using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public RectTransform pauseMenuUI;
    public GameObject inventoryMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
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
        pauseMenuUI.localScale = new Vector3(0,0,0);
        inventoryMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void MenuButton(){
        pauseMenuUI.localScale = new Vector3(0, 0, 0);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("Menu");

    }

    void Pause(){
        pauseMenuUI.localScale = new Vector3(1, 1, 1);
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
