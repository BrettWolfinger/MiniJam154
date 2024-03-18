using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Saver
{
    [SerializeField] StatsMasterListSO statsMasterList;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Button repairButton;
    [SerializeField] GameObject deathPanel;
    [SerializeField] int repairCost = 5;

    int playerHealth = 3;
    int maxHealth;
    string fileEnding = "Health.json";
    Money moneyManager;
    TextMeshProUGUI repairButtonText;
    Upgrades upgradeManager;
    SFX sFX;


    public static Action PlayerDied = delegate { };

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        moneyManager = FindObjectOfType<Money>();
        sFX = FindObjectOfType<SFX>();
        if(repairButton != null)
            repairButtonText = repairButton.GetComponentInChildren<TextMeshProUGUI>();
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
        if(repairButton != null)
            UpdateRepairButtonText();
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        sFX.PlayTrainHitSound();
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
        UpdateRepairButtonText();
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

    void UpdateRepairButtonText()
    {
        repairButtonText.text = "Repair One Health $" + repairCost;
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathPanel.SetActive(true);
        PlayerDied.Invoke();
        Destroy(this.gameObject);
    }
}