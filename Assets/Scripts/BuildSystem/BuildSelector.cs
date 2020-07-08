using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{

    public GameObject buildPanel;
    public BuildSystem buildSystem;
    // private bool showPanel = true;
    [SerializeField] GameObject notEnoughtMoney;



    public void StartBuild(GameObject go)//used by buttons in shop
    {
        int cost = go.GetComponent<PreviewObj>().prefab.GetComponent<BuildingFunctionallity>().building.buildingCost;
        if (EconomyManager.instance.CheckGoldAmount() > cost)
        {
            EconomyManager.instance.UseGold(cost);
            EconomyManager.instance.UpdateText();
            buildSystem.NewBuild(go);//this "Starts" a new build in the build system
            TogglePanel(false);

        }
        else
        {
            StartCoroutine(TogleNoMoneyNotification());
        }

        
    }

    public IEnumerator TogleNoMoneyNotification()
    {
        notEnoughtMoney.SetActive(true);
        yield return new WaitForSeconds(1.5f);// hardcoded for prototyping
        notEnoughtMoney.SetActive(false);
    }

    public void TogglePanel(bool state)
    {
       // showPanel = !showPanel;
        buildPanel.SetActive(state);
    }

}
