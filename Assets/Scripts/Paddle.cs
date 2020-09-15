using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed;
    float height;

    string input;
    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        height = transform.localScale.y;
    }

    public void Init(bool isRightPaddle)
    {

        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x; //  move bit to right

            input = "PaddleRight";
        }

        else
        {
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x; // move bit to left

            input = "PaddleLeft";
        }

        // Update this paddle's position
        transform.position = pos;

        transform.name = input;
    }


    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // Restrict paddle movement
        if(transform.position.y < GameManager.bottomLeft.y + height/2 && move < 0)
        {
            move = 0;
        }
        if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }
        transform.Translate(move * Vector2.up);
    }
}
