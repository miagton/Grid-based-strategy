using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner2 : MonoBehaviour
{
   
    public float x_Start, z_Start;
    public int ColumnLength;
    public int RowLength;
    public int x_Space, z_Space;
    public GameObject prefab;
    public BuildSystem buildSystem;

    void Start()
    {
      for (int i = 0; i < ColumnLength + RowLength; i++)
      {
            Vector3 position;
            position = new Vector3(x_Start + (x_Space * (i % ColumnLength)),0f, z_Start + (-z_Space * (i / ColumnLength)));
            GameObject go= Instantiate(prefab, position, Quaternion.identity);
            go.GetComponent<GroundCube>().SetBuildSystem(buildSystem);//pass a buildSystem referance to the ground cube prefab
            go.transform.SetParent(transform);//so 
      }

    }
   
}
