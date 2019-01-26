using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickableTypes
{
    CARROT,
    BABY
}

public class PickableObject : MonoBehaviour
{

    public bool isPicked = false;

    public PickableTypes pickableType;
    
}
