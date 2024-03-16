using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int playerHealth = 3;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI deathText;

    private void Start() 
    {
        if(isPlayer)
        {
            healthText.text = playerHealth.ToString();
        }    
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(isPlayer)
        {
            playerHealth--;
            healthText.text = playerHealth.ToString();
            Destroy(other.gameObject);
            if(playerHealth == 0)
            {
                Die();
                Destroy(this.gameObject);
            }
        }
        else
        {
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathText.enabled = true;
    }
}
