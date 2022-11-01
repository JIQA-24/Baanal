using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject inventoryMenu;
    public GameObject optionsMenu;
    public GameObject deadMenu;
    [SerializeField] private PlayerPrefsSaving PrefsSaving;
    [SerializeField] private Health ifDead;

    private void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (ifDead.GetDead())
        {
            DeadMenu();
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
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
        optionsMenu.SetActive(false);
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

    public void DeadMenu()
    {
        deadMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
