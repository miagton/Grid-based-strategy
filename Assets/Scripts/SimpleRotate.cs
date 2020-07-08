using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{

    string gameId = "3702357";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}




public class SimpleRotate : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1f, 0);
    }
}
