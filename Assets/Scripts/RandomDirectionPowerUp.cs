using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float yCord = Random.Range(GameManager.bottomLeft.y * 3f / 4f, GameManager.topRight.y * 3f / 4f);
        transform.position = new Vector2(0, yCord);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            int xDirection = Random.Range(0, 2) * -2 + 1;
            int yDirection = Random.Range(0, 2) * -2 + 1;
            FindObjectOfType<Ball>().direction = new Vector2(xDirection, yDirection);
            Destroy(gameObject);
        }
    }
}
