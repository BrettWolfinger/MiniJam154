using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Money : Saver
{
    public float money {get; private set;}

    TextMeshProUGUI moneyText;
    int moneyForDay = 0;

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
        if(SceneManager.GetActiveScene().name == "GameplayScreen")
        {
            UpdateDayMoneyText();
        }
        else
        {
            UpdateMoneyText();
        }
    }

    void OnEnable()
    {
        EnemyHealth.enemyDestroyed += AddMoneyForKill;
        UpgradeButtonController.UpgradePurchased += PurchaseUpgrade;
        PhaseManager.SurvivedGameplayPhase += AddToTotal;
    }

    void OnDisable()
    {
        EnemyHealth.enemyDestroyed -= AddMoneyForKill;
        UpgradeButtonController.UpgradePurchased -= PurchaseUpgrade;
        PhaseManager.SurvivedGameplayPhase -= AddToTotal;
    }

    void AddMoneyForKill(int moneyPerKill, float comboMultiplier)
    {
        moneyForDay += (int) (moneyPerKill*comboMultiplier);
        UpdateDayMoneyText();
    }

    void AddToTotal()
    {
        money += moneyForDay;
        Save((int)money,fileEnding);
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

    public void UpdateDayMoneyText()
    {
        moneyText.text = "Money: $" + moneyForDay.ToString();
    }
}
