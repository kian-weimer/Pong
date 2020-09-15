using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int leftPlayerScore;
    public int rightPlayerScore;

    public int numberOfHits;

    public double elapsedTimeInSeconds;
    public int elapsedTimeInMinutes;

    public string elapsedTimeAsString;


    Stopwatch stopwatch = new Stopwatch();


    // Start is called before the first frame update
    void Awake()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        elapsedTimeInSeconds = 0;
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeInSeconds = stopwatch.Elapsed.Seconds;
        elapsedTimeInMinutes = stopwatch.Elapsed.Minutes;

        elapsedTimeAsString = String.Format("{0:00}:{1:00}", elapsedTimeInMinutes, elapsedTimeInSeconds % 60);
        UnityEngine.Debug.Log(elapsedTimeAsString);
    }
}
