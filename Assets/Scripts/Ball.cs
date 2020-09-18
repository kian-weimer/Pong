using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.PackageManager.Requests;

public class Ball : MonoBehaviour
{
    //current speed of the ball
    public float speed;

    float radius;
    public Vector2 direction = new Vector2(0,0);

    Vector2 previousDirection = new Vector2(0, 0);

    void Start()
    {
        radius = transform.localScale.x / 2;
    }

    public void Reset()
    {
        if (gameObject.tag == "ballCopy")
        {
            Destroy(gameObject);
        }
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

    public void StartMovingBall()
    {
        direction.x = Random.Range(0, 2) * -2 + 1;
        direction.y = Random.Range(0, 2) * -2 + 1;
        previousDirection = direction;
    }

    // Update is called once per frame
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
            Debug.Log("Right Player Won!!");
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
            Debug.Log("Left Player Won!!");
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                FindObjectOfType<ScoreManager>().updatePlayerScore(false);
            }
            FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", true);
            Reset();
        }

        previousDirection = direction;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Paddle")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<Paddle>().isRight;
            if(speed < 10 && SceneManager.GetActiveScene().name != "MainMenu")
            {
                speed = speed * 1.1f;
                FindObjectOfType<AudioManager>().changePitch("PongSoundtrack", false);
            }

            if(isRight && direction.x > 0)
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
        previousDirection = direction;
    }

    public void stop()
    {
        direction = new Vector2(0, 0);
    }
}
