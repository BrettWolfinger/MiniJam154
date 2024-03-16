using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class DistanceTraveled : MonoBehaviour
{

    [SerializeField] int speed = 5; //mps
    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] bool isTraveling = false;

    float distanceTraveledToday = 0;
    float totalDistanceTraveled = 0;

    void OnEnable()
    {
        PhaseManager.SurvivedGameplayPhase += AddDistanceToTotal;
        //PhaseManager.DiedGameplayPhase += ResetSave;
    }

    void OnDisable()
    {
        PhaseManager.SurvivedGameplayPhase -= AddDistanceToTotal;
        //PhaseManager.DiedGameplayPhase -= ResetSave;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTraveling)
        {
            distanceTraveledToday += speed*Time.deltaTime;
            deathText.text = "Distance: " + distanceTraveledToday;
        }
    }

    
    private void AddDistanceToTotal()
    {
        totalDistanceTraveled += distanceTraveledToday;
    }
}
