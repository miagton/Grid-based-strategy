using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundCube : MonoBehaviour
{

    public Color highlightColor;
    private Color normalColor;
    private Color currentColor;
  [SerializeField] GameObject[] borderObjects;


    private MeshRenderer myRend;
    private bool isSelected;
    private bool isEdge;
    private BuildSystem buildSystem;

  
    private void Awake()
    {
        
        foreach(GameObject child in borderObjects)
        {
            child.SetActive(false);
        }
     
    }

    private void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        myRend.material.shader = Shader.Find("Universal Render Pipeline/Lit");
       
        normalColor = myRend.material.GetColor("_BaseColor");//getting the color of the ground cube (green color)
        currentColor = normalColor;
        CheckEdge();
        
    }
   

    private void CheckEdge()
    {
        
        Vector3[] directions = { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        foreach(var direction in directions)
        {
            Collider[] hits = Physics.OverlapSphere(this.transform.position + direction*.8f, 0.1f);
           
            if (hits.Length<1)
            {
                isEdge = true;
            }
        }
        if (isEdge )
        {
            
            int i = UnityEngine.Random.Range(0, borderObjects.Length);
            borderObjects[i].SetActive(true);
        }
    }

    public void SetBuildSystem(BuildSystem _build)//setting a ref to the build system that was passed in by the GridSpawner...this 
                                                    //saves us from having to do a GameObject.Find("Whatever").Getcomponent<BuildSystem>()
    {
        buildSystem = _build;
    }

    //public void OnMouseEnter()//when the mouse moves over this ground cube this method will get called
    //{
    //    if (!buildSystem.GetIsBuilding())//GetIsBuilding() returns a bool (true/false) from the buildSystem
    //    {
    //        HandleSelection();
    //    }
    //}

    //public void OnMouseExit()//when the mouse moves off of this ground cube this method will get called
    //{
    //    if (!buildSystem.GetIsBuilding())
    //    {
    //        HandleSelection();
    //    }
    //}

    public void HandleSelection()//a better name would be ToggleSelection()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            myRend.material.SetColor("_BaseColor",highlightColor);
        }
        else
        {
            myRend.material.SetColor("_BaseColor", normalColor);
        }

        myRend.material.color = currentColor;//setting the MeshRenderer's material color to whatever the currentColor is
    }


   
}
