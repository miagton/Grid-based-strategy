using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonCycle : MonoBehaviour
{
    public GameObject shopButton;
    public GameObject shopPanel;

    bool shopIsEnabled = false;
    void Update()
    {
        shopButton.SetActive(!shopPanel.activeSelf);
       // TogleMenuButton();
    }

    private void TogleMenuButton()
    {
        if (shopPanel.activeSelf)
        {
            shopButton.SetActive(false); 
        }
        else
        {
            StartCoroutine(ActivateButton(true));
        }
    }

    private IEnumerator ActivateButton(bool state)
    {
        yield return new WaitForSeconds(0.5f);
        shopButton.SetActive(state);
    }
}
