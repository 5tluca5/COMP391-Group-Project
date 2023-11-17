using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CountdownTimer : MonoBehaviour
{
    private float timeRemaining = GameConstant.Wave_Timer;
    public bool timerIsRunning = false;

    public ReactiveProperty<float> time { get; private set; }
    public ReactiveProperty<bool> timeRunOut { get; private set; }
    public Text timeText;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                time = new ReactiveProperty<float>(0);
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                timeRunOut = (ReactiveProperty<bool>)time.Select(x => x == 0).ToReactiveProperty(); 
            }
        }

        if (timeRemaining == 0) { }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EnableGameTimer() 
    {
        timerIsRunning = true;
    }

    public ReactiveProperty<bool> SubscribeTimeRunOut() 
    {
        return timeRunOut;
    }

}
