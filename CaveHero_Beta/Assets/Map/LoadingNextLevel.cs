﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingNextLevel : MonoBehaviour
{
    
    public Slider progressBar;
    public static string levelName;

    void Start ()
    {
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string nLevelName)
    {
        yield return new WaitForSeconds(0.5f);

        AsyncOperation loadingOperation;

        loadingOperation = SceneManager.LoadSceneAsync(nLevelName);

        while (!loadingOperation.isDone)
        {
            float loading = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            progressBar.value = loading;
            yield return null;
        }
    }

    public static void setLevelName(string nLevel)
    {
        levelName = nLevel;
    }
}