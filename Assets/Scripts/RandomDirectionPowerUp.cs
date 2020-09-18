using UnityEngine;

public class RandomDirectionPowerUp : MonoBehaviour
{
    // Places the powerup in a ranom Y position along the y axis x remains at home 0
    void Start()
    {
        float yCord = Random.Range(GameManager.bottomLeft.y * 3f / 4f, GameManager.topRight.y * 3f / 4f);
        transform.position = new Vector2(0, yCord);
    }

    //when this object is hit it randomly changes the direction of the ball
    //The ball cannot go in the same direction it was going
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            int xDirection = Random.Range(0, 2) * -2 + 1;
            int yDirection = Random.Range(0, 2) * -2 + 1;

            if (xDirection == (int)FindObjectOfType<Ball>().direction.x && yDirection == (int)FindObjectOfType<Ball>().direction.y)
            {
                if (Random.Range(0, 2) == 1)
                {
                    xDirection = (int)FindObjectOfType<Ball>().direction.x * -1;
                }
                else
                {
                    yDirection = (int)FindObjectOfType<Ball>().direction.y * -1;
                }
            }

            FindObjectOfType<Ball>().direction = new Vector2(xDirection, yDirection);

            //after it changes the direction of the ball it deletes itself
            Destroy(gameObject);
        }
    }
}
