using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    float speed;

    float radius;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        int yDirection = Random.Range(0, 1) * -2 + 1;
        int xDirection = Random.Range(0, 1) * -2 + 1;
        direction = new Vector2(xDirection, yDirection);
        radius = transform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
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
        }
        if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Left Player Won!!");
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Paddle")
        {
            FindObjectOfType<AudioManager>().Play("PaddleHit");
            bool isRight = other.GetComponent<Paddle>().isRight;
            if(speed < 10)
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
            if (speed < 10)
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
