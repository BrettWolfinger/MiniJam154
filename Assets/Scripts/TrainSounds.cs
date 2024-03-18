using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSounds : MonoBehaviour
{
    [SerializeField] AudioSource constantPlayer;
    [SerializeField] AudioSource whistlePlayer;

    float whistleTimer = 10f;
    bool playerAlive = true;
    bool whistleOnCooldown = false;

    void OnEnable()
    {
        PlayerHealth.PlayerDied += StopConstantPlayer;
    }
    void OnDisable()
    {
        PlayerHealth.PlayerDied -= StopConstantPlayer;
    }

    void Update()
    {
        if(playerAlive && !whistleOnCooldown)
        {
            whistleOnCooldown = true;
            StartCoroutine(PlayWhistle());
        }
    }
    
    private void StopConstantPlayer()
    {
        playerAlive = false;
        constantPlayer.Stop();
    }

    IEnumerator PlayWhistle()
    {
        print("play whistle");
        yield return new WaitForSeconds(whistleTimer);
        whistlePlayer.Play();
        whistleOnCooldown = false;
    }
}
