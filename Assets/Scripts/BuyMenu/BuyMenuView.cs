using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*Script to manage the behaviors of UI elements on the BuyScreen
*/
public class BuyMenuView : BuyMenuElement
{


    //Serialized fields for UI components related to an individual fish listing on the buy screen
    [SerializeField] TextMeshProUGUI upgradeText;
    [SerializeField] TextMeshProUGUI currentValueText;
    [SerializeField] TextMeshProUGUI nextValueText;
    // [SerializeField] TextMeshProUGUI StockText;
    // [SerializeField] TextMeshProUGUI PriceText;
    // [SerializeField] Button buyButton;
    
    TextMeshProUGUI buyButtonText;

    void Awake()
    {
        //buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        //Update UI text fields with new information
        UpdateTexts();
    }

    public void UpdateTexts()
    {
        UpdateUpgradeText();
        // UpdateCurrentValueText();
        // UpdateNextValueText();
    }

    //straightforward updater methods to set the text on ui components
    public void UpdateUpgradeText()
    {
        upgradeText.text = buyMenu.model.currentValue.ToString() + "-->" + buyMenu.model.nextValue.ToString();
    }


    //straightforward updater methods to set the text on ui components
    public void UpdateCurrentValueText()
    {
        currentValueText.text = buyMenu.model.currentValue.ToString();
    }

    public void UpdateNextValueText()
    {
        nextValueText.text = "Stock: " + buyMenu.model.nextValue.ToString();
    }
}
