using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonController : BuyMenuElement
{
    public static Action<UpgradeableStatSO> UpgradePurchased = delegate { };

    //OnClick method called by the buy button. Calls everything else relevant to signaling
    // a purchase was made.
    public void BuyUpgrade()
    {
        if(buyMenu.model.moneyManager.money >= buyMenu.model.costToUpgrade)
        {
            UpgradePurchased.Invoke(buyMenu.model.statToUpgrade);
            buyMenu.model.UpdateValues();
            buyMenu.view.UpdateTexts();
        }
        else
        {
            //not enough money
        }
    }
}
