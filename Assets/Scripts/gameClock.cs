using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class gameClock : MonoBehaviour
{
    public TextMeshProUGUI textTimer;

    private float timer = 0.0f;
    private bool timerActive = false;

    void Awake()
    {
        textTimer = GetComponentInChildren<TextMeshProUGUI>();
        ResetTimer();
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {  
            timer += Time.deltaTime;
            Variables.clock = timer;
            DisplayTime();
        }
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer/60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes *60);

        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void ResetTimer()
    {
        timer = 0.0f;
    }
}
