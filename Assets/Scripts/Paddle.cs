using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed = 0f;
    float height;

    string input;
    public bool isRight;

    //sets the height of the paddle
    void Start()
    {
        height = transform.localScale.y;
    }

    //creates the paddle
    public void Init(bool isRightPaddle)
    {

        isRight = isRightPaddle;
        Vector2 pos = Vector2.zero;

        //places the paddle on the right side
        if (isRightPaddle)
        {
            pos = new Vector2(GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x; //  move bit to right

            input = "PaddleRight";
        }

        //places the paddle on the left side
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


    // Moves the paddle as you press the keys
    void Update()
    {
        //the input is ties to w and s for left paddle and the right paddle is tied to the up and down arrow
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

        // Restrict paddle movement
        if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
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
