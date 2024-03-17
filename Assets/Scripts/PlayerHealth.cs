using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : Saver
{
    [SerializeField] StatsMasterListSO statsMasterList;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject deathPanel;

    int playerHealth = 3;
    int maxHealth;
    string fileEnding = "Health.json";
    Money moneyManager;
    int repairCost = 10;
    Upgrades upgradeManager;

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        moneyManager = FindObjectOfType<Money>();
    }

    void OnEnable()
    {
        UpgradeButtonController.UpgradePurchased += HealthUpgrade;
    }

    void OnDisable()
    {
        UpgradeButtonController.UpgradePurchased -= HealthUpgrade;
    }

    private void Start() 
    {
        maxHealth = (int) upgradeManager.GetStatValue(statsMasterList.MaxHealth.name);
        playerHealth = Load(fileEnding);
        //No save data found, initialize at default value
        if(playerHealth == -1)
        {
            playerHealth = (int) statsMasterList.MaxHealth.baseValue;
            Save(playerHealth,fileEnding);
        }
        UpdateHealthText();
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        playerHealth--;
        Save(playerHealth,fileEnding);
        UpdateHealthText();
        Destroy(other.gameObject);
        if(playerHealth == 0)
        {
            Die();
        }
    }

    void Heal()
    {
        playerHealth++;
        UpdateHealthText();
        Save(playerHealth,fileEnding);
    }

    void HealthUpgrade(UpgradeableStatSO statToUpgrade)
    {
        if(statToUpgrade == statsMasterList.MaxHealth)
        {
            maxHealth++;
            Heal();
        }
    }

    public void PurchaseRepair()
    {
        if(moneyManager.money >= repairCost && playerHealth < maxHealth)
        {
            moneyManager.SubtractMoney(repairCost);
            Heal();
        }
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + playerHealth.ToString() + "/" + maxHealth.ToString(); 
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathPanel.SetActive(true);

        Destroy(this.gameObject);
    }
}