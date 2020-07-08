using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedItemsSet : MonoBehaviour
{
    public HashSet<string> CreatedItems { get; private set; } = new HashSet<string>();
}
