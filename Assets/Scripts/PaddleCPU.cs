using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleCPU : MonoBehaviour
{
    [SerializeField]
    float speed;
    float height;

    public bool isRight;
    Ball ball; 
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
        ball = FindObjectOfType<Ball>();
    }
    
    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x; //  move bit to right

        }

        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x; // move bit to left

        }

        // Update this paddle's position
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;
        if (ball.transform.position.y > transform.position.y + offset)
        {
            move = 1 * Time.deltaTime * speed;
        }
        else if (ball.transform.position.y < transform.position.y - offset)
        {
             move = -1 * Time.deltaTime * speed;
        }
        // Restrict paddle movement
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }

        if (isRight)
        {
            if (ball.transform.position.x < GameManager.topRight.x / 2)
            {
                speed = 1;
            }
            else
            {
                speed = 7;
            }
        }

        if (!isRight)
        {
            if (ball.transform.position.x > GameManager.topRight.x / 2)
            {
                speed = 1.5f;
            }
            else
            {
                speed = 7;
            }
        }


        transform.Translate(move * Vector2.up);
    }
}