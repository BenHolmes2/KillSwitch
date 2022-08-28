using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using TMPro;
using UnityEngine;

public class TimeBoard : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    Stopwatch timer;
    
    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
       // timeText.text = 
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = timer.Elapsed.Seconds;
        int minutes = timer.Elapsed.Minutes;

        if (minutes<10)
        {
            if (seconds<10)
            {
                timeText.text = "0"+minutes.ToString()+":0"+seconds.ToString();
            }
            else
            {
                timeText.text = "0"+minutes.ToString()+":"+seconds.ToString();
            }
        }
        else
        {
            if (seconds<10)
            {
                timeText.text = minutes.ToString()+":0"+seconds.ToString();
            }
            else
            {
                timeText.text = minutes.ToString()+":"+seconds.ToString();
            };
        }
    }
}
