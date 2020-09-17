using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    Ball[] balls = new Ball[100];
    int ballCount = 1;
    public Paddle paddle;
    public PaddleCPU paddleCPU;

    bool ballStarted = false;
    public static bool beginGame;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static Stopwatch gameStartTimer = new Stopwatch();

    public GameObject HUD;



    // Start is called before the first frame update
    void Start()
    {
        beginGame = false;
        HUD.transform.Find("StartGame").gameObject.SetActive(true);
        HUD.transform.Find("StartTimer").gameObject.SetActive(false);

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        FindObjectOfType<AudioManager>().PlayIfNotPlaying("PongSoundtrack");

        balls[0] = Instantiate(ball);

        if (SceneManager.GetActiveScene().name == "SoloGame")
        {
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            PaddleCPU paddle2 = Instantiate(paddleCPU) as PaddleCPU;
            paddle1.Init(true); // right paddle
            paddle2.Init(false); // left paddle
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            PaddleCPU paddle1 = Instantiate(paddleCPU) as PaddleCPU;
            PaddleCPU paddle2 = Instantiate(paddleCPU) as PaddleCPU;
            paddle1.Init(true); // right paddle
            paddle2.Init(false); // left paddle
        }
        else
        {
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            Paddle paddle2 = Instantiate(paddle) as Paddle;
            paddle1.Init(true); // right paddle
            paddle2.Init(false); // left paddle
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("k") && Input.GetKeyDown("i") && Input.GetKeyDown("r") && Input.GetKeyDown("y"))
        {
            addBall();
        }
        if (Input.anyKeyDown && !beginGame)
        {
            HUD.transform.Find("StartGame").gameObject.SetActive(false);
            HUD.transform.Find("StartTimer").gameObject.SetActive(true);
            beginGame = true;
            gameStartTimer.Stop();
            gameStartTimer.Reset();
            gameStartTimer.Start();
        }

        if (SceneManager.GetActiveScene().name != "MainMenu" && beginGame)
        {
            double ballTimerSeconds = gameStartTimer.Elapsed.Seconds;
            HUD.transform.Find("StartTimer").GetComponent<Text>().text = (3.0 - ballTimerSeconds).ToString();
            
            if (ballTimerSeconds == 4)
            {
                ballStarted = false;
                gameStartTimer.Stop();
                gameStartTimer.Reset();
                HUD.transform.Find("StartTimer").gameObject.SetActive(false);

                foreach (Ball b in balls)
                {
                    if (b != null)
                    {
                        b.StartMovingBall();
                    }
                }
            }
        }
        else if(!ballStarted)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                ballStarted = true;
                foreach (Ball b in balls)
                {
                    if (b != null)
                    {
                        b.StartMovingBall();
                    }
                }
            }
        }
    }

    public void ResetHUD()
    {
        HUD.transform.Find("StartTimer").gameObject.SetActive(true);
    }

    public void addBall()
    {
        balls[ballCount] = Instantiate(ball);
        startBall(ballCount);
        ballCount++;
    }

    public void startBall(int index)
    {
        balls[index].StartMovingBall();
    }

    public void endGame(bool isRight)
    {
        if (isRight)
        {
            HUD.transform.Find("EndGame").Find("EndGameMessage").GetComponent<Text>().text = "Game Over!\nRight Player Wins!";
        } else
        {
            HUD.transform.Find("EndGame").Find("EndGameMessage").GetComponent<Text>().text = "Game Over!\nLeft Player Wins!";
        }

        foreach (Ball b in balls)
        {
            if (b != null)
            {
                b.gameObject.SetActive(false);
            }
        }
        HUD.transform.Find("EndGame").gameObject.SetActive(true);
    }
}
