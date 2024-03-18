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
    [SerializeField] float reloadSpeed = 1.5f;
    [SerializeField] TextMeshProUGUI ammoCountUI;

    Upgrades upgradeManager;
    SFX sFX;
    int shotsPerReload;

    int bulletsLeft;
    bool IsReloading = false;

    void Awake()
    {
        upgradeManager = FindObjectOfType<Upgrades>();
        sFX = FindObjectOfType<SFX>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shotsPerReload = (int) upgradeManager.GetStatValue(statsList.Ammo.name);
        bulletsLeft = shotsPerReload;
        ammoCountUI.text = bulletsLeft.ToString();
        UpdateAmmoText();
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
            sFX.PlayGunSound();
                
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = (mousePosition - (Vector2)transform.position) * projectileSpeed;
            }
            bulletsLeft--;
            UpdateAmmoText();
        }
        if(bulletsLeft == 0)
        {
            if(!IsReloading) 
            {
                print("Is reloading");
                IsReloading = true;
                StartCoroutine(Reload());
            }
        }
    }

    public IEnumerator Reload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(reloadSpeed);
        bulletsLeft = shotsPerReload;
        UpdateAmmoText();
        IsReloading = false;
    }

    void UpdateAmmoText()
    {
        ammoCountUI.text = "Ammo: " + bulletsLeft +"/" + shotsPerReload;
    }
}
