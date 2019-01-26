using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthController : MonoBehaviour
{
    public static BirthController Instance;

    [Header("Timers")]
    public float birthTime = 20f;
    public bool isCounting = false;
    private float timer = 0;
    private float timerNormalized = 0;

    [Header("Baby")]
    public bool babyIsReady;
    public bool babyIsWaiting;
    public Animator birthAnimator;

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        ResetTimer();

        babyIsReady = false;
        babyIsWaiting = false;
    }

    private void StopTimer()
    {
        isCounting = false;
        timer = 0;
        timerNormalized = timer / birthTime;
    }

    private void ResetTimer()
    {
        isCounting = true;
        timer = 0;
        timerNormalized = timer / birthTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer > birthTime)
        {
            StopTimer();

            babyIsReady = true;
        }

        if (babyIsReady)
        {
            if (!babyIsWaiting)
            {
                DeployBaby();
            }
        }

    }

    public void DeployBaby()
    {
        babyIsReady = false;
        ResetTimer();

        birthAnimator.SetTrigger("Start");
    }

    public void SetBabyReady()
    {
        babyIsWaiting = true;

        var pickupPlace = GetComponent<PickupPlace>();

        pickupPlace.AllowPickup();
    }


}
