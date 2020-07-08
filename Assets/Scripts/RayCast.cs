using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float interactDIstance;
    public Camera cam;
    public UpgradeManager upgradeManager;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoRaycast();
        }
    }

    private void DoRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit, interactDIstance))
        {
            if (hit.collider.CompareTag("Building"))
            {
                Building building = hit.collider.GetComponent<BuildingFunctionallity>().building;
                upgradeManager.ShowUpgradeBuilding(building, hit.collider.gameObject);
            }
           
            //else // reconsider l8er cause of raycast hitting objects
            //{
            //    upgradeManager.HideUpgradePanel();

            //}
        }
    }
}
