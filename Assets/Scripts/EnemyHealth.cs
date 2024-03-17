using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : Saver
{
    [SerializeField] StatsMasterListSO statsMasterList;
    [SerializeField] int moneyPerKill;
    int enemyHealth;
    Upgrades upgradeManager;
    HitStreak hitStreak;

    public static Action<int,float> enemyDestroyed = delegate { };

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        hitStreak = FindObjectOfType<HitStreak>();
    }
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
       enemyHealth = (int) upgradeManager.GetStatValue(statsMasterList.EnemyHealth.name);
       print(enemyHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    //Enemy collides with player
    private void OnCollisionEnter2D(Collision2D other) 
    {
       if(other.gameObject.GetComponent<PlayerHealth>())
       {
            Destroy(this.gameObject);
       }
    }

    private void Die()
    {
        enemyDestroyed.Invoke(moneyPerKill,hitStreak.GetMultiplier());
        Destroy(this.gameObject);
    }
}