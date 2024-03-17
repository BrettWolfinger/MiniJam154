using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : Saver
{
    public float money {get; private set;}

    TextMeshProUGUI moneyText;

    string fileEnding = "/Money.json";

    void Awake()
    {
        moneyText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        money = Load(fileEnding);
        //No save data found, initialize at default value
        if(money == -1)
        {
            money = 0;
            Save((int)money,fileEnding);
        }
    }

    void Start()
    {
        UpdateMoneyText();
    }

    void OnEnable()
    {
        EnemyHealth.enemyDestroyed += AddMoney;
        UpgradeButtonController.UpgradePurchased += PurchaseUpgrade;
        //PhaseManager.DiedGameplayPhase += ResetSave;
    }

    void OnDisable()
    {
        EnemyHealth.enemyDestroyed -= AddMoney;
        UpgradeButtonController.UpgradePurchased -= PurchaseUpgrade;
        //PhaseManager.DiedGameplayPhase -= ResetSave;
    }

    void AddMoney(int moneyPerKill)
    {
        money += moneyPerKill;
        Save((int)money,fileEnding);
        UpdateMoneyText();
    }

    public void SubtractMoney(float amountToSubtract)
    {
        money -= amountToSubtract;
        Save((int)money,fileEnding);
        UpdateMoneyText();
    }

    void PurchaseUpgrade(UpgradeableStatSO upgradedStat)
    {
        SubtractMoney(upgradedStat.costToUpgrade);
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "Money: $" + money.ToString();
    }
}
