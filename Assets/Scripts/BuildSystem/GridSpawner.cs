using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public List<GroundCube> grid = new List<GroundCube>();
    public int xLength;//how many cubes in the x direction
    public int zLength;//how many in the z 
    public GameObject tilePRefab;//ground cube prefab
   
    public BuildSystem buildSystem;//ref to the build system


    private void Awake()
    {
        //SaveLoad.Saving += Save;
        //SaveLoad.Loading += Load;
    }
    private void Start()
    {
        if (grid.Count==0) CreateGround();
        
    }


    private void CreateGround()
    {
        for (int x = 0; x < xLength; x++)
        {
            for (int z = 0; z < zLength; z++)
            {
                Vector3 pos = new Vector3(x + 0.5f, 0, z + 0.5f);//offset the x and z by 0.5 so the left and right sides of ground cube are at whole number intervals
                GameObject go = Instantiate(tilePRefab, pos, Quaternion.identity);
                go.GetComponent<GroundCube>().SetBuildSystem(buildSystem);//pass a buildSystem referance to the ground cube prefab
                go.transform.SetParent(transform);//so the hierarchy looks better'
                //grid.Add(go.GetComponent<GroundCube>());// adding to our list to save and load it
            }
        }
    }

    //void Save()
    //{
    //    SaveLoad.Save<List<GroundCube>>(grid, "grid");// may not define type cause of T , but still can be done for more info
    //}
    //void Load()
    //{
    //    if (SaveLoad.SaveAlreadyExists("grid"))
    //        grid = SaveLoad.Load<List<GroundCube>>("grid");
    //}
}
