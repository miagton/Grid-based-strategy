using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFunctionallity : MonoBehaviour
{

    //can store building producing resourses etc
    public Building building;
    void Start()
    {
        StartCoroutine(ProduceResources());
    }

   private IEnumerator ProduceResources()
    {
        while (true)
        {
            EconomyManager.instance.GainGold(building.prodactionAmount);
            EconomyManager.instance.UpdateText();
            yield return new WaitForSeconds(building.productionTime);
        }
    }
}
