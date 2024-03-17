using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using TMPro;
using UnityEngine;

public class DistanceTraveled : Saver
{
    [SerializeField] StatsMasterListSO statsMasterList;
    int speed; //mps
    [SerializeField] bool isTraveling = false;

    float distanceTraveledToday = 0;
    float totalDistanceTraveled = 0;
    TextMeshProUGUI totalDistanceText;

    string fileEnding = "/Distance.json";
        Upgrades upgradeManager;

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        totalDistanceText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        totalDistanceTraveled = Load(fileEnding);
        //No save data found, initialize at default value
        if(totalDistanceTraveled == -1)
        {
            totalDistanceTraveled = 0;
            Save((int)totalDistanceTraveled,fileEnding);
        }
    }

    void Start()
    {
        speed = (int) upgradeManager.GetStatValue(statsMasterList.Speed.name);
        UpdateDistanceText();
    }

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
            //deathText.text = "Distance: " + distanceTraveledToday;
        }
    }

    
    private void AddDistanceToTotal()
    {
        totalDistanceTraveled += distanceTraveledToday;
        Save((int)totalDistanceTraveled,fileEnding);
    }

    public void UpdateDistanceText()
    {
        totalDistanceText.text = "Total Distance: " + totalDistanceTraveled.ToString();
    }
}
