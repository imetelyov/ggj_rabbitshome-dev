using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlace : MonoBehaviour
{
    public PlaceTrigger placeTrigger;

    public bool timerBasedPickup = false;

    public bool infinitePickup = false;

    public bool canPickup = false;
    public bool canPlace = false;

    public bool vacant = true;

    public PickableTypes pickupType;

    public GameObject pickupObjectPrefab;

    public GameObject pickupObject;

    public GameObject pickupContainer;

    PlayerGameplay activePlayerGP = null;

    // Start is called before the first frame update
    void Start()
    {
        if (placeTrigger == null)
        {
            placeTrigger = GetComponent<PlaceTrigger>();
        }

        placeTrigger.OnTriggerEnter_Custom += SetCanInteractTrue;
        placeTrigger.OnTriggerExit_Custom += SetCanInteractFalse;
    }

    public void SetCanInteractTrue(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activePlayerGP = other.GetComponent<PlayerGameplay>();
            activePlayerGP.activePickupPlace = this;

            if (activePlayerGP.freeHands && (infinitePickup || vacant == false))
            {
                canPickup = true;

                activePlayerGP.activeAction = PlayerActions.PICKUP;
                activePlayerGP.ShowActionIndicator();
                
            }
            
            if (!activePlayerGP.freeHands && vacant && pickupType == activePlayerGP.pickupObject.GetComponent<PickableObject>().pickableType)
            {
                canPlace = true;

                activePlayerGP.activeAction = PlayerActions.PLACE;
                activePlayerGP.ShowActionIndicator();

            }

        }
    }

    public void SetCanInteractFalse(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            canPlace = false;
            activePlayerGP = other.GetComponent<PlayerGameplay>();
            activePlayerGP.activePickupPlace = null;

            activePlayerGP.activeAction = PlayerActions.NONE;
            activePlayerGP.ShowActionIndicator();
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

            DisallowPickup();
        }


        if (infinitePickup || vacant == false)
        {
            canPickup = true;

            if (activePlayerGP.freeHands)
            {
                activePlayerGP.activeAction = PlayerActions.PICKUP;
                activePlayerGP.ShowActionIndicator();
            }
        }

        if (vacant)
        {
            canPlace = true;

            if (!activePlayerGP.freeHands)
            {
                activePlayerGP.activeAction = PlayerActions.PLACE;
                activePlayerGP.ShowActionIndicator();
            }
        }


        return objectToReturn;
    }

    public void Place()
    {
        pickupObject.transform.parent = pickupContainer.transform;
        pickupObject.transform.position = pickupContainer.transform.position;
        pickupObject.transform.rotation = pickupContainer.transform.rotation;

        vacant = false;


        if (infinitePickup || vacant == false)
        {
            canPickup = true;

            if (activePlayerGP.freeHands)
            {
                activePlayerGP.activeAction = PlayerActions.PICKUP;
                activePlayerGP.ShowActionIndicator();
            }
        }

        if (vacant)
        {
            canPlace = true;

            if (!activePlayerGP.freeHands)
            {
                activePlayerGP.activeAction = PlayerActions.PLACE;
                activePlayerGP.ShowActionIndicator();
            }
        }
    }


    public void AllowPickup()
    {
        if (timerBasedPickup)
        {
            infinitePickup = true;
        }
    }

    public void DisallowPickup()
    {
        if (timerBasedPickup)
        {
            infinitePickup = false;
        }
    }
}
