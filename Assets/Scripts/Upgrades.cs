using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : UpgradeSaver
{
    //[SerializeField] UpgradeableStatSO[] listOfStats;
    [SerializeField] StatsMasterListSO statsMasterListSO;

    Dictionary<string, int> upgradeLevels;
    string fileEnding = "/Upgrades.json";

    void Awake()
    {
        upgradeLevels = LoadUpgrade(fileEnding);
        //No save data found, initialize at default value
        if(upgradeLevels == null)
        {
            ResetUpgradeLevels();
            SaveUpgrade(upgradeLevels,fileEnding);
        }
    }

    void OnEnable()
    {
        UpgradeButtonController.UpgradePurchased += PurchaseUpgrade;
        EnemyMutations.Mutation += PurchaseUpgrade;
    }

    void OnDisable()
    {
        UpgradeButtonController.UpgradePurchased -= PurchaseUpgrade;
        EnemyMutations.Mutation -= PurchaseUpgrade;
    }

    private void PurchaseUpgrade(UpgradeableStatSO upgradedStat)
    {
        IncreaseUpgradeLevel(upgradedStat.name);
    }

    void IncreaseUpgradeLevel(string statName)
    {
        upgradeLevels[statName] += 1;
        SaveUpgrade(upgradeLevels,fileEnding);
    }

    void ResetUpgradeLevels()
    {
        upgradeLevels = new Dictionary<string, int>();

        foreach(UpgradeableStatSO stat in statsMasterListSO.statsList)
        {
            upgradeLevels.Add(stat.name,0);
        }
    }

    public float GetStatValue(string statName)
    {
        UpgradeableStatSO result = Array.Find(statsMasterListSO.statsList, element => element.name == statName);
        
        return result.baseValue + (upgradeLevels[result.name] * result.changePerUpgrade);
    }
}
