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
    Ball newBall;
    public Paddle paddle;
    public PaddleCPU paddleCPU;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static bool gameStarted = false;

    public static Stopwatch gameStartTimer = new Stopwatch();

    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        gameStartTimer.Start();

        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        FindObjectOfType<AudioManager>().Play("PongSoundtrack");

        newBall = Instantiate(ball);

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
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            double elapsedTimeInSeconds = gameStartTimer.Elapsed.Seconds;

            HUD.transform.Find("StartTimer").GetComponent<Text>().text = (3.0 - elapsedTimeInSeconds).ToString();
            if (elapsedTimeInSeconds == 3 && !gameStarted)
            {
                gameStarted = true;
                gameStartTimer.Stop();
                gameStartTimer.Reset();
                HUD.transform.Find("StartTimer").gameObject.SetActive(false);
                newBall.StartMovingBall();
            }
        }
    }

    public void ResetHUD()
    {
        HUD.transform.Find("StartTimer").gameObject.SetActive(true);
    }
}
