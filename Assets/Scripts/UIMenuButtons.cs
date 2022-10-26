using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIMenuButtons : MonoBehaviour
{
    [SerializeField] private PlayerPrefsSaving PrefsSaving;
    public void StartGame()
    {
        PrefsSaving.SaveData();
        SceneManager.LoadScene("Prototype");
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
}
