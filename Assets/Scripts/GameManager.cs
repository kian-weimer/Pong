using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Ball[] balls = new Ball[100];
    int ballCount = 1;
    public Paddle paddle;
    public PaddleCPU paddleCPU;

    bool ballStarted = false;
    public static bool beginGame;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    public static Stopwatch gameStartTimer = new Stopwatch();

    public GameObject HUD;



    // Sets up the game
    void Start()
    {
        beginGame = false;

        // if in a game scence initalizies the start messages
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            HUD.transform.Find("StartGame").gameObject.SetActive(true);
            HUD.transform.Find("StartTimer").gameObject.SetActive(false);
        }

        // sets up the screen dimensions
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        FindObjectOfType<AudioManager>().PlayIfNotPlaying("PongSoundtrack");

        //creates the starting ball
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

        // when a key is pressed it begins the game
        if (Input.anyKeyDown && !beginGame && SceneManager.GetActiveScene().name != "MainMenu")
        {
            FindObjectOfType<AudioManager>().Play("Countdown");
            HUD.transform.Find("StartGame").gameObject.SetActive(false);
            HUD.transform.Find("StartTimer").gameObject.SetActive(true);

            beginGame = true;
            gameStartTimer.Stop();
            gameStartTimer.Reset();
            gameStartTimer.Start();
        }

        // runs the ball timer and if the time is up it starts moving the ball and begins the stats
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
        else if (!ballStarted)
        {
            // if the game is in the main menu it just begins
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

    // resets the hud start timer text objects
    public void ResetHUD()
    {
        HUD.transform.Find("StartTimer").gameObject.SetActive(true);
    }

    // adds another ball to the game
    public void addBall(bool isCopy = false)
    {
        balls[ballCount] = Instantiate(ball);
        startBall(ballCount);
        ballCount++;
        if (isCopy)
        {
            balls[ballCount - 1].tag = "ballCopy";
        }
    }

    // starts moving the balls
    public void startBall(int index)
    {
        balls[index].StartMovingBall();
    }

    // Happens when one of the player wins
    public void endGame(bool isRight)
    {
        if (isRight)
        {
            HUD.transform.Find("EndGame").Find("EndGameMessage").GetComponent<Text>().text = "Game Over!\nRight Player Wins!";
        }
        else
        {
            HUD.transform.Find("EndGame").Find("EndGameMessage").GetComponent<Text>().text = "Game Over!\nLeft Player Wins!";
        }

        foreach (Ball b in balls)
        {
            if (b != null)
            {
                b.GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            }
        }
        HUD.transform.Find("EndGame").gameObject.SetActive(true);
    }
}
