using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : Saver
{
    [SerializeField] StatsMasterListSO statsMasterList;
    [SerializeField] int moneyPerKill;

    public static Action<int> enemyDestroyed = delegate { };

    void OnEnable()
    {
        //UpgradeButtonController.UpgradePurchased += HealthUpgrade;
    }

    void OnDisable()
    {
        //UpgradeButtonController.UpgradePurchased -= HealthUpgrade;
    }

    private void Start() 
    {
       
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        Die();
        //Destroy(other.gameObject);
    }

    private void Die()
    {
        enemyDestroyed.Invoke(moneyPerKill);
        Destroy(this.gameObject);
    }
}