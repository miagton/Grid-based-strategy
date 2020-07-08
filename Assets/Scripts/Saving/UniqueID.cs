using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string ID { get; private set; }

    private void Awake()
    {
        ID = transform.position.sqrMagnitude + " " + name + " " + transform.GetSiblingIndex();
        Debug.Log("id for " + name + " is " + ID);
    }
}
