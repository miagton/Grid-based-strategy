using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public BuildSystem buildSystem;
    public BuildSelector buildSelector;
    public GameObject panel;

    public Text nameText;
    public Text lvlText;
    public Text descriptionText;
    public Text costText;
    public Text upgradeCostText;
    public Text produceTimeText;
    public Text produceAmountText;

    [SerializeField] int upgradeModifier=2;// optionally is used only 1 modifier for all building stats for prototype 
    
    
    private bool showPanel = false;

    private Building building;
    private GameObject buildingGameObject;

    public void ShowUpgradeBuilding(Building _building,GameObject _buildingGameobject)
    {
        building = _building;
        buildingGameObject = _buildingGameobject;

        UpdateUI();
        TogglePanel(true);
    }
    public void HideUpgradePanel()
    {
        TogglePanel(false);
    }
    public void PickUpBuilding()
    {
        Vector3 position = buildingGameObject.transform.position;
        buildSystem.NewBuild(building.previewGameObject,building);//starting building cycle with preview gameobject and passing our building to save info from it
       
        //reseting values and destroying actual building gameobject in order to create feeling of picking it up in order to move to new position
        building = null;
        Destroy(buildingGameObject);
        buildingGameObject = null;

        TogglePanel(false);
    }
    public void DestroyBuilding()
    {
        building = null;
        Destroy(buildingGameObject);
        buildingGameObject = null;
        TogglePanel(false);
    }

    public void UpgradeBuilding()
    {
        // checking if theres enough resourses to upgrade
        if (EconomyManager.instance.CheckGoldAmount() >= building.costToUpgrade)
        {
            EconomyManager.instance.UseGold(building.costToUpgrade);
            EconomyManager.instance.UpdateText();
            //hard coded just for purpose of creating prototype
            UpdateBuildingStats();
            StartCoroutine(ShowUpgradeEffects());//optional in prototype => to visally show that building was upgraded
            //Debug.Log("Building " + building.buuldingName + " has been upgraded!"); => for debugging 
            UpdateUI();//updating UI with new values
        }
        StartCoroutine(buildSelector.TogleNoMoneyNotification());// showing no money notification

    }

    private void UpdateBuildingStats()
    {
        building.currentLVL += upgradeModifier;
        building.buildingCost += upgradeModifier * 2;
        building.costToUpgrade += upgradeModifier * 3;
        building.productionTime += upgradeModifier;
    }

    private IEnumerator ShowUpgradeEffects()
    {

        SoundManager.instance.PlayUpgradeSound();
        building.lvlUpVFX.Play();
        yield return new WaitForSeconds(1f);
        
    }

    public void TogglePanel(bool state)
    {
        //showPanel = !showPanel;
       // CameraCOntrollerv2 controller = FindObjectOfType<CameraCOntrollerv2>();
        panel.SetActive(state);
      //  controller.gameObject.SetActive(!state);
    }
    public void UpdateUI()
    {
        nameText.text = building.buuldingName;
        lvlText.text = building.currentLVL.ToString();
        descriptionText.text = building.buildingDescription;
        costText.text = building.buildingCost.ToString();
        upgradeCostText.text = building.costToUpgrade.ToString();
        produceTimeText.text = building.productionTime.ToString();
        produceAmountText.text = building.prodactionAmount.ToString();

    }



}
