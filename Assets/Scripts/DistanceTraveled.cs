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
    [SerializeField] TextMeshProUGUI deathScreenText;

    float distanceTraveledToday = 0;
    float totalDistanceTraveled = 0;
    TextMeshProUGUI totalDistanceText;
    Leaderboard leaderboard;

    string fileEnding = "/Distance.json";
    Upgrades upgradeManager;

    void Awake()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
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
        PlayerHealth.PlayerDied += EndGame;
        //PhaseManager.DiedGameplayPhase += ResetSave;
    }

    void OnDisable()
    {
        PhaseManager.SurvivedGameplayPhase -= AddDistanceToTotal;
        PlayerHealth.PlayerDied -= EndGame;
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

    void EndGame()
    {
        AddDistanceToTotal();
        StartCoroutine(SubmitDistance());
        DeathScreenText();
    }

    IEnumerator SubmitDistance()
    {
        yield return leaderboard.SubmitScoreRoutine((int)totalDistanceTraveled);
    }

    public void UpdateDistanceText()
    {
        totalDistanceText.text = "Total Distance: " + totalDistanceTraveled.ToString();
    }

    public void DeathScreenText()
    {
        deathScreenText.text = deathScreenText.text + ((int) totalDistanceTraveled).ToString();
    }
}
