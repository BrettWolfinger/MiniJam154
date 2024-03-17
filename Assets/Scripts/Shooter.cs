using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] StatsMasterListSO statsList;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float reloadSpeed = 2f;
    [SerializeField] TextMeshProUGUI ammoCountUI;

    Upgrades upgradeManager;
    int shotsPerReload;

    int bulletsLeft;
    bool IsReloading = false;

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shotsPerReload = (int) upgradeManager.GetStatValue(statsList.Ammo.name);
        bulletsLeft = shotsPerReload;
        ammoCountUI.text = bulletsLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector2 mousePosition)
    {
        if(bulletsLeft > 0 )
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                    transform.position, Quaternion.identity);
                
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = (mousePosition - (Vector2)transform.position) * projectileSpeed;
            }
            bulletsLeft--;
            ammoCountUI.text = bulletsLeft.ToString();
        }
        else
        {
            if(!IsReloading) 
            {
                print("Is reloading");
                IsReloading = true;
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        bulletsLeft = shotsPerReload;
        ammoCountUI.text = bulletsLeft.ToString();
        IsReloading = false;
    }
}
