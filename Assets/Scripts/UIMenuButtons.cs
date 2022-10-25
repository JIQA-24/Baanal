using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMenuButtons : MonoBehaviour
{
    [SerializeField] private PlayerPrefsSaving Test;
    public void StartGame()
    {
        
        SceneManager.LoadScene("Prototype");
        Test.LoadData();
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
