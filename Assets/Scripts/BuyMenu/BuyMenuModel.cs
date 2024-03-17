using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuModel : BuyMenuElement
{
    //Serialized fields for UI components related to an individual fish listing on the buy screen
    [SerializeField] public UpgradeableStatSO statToUpgrade;
    
    public Money moneyManager;
    public Upgrades upgradeManager;
    public float currentValue;
    public float nextValue;
    public float costToUpgrade;


    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        moneyManager = FindObjectOfType<Money>();
    }
    void Start()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        currentValue = upgradeManager.GetStatValue(statToUpgrade.name);
        nextValue = currentValue + statToUpgrade.changePerUpgrade;
        costToUpgrade = statToUpgrade.costToUpgrade;
    }
}
