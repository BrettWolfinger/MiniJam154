using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DayCounter : Saver
{
    TextMeshProUGUI dayText;
    int day = 1;
    string fileEnding = "/Day.json";

    void Awake()
    {
        dayText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        day = Load(fileEnding);
        //No save data found, initialize at default value
        if(day == -1)
        {
            day = 1;
            Save(day,fileEnding);
        }
    }

    void Start()
    {
        UpdateDayText();
    }


    void OnEnable()
    {
        PhaseManager.SurvivedGameplayPhase += IncrementDay;
        //PhaseManager.DiedGameplayPhase += ResetSave;
    }

    void OnDisable()
    {
        PhaseManager.SurvivedGameplayPhase -= IncrementDay;
        //PhaseManager.DiedGameplayPhase -= ResetSave;
    }

    void IncrementDay()
    {
        day++;
        Save(day,fileEnding);
    }

    public void UpdateDayText()
    {
        dayText.text = "Day: " + day.ToString();
    }
}
