using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacesTypes
{
    BIRTH,
    FRIGE,
    CHAIR,
    TABLE,
    SCHOOL,
    BED,
    GAME
}

public class PlaceTrigger : MonoBehaviour
{

    public Action<Collider> OnTriggerEnter_Custom;
    public Action<Collider> OnTriggerStay_Custom;
    public Action<Collider> OnTriggerExit_Custom;

    public Collider placeCollider;

    public PlacesTypes placeType;

    // Start is called before the first frame update
    void Awake()
    {
        Init();

        if (placeCollider == null)
        {
            placeCollider = GetComponent<Collider>();
        }
    }

    public void Init()
    {
        OnTriggerEnter_Custom = null;
        OnTriggerStay_Custom = null;
        OnTriggerExit_Custom = null;
    }

    // AS TRIGGER - FIXED UPDATE

    private void OnTriggerEnter(Collider other)
    {
        if (OnTriggerEnter_Custom != null)
        {
            OnTriggerEnter_Custom(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (OnTriggerStay_Custom != null)
        {
            OnTriggerStay_Custom(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnTriggerExit_Custom != null)
        {
            OnTriggerExit_Custom(other);
        }
    }

}
