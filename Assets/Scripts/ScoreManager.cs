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
    public int numberOfRoundHits;
    public int numberOfLeftHits;
    public int numberOfRightHits;

    public int winStreak;
    public int rightPlayerWinStreak;
    public int leftPlayerWinStreak;
    public bool winStreakHolderIsRight;

    public double elapsedTimeInSeconds = 0;
    public int elapsedTimeInMinutes;
    public string elapsedTimeAsString;

    public double roundStartTimeInSeconds = 0;
    public int roundStartTimeMinutes = 0;

    public double longestRoundInSeconds;
    public string longestRoundAsString;

    public static bool gameOver = false;


    public GameObject HUD;
    Stopwatch stopwatch = new Stopwatch();


    // Start is called before the first frame update
    void Awake()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        elapsedTimeInSeconds = 0;
        HUD.transform.Find("ScoreUI").GetComponent<Text>().text = scoreBoard;
        HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.beginGame)
        {
            if (elapsedTimeInSeconds == 0)
            {
                stopwatch.Start();
            }
            elapsedTimeInSeconds = stopwatch.Elapsed.Seconds;
            elapsedTimeInMinutes = stopwatch.Elapsed.Minutes;

            elapsedTimeAsString = String.Format("{0:00}:{1:00}", elapsedTimeInMinutes, elapsedTimeInSeconds % 60);

            HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;

            if (Input.GetKeyDown("i"))
            {
                HUD.transform.Find("BonusStats").gameObject.SetActive(!HUD.transform.Find("BonusStats").gameObject.activeSelf);
                HUD.transform.Find("BonusStatsPrompt").gameObject.SetActive(!HUD.transform.Find("BonusStatsPrompt").gameObject.activeSelf);
            }
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
                winStreak = rightPlayerWinStreak;
                HUD.transform.Find("BonusStats").Find("WinStreak").GetComponent<Text>().text = "Highest Win Streak:\nRight Player, " +
                    rightPlayerWinStreak + " wins";
            }
            if (rightPlayerScore == 5)
            {
                gameOver = true;
                FindObjectOfType<GameManager>().endGame(true);
            }
        } else
        {
            leftPlayerScore++;
            leftPlayerWinStreak++;
            rightPlayerWinStreak = 0;
            if (leftPlayerWinStreak > winStreak)
            {
                winStreak = leftPlayerWinStreak;
                HUD.transform.Find("BonusStats").Find("WinStreak").GetComponent<Text>().text = "Highest Win Streak:\nLeft Player, " +
                    leftPlayerWinStreak + " wins";
            }
            if (leftPlayerScore == 5)
            {
                gameOver = true;
                FindObjectOfType<GameManager>().endGame(false);
            }
        }

        HUD.transform.Find("BonusStats").Find("RightPlayer").Find("WinStreak").GetComponent<Text>().text = "Streak: " + rightPlayerWinStreak + " wins";
        HUD.transform.Find("BonusStats").Find("LeftPlayer").Find("WinStreak").GetComponent<Text>().text = "Streak: " + leftPlayerWinStreak + " wins";

        scoreBoard = String.Format("{0:00} | {1:00}", leftPlayerScore, rightPlayerScore);
        HUD.transform.Find("ScoreUI").GetComponent<Text>().text = scoreBoard;

        numberOfRoundHits = 0;
        HUD.transform.Find("BonusStats").Find("RoundHitCounter").GetComponent<Text>().text = "Round Hits: " + numberOfRoundHits;

        double roundStartTime = roundStartTimeInSeconds;
        double roundEndTime = stopwatch.Elapsed.Seconds;
        double roundTimeSeconds = roundEndTime - roundStartTime;

        if (roundTimeSeconds > longestRoundInSeconds)
        {

            int roundTimeMinutes = stopwatch.Elapsed.Minutes - roundStartTimeMinutes;
            longestRoundInSeconds = roundTimeSeconds;
            longestRoundAsString = String.Format("{0:00}:{1:00}", roundTimeMinutes, roundTimeSeconds % 60);
            HUD.transform.Find("BonusStats").Find("LongestRound").GetComponent<Text>().text = "Longest Round: " + longestRoundAsString;
        }

        roundStartTimeInSeconds = stopwatch.Elapsed.Seconds;
    }

    public void increaseHitCount(bool isRight)
    {
        numberOfHits++;
        HUD.transform.Find("BonusStats").Find("TotalHitCounter").GetComponent<Text>().text = "Total Hits: " + numberOfHits;
        numberOfRoundHits++;
        HUD.transform.Find("BonusStats").Find("RoundHitCounter").GetComponent<Text>().text = "Round Hits: " + numberOfRoundHits;

        if (isRight)
        {
            numberOfRightHits++;
            HUD.transform.Find("BonusStats").Find("RightPlayer").Find("TotalHitCounter").GetComponent<Text>().text =
                "Total Hits: " + numberOfRightHits;
        }
        else
        {
            numberOfLeftHits++;
            HUD.transform.Find("BonusStats").Find("LeftPlayer").Find("TotalHitCounter").GetComponent<Text>().text =
                "Total Hits: " + numberOfLeftHits;
            ;
        }
    }

    public void Reset()
    {
        // leftPlayerScore = 0;
        // rightPlayerScore = 0;
        // scoreBoard = String.Format("{0:00} | {1:00}", 0, 0);
        // HUD.transform.Find("ScoreUI").GetComponent<Text>().text = scoreBoard;

        numberOfHits = 0;
        HUD.transform.Find("BonusStats").Find("TotalHitCounter").GetComponent<Text>().text = "Total Hits: " + numberOfHits;
        numberOfRoundHits = 0;
        HUD.transform.Find("BonusStats").Find("RoundHitCounter").GetComponent<Text>().text = "Round Hits: " + numberOfRoundHits;
        numberOfLeftHits = 0;
        HUD.transform.Find("BonusStats").Find("LeftPlayer").Find("TotalHitCounter").GetComponent<Text>().text =
                "Total Hits: " + numberOfLeftHits;
        numberOfRightHits = 0;
        HUD.transform.Find("BonusStats").Find("RightPlayer").Find("TotalHitCounter").GetComponent<Text>().text =
                "Total Hits: " + numberOfRightHits;

        winStreak = 0;
        HUD.transform.Find("BonusStats").Find("WinStreak").GetComponent<Text>().text = "Highest Win Streak:\n--------";
        rightPlayerWinStreak = 0;
        HUD.transform.Find("BonusStats").Find("RightPlayer").Find("WinStreak").GetComponent<Text>().text = "Streak: " + rightPlayerWinStreak + " wins";
        leftPlayerWinStreak = 0;
        HUD.transform.Find("BonusStats").Find("LeftPlayer").Find("WinStreak").GetComponent<Text>().text = "Streak: " + leftPlayerWinStreak + " wins";
        winStreakHolderIsRight = false;

        //elapsedTimeInSeconds = 0.0;
        //elapsedTimeInMinutes = 0;

        //elapsedTimeAsString = String.Format("{0:00}:{1:00}", elapsedTimeInMinutes, elapsedTimeInSeconds % 60);
        //HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;

        //roundStartTimeInSeconds = 0;
        //roundStartTimeMinutes = 0;

        longestRoundInSeconds = 0;
        longestRoundAsString = longestRoundAsString = String.Format("{0:00}:{1:00}", 0, 0);
        HUD.transform.Find("BonusStats").Find("LongestRound").GetComponent<Text>().text = "Longest Round: " + longestRoundAsString;
        //stopwatch.Reset();
    }
}
