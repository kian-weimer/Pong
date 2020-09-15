using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;
    public PaddleCPU paddleCPU;

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        FindObjectOfType<AudioManager>().Play("PongSoundtrack");

        Instantiate(ball);

        if(SceneManager.GetActiveScene().name == "SoloGame")
        {
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            PaddleCPU paddle2 = Instantiate(paddleCPU) as PaddleCPU;
            paddle1.Init(true); // right paddle
            paddle2.Init(); // left paddle
        }
        else
        {
            Paddle paddle1 = Instantiate(paddle) as Paddle;
            Paddle paddle2 = Instantiate(paddle) as Paddle;
            paddle1.Init(true); // right paddle
            paddle2.Init(false); // left paddle
        }
    }

    public void test()
    {
        Debug.Log("yes!");
    }
}
