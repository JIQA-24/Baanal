using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;


public class StartControl : MonoBehaviour
{
   // public TextMeshProUGUI loaderText;
    //private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameLoadRoutine());
        //StartCoroutine(loadingTxtCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void loadGame()
    //{
        //StartCoroutine(loadingTxtCoroutine());
    //}

    //IEnumerator loadingTxtCoroutine()
    //{
        //yield return new WaitForSeconds(0.25f);

        //loaderText.text = "Cargando ";
        //for (int i = 0; i < counter; i++)
        //{

            //loaderText.text = loaderText.text + ".";
        //}
        //counter++;
        //if (counter == 4)
        //{
            //counter = 0;
        //}
        //StartCoroutine(loadingTxtCoroutine());
    //}

    private IEnumerator GameLoadRoutine()
    {
        AsyncOperation op;
        op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Menu");

        op.allowSceneActivation = false;
        yield return new WaitForSeconds(2f);
        op.allowSceneActivation = true;
    }
}
