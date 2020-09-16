using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int leftPlayerScore;
    public int rightPlayerScore;
    public string scoreBoard = String.Format("{0:00} | {1:00}", 0, 0);

    public int numberOfHits;
    public int numberOfLeftHits;
    public int numberOfRightHits;

    public int winStreak;
    public int rightPlayerWinStreak;
    public int leftPlayerWinStreak;
    public bool winStreakHolderIsRight;

    public double elapsedTimeInSeconds;
    public int elapsedTimeInMinutes;
    public string elapsedTimeAsString;

    public double roundStartTimeInSeconds;

    public double longestRoundInSeconds;
    public int longestRoundMinutes;
    public string longestRoundAsString;


    public GameObject HUD;
    Stopwatch stopwatch = new Stopwatch();


    // Start is called before the first frame update
    void Awake()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        elapsedTimeInSeconds = 0;
        stopwatch.Start();
        HUD.transform.Find("ScoreUI").GetComponent<Text>().text = scoreBoard;
        HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTimeInSeconds = stopwatch.Elapsed.Seconds;
        elapsedTimeInMinutes = stopwatch.Elapsed.Minutes;

        elapsedTimeAsString = String.Format("{0:00}:{1:00}", elapsedTimeInMinutes, elapsedTimeInSeconds % 60);
        
        HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;

        if (Input.GetKeyDown("i"))
        {
            HUD.transform.Find("BonusStats").gameObject.SetActive(!HUD.transform.Find("BonusStats").gameObject.activeSelf);
        }
    }

    public void updatePlayerScore(bool isRight)
    {
        if (isRight)
        {
            rightPlayerScore++;
            rightPlayerWinStreak++;
            leftPlayerWinStreak = 0;
            if (rightPlayerWinStreak > winStreak)
            {
                HUD.transform.Find("BonusStats").Find("WinStreak").GetComponent<Text>().text = "Highest Win Streak:\nRight Player, " +
                    rightPlayerWinStreak + " wins";
            }
        } else
        {
            leftPlayerScore++;
            leftPlayerWinStreak++;
            rightPlayerWinStreak = 0;
            if (leftPlayerWinStreak > winStreak)
            {
                HUD.transform.Find("BonusStats").Find("WinStreak").GetComponent<Text>().text = "Highest Win Streak:\nLeft Player, " +
                    leftPlayerWinStreak + " wins";
            }
        }
        scoreBoard = String.Format("{0:00} | {1:00}", leftPlayerScore, rightPlayerScore);


    }
}
