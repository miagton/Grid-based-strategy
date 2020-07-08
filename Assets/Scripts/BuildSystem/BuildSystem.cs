using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    public Camera cam;
    public LayerMask layer;

   

    public RayCast rayCastScript;
    public GameObject buildPanel;
    
    private GameObject preview;//this is the preview object that you will be moving around in the scene
    private PreviewObj previewScript;//this is the script that is sitting on that object

    private bool isBuilding = false;
    private Building pickedUpBuilding;


    private void Update()
    {
        
        if (Input.touchCount > 0 &&Input.GetTouch(0).phase==TouchPhase.Ended && isBuilding && previewScript.CanBuild())//d isBuiding = true, and the Preview Script -> canBuild = true + we have a touch there
        {
              
                BuildIt();//then build the thing
               
        }

        //if (Input.GetMouseButtonDown(1) && isBuilding)//stop build
        //{
        //    StopBuild();
        //}

        if (Input.GetKeyDown(KeyCode.R) && isBuilding)//for rotation
        {
            RotatePreviewBuilding();//spins like a top, in 90 degree turns
        }

        if (isBuilding)
        {
            DoRay();
        }
    }

    public void RotatePreviewBuilding()
    {
        preview.transform.Rotate(0f, 90f, 0f);
    }

    public void NewBuild(GameObject _go,Building _pickedUpBuilding=null)//this gets called by one of the buttons ,setting to null will be overrided, but prevents from error in other classes
    {
        preview = Instantiate(_go, Vector3.zero, Quaternion.identity);//set the preview = to something
        previewScript = preview.GetComponent<PreviewObj>();//grab the script that is sitting on the preview
        isBuilding = true;//we can now build

        pickedUpBuilding = _pickedUpBuilding;
       
        rayCastScript.enabled = false;// disabling it to avoid 2 rayacst intersect
        TogleBuildPanel(false);
        
    }

    private void StopBuild()
    {
        Destroy(preview);//get rid of the preview
        preview = null;
        previewScript = null;
       
        isBuilding = false;
       
        pickedUpBuilding = null;

       // selector.TogglePanel();//toggle the button panel back on

        rayCastScript.enabled = true;//enabling back
       // TogleBuildPanel();
    }

    private void BuildIt()//actually build gameobject
    {
        if (pickedUpBuilding != null)
        {
         previewScript.Build(pickedUpBuilding);//just calls the Build() method on the previewScript for moving objects

        }
        else
        {
            previewScript.Build();//just calls the Build() method on the previewScript
        }
        StopBuild();
    }

    private void DoRay()//simple ray cast from the main camera
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layer))//notice there is a layer that we are worried about
        {
            PositionObj(hit.point);
        }
    }

    private void PositionObj(Vector3 _pos)
    {
        int x = Mathf.RoundToInt(_pos.x);//just round the x,y,z values to the nearest int
        //int y = Mathf.RoundToInt(_pos.y);//y value is hardcoded(optional)=> could be change to add Y dimension to game
        int z = Mathf.RoundToInt(_pos.z);

        preview.transform.position = new Vector3(x, 1f, z);//set the previews transform postion to a new Vector3 made up of the x,y,z that is roundedToInt

    }


    public bool GetIsBuilding()// returns the isBuilding bool, so it cant get changed by another script
    {
        return isBuilding;
    }

    private void TogleBuildPanel(bool state )
    {
        buildPanel.SetActive(state);//hides when we are building, and opossite
    }
    public void TogleShopOn()//temporary for button usage
    {
        buildPanel.SetActive(true);
    }
    public void TogleShopOff()//temporary for button usage
    {
        buildPanel.SetActive(false);
    }
}


