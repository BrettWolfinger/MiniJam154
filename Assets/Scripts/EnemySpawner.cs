using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float timeBetweenSpawns = 2f;
    [SerializeField] float enemySpeed = 2f;

    int leftBound = -8;
    int rightBound = 8;
    bool onCooldown = false;

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
}
