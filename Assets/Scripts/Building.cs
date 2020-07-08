using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


[System.Serializable]
public class Building  //data holder
{
    public string buuldingName;
    public string buildingDescription;

    public int currentLVL;
    public int maxLVL;

    public int buildingCost;
    public int costToUpgrade;

    public float productionTime;
    public int prodactionAmount;


    public GameObject previewGameObject;
  
    public ParticleSystem lvlUpVFX;

    //autogen constructor
    public Building(string buuldingName, string buildingDescription, int currentLVL, int maxLVL, int buildingCost, int costToUpgrade, float productionTime, int prodactionAmount, GameObject previewGameObject, ParticleSystem lvlUpVFX)
    {
        this.buuldingName = buuldingName;
        this.buildingDescription = buildingDescription;
        this.currentLVL = currentLVL;
        this.maxLVL = maxLVL;
        this.buildingCost = buildingCost;
        this.costToUpgrade = costToUpgrade;
        this.productionTime = productionTime;
        this.prodactionAmount = prodactionAmount;
        this.previewGameObject = previewGameObject;
       
        this.lvlUpVFX = lvlUpVFX;
    }
}
