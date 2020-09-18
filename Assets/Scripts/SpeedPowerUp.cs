using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpeedPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float yCord = Random.Range(GameManager.bottomLeft.y*3f/4f, GameManager.topRight.y * 3f/4f);
        transform.position = new Vector2(0, yCord);
    }
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
