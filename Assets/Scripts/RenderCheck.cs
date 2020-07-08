using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCheck : MonoBehaviour
{
    public GameObject camera;
    private MeshRenderer mesh;
    [SerializeField] float renderDistance = 10f;
        
        private void Awake()
        {
          mesh = GetComponent<MeshRenderer>();
          camera = GameObject.FindGameObjectWithTag("MainCamera");
        }


    void Update()
    {
        Vector3 target = camera.transform.position;
        if (Vector3.Distance(this.transform.position, target) > renderDistance)
        {
            mesh.enabled = false;
        }
        else
        {
            mesh.enabled = true;
        }
    }
}
