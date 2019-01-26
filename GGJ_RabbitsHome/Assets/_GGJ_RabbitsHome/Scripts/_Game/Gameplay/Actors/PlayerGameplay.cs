using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActions
{
    PICKUP,
    PUT,
    PLACE
}


public class PlayerGameplay : MonoBehaviour
{

    public Collider standTrigger;

    public GameObject pickupContainer;

    public PickupPlace activePickupPlace = null;

    // PARAMS
    public bool freeHands = true;

    // Start is called before the first frame update
    void Start()
    {
 
    }


    public void TakeObject()
    {
        if (!freeHands)
        {
            return;
        }

    }

    public void PlaceObject()
    {
        if (freeHands)
        {
            return;
        }
    }
}
