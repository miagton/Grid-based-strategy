using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private SceneProgressLoader loader;
   
   

   
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(int index)
    {
        loader = FindObjectOfType<SceneProgressLoader>();
        loader.LoadScene(index);
    }
}
