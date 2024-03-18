using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDay : MonoBehaviour
{
    [SerializeField] float lengthOfDay = 60f; //in seconds
    [SerializeField] Slider timeSlider;
    [SerializeField] GameObject endOfDayMenu;

    public static Action EndOfDay = delegate { };

    float timeLeft;
    bool paused = false;
    
    void Awake() {
        timeLeft = lengthOfDay;
    }

    void Start()
    {
        timeSlider.value = timeLeft / lengthOfDay;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = timeLeft - Time.deltaTime;
        timeSlider.value = timeLeft / lengthOfDay;
        if(timeLeft < 0 && !paused)
        {
            paused = true;
            EndOfDay.Invoke();
            Time.timeScale = 0;
            endOfDayMenu.SetActive(true);
        }
    }
}
