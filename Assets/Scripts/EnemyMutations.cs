using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMutations : MonoBehaviour
{
    [SerializeField] StatsMasterListSO enemyStatsMasterList;
    [SerializeField] TextMeshProUGUI enemyHealthDisplay;
    [SerializeField] TextMeshProUGUI mutationDisplayText;
    Upgrades upgradeManager;
    public static Action<UpgradeableStatSO> Mutation = delegate { };

    void Awake() 
    {
        upgradeManager = FindObjectOfType<Upgrades>();    
    }

    void OnEnable()
    {
        TimeOfDay.EndOfDay += Mutate;
    }

    void OnDisable()
    {
        TimeOfDay.EndOfDay -= Mutate;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(enemyHealthDisplay != null)
            UpdateHealthDisplay();
    }

    public void Mutate()
    {
        int randRoll = UnityEngine.Random.Range(0,enemyStatsMasterList.statsList.Length);

        print(randRoll);
        print(enemyStatsMasterList.statsList[randRoll]);
        Mutation.Invoke(enemyStatsMasterList.statsList[randRoll]);
        float newValue = upgradeManager.GetStatValue(enemyStatsMasterList.statsList[randRoll].name);
        float change = enemyStatsMasterList.statsList[randRoll].changePerUpgrade;
        mutationDisplayText.text = enemyStatsMasterList.statsList[randRoll].name + "\n" + (newValue - change) + "-->" + newValue;
    }

    void UpdateHealthDisplay()
    {
        enemyHealthDisplay.text = "Enemy Health: " + upgradeManager.GetStatValue(enemyStatsMasterList.EnemyHealth.name);
    }
}
