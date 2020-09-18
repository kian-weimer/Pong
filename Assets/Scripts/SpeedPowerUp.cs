using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpeedPowerUp : MonoBehaviour
{
    // Places the powerup in a ranom Y position along the y axis x remains at home 0 
    void Start()
    {
        float yCord = 0;
        while (yCord >= GameManager.topRight.y / 10 || yCord <= GameManager.bottomLeft.y / 10)
        {
            yCord = Random.Range(GameManager.bottomLeft.y * 3f / 4f, GameManager.topRight.y * 3f / 4f);
        }
        transform.position = new Vector2(0, yCord);
    }

    //if the powerup is collided with a ball it speeds up the ball and destroys itself
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            FindObjectOfType<Ball>().speed = FindObjectOfType<Ball>().speed * 2;
            Destroy(gameObject);
        }
    }
}
