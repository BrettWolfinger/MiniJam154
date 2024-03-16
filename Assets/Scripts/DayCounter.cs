using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    TextMeshProUGUI dayText;
    int day = 1;
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/Day.json";
        dayText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        Load();
    }

    void Start()
    {
        UpdateDayText();
    }


    void OnEnable()
    {
        PhaseManager.SurvivedGameplayPhase += IncrementDay;
        //PhaseManager.DiedGameplayPhase += LoadMainMenu;
    }

    void OnDisable()
    {
        PhaseManager.SurvivedGameplayPhase -= IncrementDay;
        //PhaseManager.DiedGameplayPhase -= LoadMainMenu;
    }

    void IncrementDay()
    {
        day++;
        Save();
    }

    public void UpdateDayText()
    {
        dayText.text = "Day: " + day.ToString();
    }

    private void Save()
    {
        DaySave daySave = new DaySave();
        daySave.day = day;
        string json = JsonUtility.ToJson(daySave);
        System.IO.File.WriteAllText(path, json);
    }

    private void Load()
    {
        DaySave daySave = new DaySave();
        //If there is no save file that exists, create one
        if (!File.Exists(path))
        {
            Save();
        }
        string json = System.IO.File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, daySave);
        day = daySave.day;
    }

    [System.Serializable]
    public class DaySave
    {
        public int day;
    }
}
