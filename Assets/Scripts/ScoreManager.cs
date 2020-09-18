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


    // begins the stats and sets up the hud
    void Awake()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        elapsedTimeInSeconds = 0;
        HUD.transform.Find("ScoreUI").GetComponent<Text>().text = scoreBoard;
        HUD.transform.Find("Time").GetComponent<Text>().text = elapsedTimeAsString;
    }

    // keeps time witht he stop watch
    // if the user presses i the game displays the extra stats
    void Update()
    {
        if (GameManager.beginGame)
        {
            if (elapsedTimeInSeconds == 0)
            {
                stopwatch.Start();
                gameOver = false;
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

    // updates the players score when a ball reaches the end of the screen is called in the ball class
    // if a player reaches 5 it ends the game
    public void updatePlayerScore(bool isRight)
    {
        //updates the right players score
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
        } 

        //updates the left players score
        else
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

        //updates the HUD and resets round stats
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

    // increases the number of hits in the hud
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

    // clears out the stats to the inital values and updates the HUD
    public void Reset()
    {
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

        longestRoundInSeconds = 0;
        longestRoundAsString = longestRoundAsString = String.Format("{0:00}:{1:00}", 0, 0);
        HUD.transform.Find("BonusStats").Find("LongestRound").GetComponent<Text>().text = "Longest Round: " + longestRoundAsString;
    }
}
