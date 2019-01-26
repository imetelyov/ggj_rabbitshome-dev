using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlace : MonoBehaviour
{
    public PlaceTrigger placeTrigger;

    public bool infinitePickup = false;
    public bool canPickup = false;

    public bool vacant = true;

    public GameObject pickupObjectPrefab;

    public GameObject pickupObject;

    // Start is called before the first frame update
    void Start()
    {
        if (placeTrigger == null)
        {
            placeTrigger = GetComponent<PlaceTrigger>();
        }

        placeTrigger.OnTriggerEnter_Custom += SetCanPickupTrue;
        placeTrigger.OnTriggerExit_Custom += SetCanPickupFalse;
    }

    public void SetCanPickupTrue(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            PlayerGameplay playerGP = other.GetComponent<PlayerGameplay>();
            playerGP.activePickupPlace = this;

        }
    }

    public void SetCanPickupFalse(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            PlayerGameplay playerGP = other.GetComponent<PlayerGameplay>();
            playerGP.activePickupPlace = null;
        }
    }

    public GameObject Pickup()
    {
        GameObject objectToReturn;

        if (!infinitePickup)
        {
            objectToReturn = pickupObject;
            pickupObject = null;
            vacant = true;
        }
        else
        {
            objectToReturn = pickupObjectPrefab;
            vacant = false;
        }

        return pickupObject;
    }

}
