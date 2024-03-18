using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] StatsMasterListSO statsMasterList;
    int damage = 10;
    Upgrades upgradeManager;
    bool hitEnemy = false;
    HitStreak hitStreak;

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        hitStreak = FindObjectOfType<HitStreak>();
    }

    private void Start() 
    {
       damage = (int) upgradeManager.GetStatValue(statsMasterList.Damage.name);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<EnemyHealth>())
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        hitEnemy = true;
        Destroy(this.gameObject);  
    }

    //Destroy offscreen projectiles
    void OnBecameInvisible() 
    {
        Destroy(this.gameObject);
    }

    void OnDestroy() 
    {
        hitStreak.BulletDestruction(hitEnemy);
    }
}
