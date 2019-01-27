using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuasarGames;

public enum PlayerActions
{
    PICKUP,
    PUT,
    PLACE,
    NONE
}

public class PlayerGameplay : MonoBehaviour
{

    public Collider standTrigger;

    public GameObject pickupContainer;

    public GameObject pickupObject;

    public PickupPlace activePickupPlace = null;

    public bool showAction = false;
    public PlayerActions activeAction;
    public GameObject pickupIndicator;
    public GameObject placeIndicator;


    // PARAMS
    public bool freeHands = true;

    private void Start()
    {
        activeAction = PlayerActions.NONE;
        ShowActionIndicator();
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() == GameState.GAMEPLAY)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                MakeAction();
            }
        }
    }

    public void MakeAction()
    {
        if (activePickupPlace != null)
        {
            if (activePickupPlace.canPickup && activePickupPlace.infinitePickup && !activePickupPlace.vacant && freeHands)
            {
                freeHands = false;

                pickupObject = Instantiate(activePickupPlace.Pickup(), pickupContainer.transform.position, transform.rotation, pickupContainer.transform);

                
            }
            else if (activePickupPlace.canPickup && !activePickupPlace.infinitePickup && !activePickupPlace.vacant && freeHands)
            {
                freeHands = false;

                pickupObject = activePickupPlace.Pickup();

                pickupObject.transform.position = pickupContainer.transform.position;
                pickupObject.transform.rotation = transform.rotation;
                pickupObject.transform.parent = pickupContainer.transform;

                
            }
            else if (activePickupPlace.canPickup && !freeHands)
            {

            }
            else if (activePickupPlace.canPlace && activePickupPlace.vacant && !freeHands)
            {
                freeHands = true;

                activePickupPlace.avalibleObjectsCount += 1;

                activePickupPlace.pickupObject = pickupObject;
                activePickupPlace.Place();
                
                pickupObject = null;
                
            }
        }

    }

    public void ShowActionIndicator()
    {
        switch (activeAction)
        {
            case PlayerActions.PICKUP:
                pickupIndicator.SetActive(true);
                placeIndicator.SetActive(false);
                break;
            case PlayerActions.PLACE:
                pickupIndicator.SetActive(false);
                placeIndicator.SetActive(true);
                break;
            case PlayerActions.NONE:
                pickupIndicator.SetActive(false);
                placeIndicator.SetActive(false);
                break;
            default:
                pickupIndicator.SetActive(false);
                placeIndicator.SetActive(false);
                break;
        }
        
    }


}
