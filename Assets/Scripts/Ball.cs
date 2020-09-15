using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.PackageManager.Requests;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction = new Vector2(0,0);

    void Start()
    {
        radius = transform.localScale.x / 2;
    }

    public void Reset()
    {
        transform.position = new Vector2(0, 0);
        transform.Translate(new Vector2(0, 0));
        direction = new Vector2(0, 0);
        speed = 5;
        GameManager.gameStarted = false;
        GameManager.gameStartTimer.Start();
    }

    public void StartMovingBall()
    {
        direction.x = Random.Range(0, 2) * -2 + 1;
        direction.y = Random.Range(0, 2) * -2 + 1;
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
            Debug.Log("Right Player Won!!");
            Reset();
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left Player Won!!");
            Reset();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Paddle")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<Paddle>().isRight;
            if(speed < 10 && SceneManager.GetActiveScene().name != "MainMenu")
            {
                speed = speed * 1.1f;
            }

            if(isRight && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            if (!isRight && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }

        if (other.tag == "PaddleCPU")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<PaddleCPU>().isRight;
            if (speed < 10 && SceneManager.GetActiveScene().name != "MainMenu")
            {
                speed = speed * 1.1f;
            }

            if (isRight && direction.x > 0)
            {
                direction.x = -direction.x;
            }

            if (!isRight && direction.x < 0)
            {
                direction.x = -direction.x;
            }
        }
    }
}
