using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed;
    float radius;

    public Vector2 direction = new Vector2(0, 0);

    //sets up the balls radius starts moving in a later method.
    void Start()
    {
        radius = transform.localScale.x / 2;
        if(gameObject.tag == "ballCopy")
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }  
    }

    //when the ball is out of bounds on either the left or right side
    public void Reset()
    {
        //if the ball is a copy it will delete itself
        if (gameObject.tag == "ballCopy")
        {
            Destroy(gameObject);
        }

        //if the ball isn't a copy starts a new countdown and resets the ball to the middle of the screen
        else
        {
            if (!ScoreManager.gameOver)
            {
                FindObjectOfType<AudioManager>().Play("Countdown");
            }

            transform.position = new Vector2(0, 0);
            transform.Translate(new Vector2(0, 0));
            direction = new Vector2(0, 0);
            speed = 5;
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<GameManager>().ResetHUD();
            }
            GameManager.gameStartTimer.Start();
        }

    }

    //chooses a random direction to sdtart the ball going in
    public void StartMovingBall()
    {
        if(gameObject.tag != "ballCopy")
        {
            direction.x = Random.Range(0, 2) * -2 + 1;
            direction.y = Random.Range(0, 2) * -2 + 1;
        }
    }

    //moves the ball every frame and detects if it hits the wall on either the top or bottom of the screen
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            FindObjectOfType<AudioManager>().Play("WallHit");
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            FindObjectOfType<AudioManager>().Play("WallHit");
            direction.y = -direction.y;
        }

        // Game Over
        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)

        {
            FindObjectOfType<AudioManager>().Play("Goal");
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<ScoreManager>().updatePlayerScore(true);
            }
            FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", true);
            Reset();
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            FindObjectOfType<AudioManager>().Play("Goal");
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<ScoreManager>().updatePlayerScore(false);
            }
            FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", true);
            Reset();
        }
    }

    //used to detect collions with the paddles
    //the ball will speed up on each hit as long as the scene is not main menu
    void OnTriggerEnter2D(Collider2D other)
    {

        //if it hits a player paddle
        if (other.tag == "Paddle")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<Paddle>().isRight;
            if (speed < 10 && SceneManager.GetActiveScene().name != "MainMenu")
            {
                speed = speed * 1.1f;
                FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", false);
            }

            if (isRight && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            if (!isRight && direction.x < 0)
            {
                direction.x = -direction.x;
            }

            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<ScoreManager>().increaseHitCount(isRight);
            }
        }

        //if it hits a computer paddle
        if (other.tag == "PaddleCPU")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<PaddleCPU>().isRight;
            if (speed < 10 && SceneManager.GetActiveScene().name != "MainMenu")
            {
                speed = speed * 1.1f;
                FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", false);
            }

            if (isRight && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            if (!isRight && direction.x < 0)
            {
                direction.x = -direction.x;
            }

            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<ScoreManager>().increaseHitCount(isRight);
            }
        }
    }
}
