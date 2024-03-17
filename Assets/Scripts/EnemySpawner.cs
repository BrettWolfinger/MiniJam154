using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] StatsMasterListSO enemyStatsMasterList;
    [SerializeField] GameObject EnemyPrefab;
    float timeBetweenSpawns;
    float enemySpeed;

    int leftBound = -8;
    int rightBound = 8;
    bool onCooldown = true;
    Upgrades upgradeManager;

    private void Awake() {
        upgradeManager = FindObjectOfType<Upgrades>();
    }

    private void Start() 
    {
        timeBetweenSpawns = upgradeManager.GetStatValue(enemyStatsMasterList.EnemyRespawnRate.name);
        enemySpeed = upgradeManager.GetStatValue(enemyStatsMasterList.EnemySpeed.name);
        StartCoroutine(InitialDelay());
    }

    private void Update() 
    {
        if(!onCooldown)
            StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        onCooldown = true;
        Vector2 spawnLocation = new Vector2(Random.Range(leftBound,rightBound), -5);
        GameObject instance = Instantiate(EnemyPrefab, spawnLocation, Quaternion.identity);
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = (new Vector2(0,0) - (Vector2)instance.transform.position) * enemySpeed;
        }
        yield return new WaitForSeconds(timeBetweenSpawns);
        onCooldown = false;
    }

    //Short delay at start to give player time to get bearings
    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(2);
        onCooldown = false;
    }
}
