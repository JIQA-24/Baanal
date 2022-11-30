using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{

    public GameObject startButton;
    public Animator animator;
    public string nextScene;

    public void startNewGame()
    {
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete() {
        SceneManager.LoadScene(nextScene);
    }
}
