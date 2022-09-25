using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Prototype");
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
