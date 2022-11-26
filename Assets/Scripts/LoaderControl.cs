using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;


public class LoaderControl : MonoBehaviour
{

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

    public IEnumerator GameLoadRoutine()
    {
        AsyncOperation op;
        op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Prototype");

        op.allowSceneActivation = false;
        yield return new WaitForSeconds(2f);
        op.allowSceneActivation = true;
        
    }
}
