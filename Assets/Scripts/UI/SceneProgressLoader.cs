using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneProgressLoader : MonoBehaviour
{
    [SerializeField] private Text progressText;
    [SerializeField] private Slider slider;

    private AsyncOperation operation;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
       
        int loaders = FindObjectsOfType<SceneProgressLoader>().Length;
        if (loaders > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);

        }
    }
    public void LoadScene(int sceneIndex)
    {
        UpdateProgressUi(0);
        canvas.gameObject.SetActive(true);
        StartCoroutine(BeginLoad(sceneIndex));
    }

    private IEnumerator BeginLoad(int sceneIndex)
    {
        //actually first time using AsyncOperation here
        operation = SceneManager.LoadSceneAsync(sceneIndex);
       
       //updating progress bar while scene is loading
        while (!operation.isDone)
        {
            UpdateProgressUi(operation.progress);
            yield return null;
        }
       //updating it again when loading is done
        UpdateProgressUi(operation.progress);
        operation = null;
        canvas.gameObject.SetActive(false);
    }

    private void UpdateProgressUi(float progress)
    {
       //updating slider
        slider.value = progress;
        //updating text
        progressText.text = (int)(progress * 100f) + " %";
    }
}
