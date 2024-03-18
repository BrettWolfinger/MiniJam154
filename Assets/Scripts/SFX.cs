using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioClip gunSound;
    [SerializeField] AudioClip trainHitSound;
    [SerializeField] AudioClip trainDestroyedSound;
    [SerializeField] AudioClip purchaseUpgradeSound;
    //[SerializeField] AudioClip enemyDestroyedSound;
    AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        UpgradeButtonController.UpgradePurchased += PlayPurchaseUpgradeSound;
        PlayerHealth.PlayerDied += PlayTrainDestroyedSound;
        //EnemyHealth.enemyDestroyed += PlayEnemyDestroyedSound;
    }

    void OnDisable()
    {
        UpgradeButtonController.UpgradePurchased -= PlayPurchaseUpgradeSound;
        PlayerHealth.PlayerDied -= PlayTrainDestroyedSound;
        //EnemyHealth.enemyDestroyed -= PlayEnemyDestroyedSound;
    }
    
    public void PlayGunSound()
    {
        audioSource.clip = gunSound;
        audioSource.Play();
    }

    public void PlayTrainHitSound()
    {
        audioSource.clip = trainHitSound;
        audioSource.Play();
    }

    public void PlayTrainDestroyedSound()
    {
        audioSource.clip = trainDestroyedSound;
        audioSource.Play();
    }

    public void PlayPurchaseUpgradeSound(UpgradeableStatSO ignore)
    {
        audioSource.clip = purchaseUpgradeSound;
        audioSource.Play();
    }

    // private void PlayEnemyDestroyedSound(int arg1, float arg2)
    // {
    //     audioSource.clip = enemyDestroyedSound;
    //     audioSource.Play();
    // }
}
