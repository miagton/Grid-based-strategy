using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateHolder : MonoBehaviour
{
    public void SaveGAme()
    {
        SaveLoad.StartSaving();
    }

    private void OnEnable()
    {
        SaveLoad.StartLoading();
    }
}
