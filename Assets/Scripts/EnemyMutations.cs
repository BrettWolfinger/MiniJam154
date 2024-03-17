using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMutations : MonoBehaviour
{
    [SerializeField] StatsMasterListSO enemyStatsMasterList;
    [SerializeField] TextMeshProUGUI enemyHealthDisplay;
    Upgrades upgradeManager;
    public static Action<UpgradeableStatSO> Mutation = delegate { };

    void Awake() 
    {
        upgradeManager = FindObjectOfType<Upgrades>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthDisplay();
    }

    public void Mutate()
    {
        int randRoll = UnityEngine.Random.Range(0,enemyStatsMasterList.statsList.Length);

        print(randRoll);
        print(enemyStatsMasterList.statsList[randRoll]);
        Mutation.Invoke(enemyStatsMasterList.statsList[randRoll]);
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        enemyHealthDisplay.text = "Enemy Health: " + upgradeManager.GetStatValue(enemyStatsMasterList.EnemyHealth.name);
    }
}
