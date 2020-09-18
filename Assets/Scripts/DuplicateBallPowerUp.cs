using UnityEngine;

public class DuplicateBallPowerUp : MonoBehaviour
{
    // Places the powerup in a ranom Y position along the y axis x remains at home 0
    void Start()
    {
        float yCord = Random.Range(GameManager.bottomLeft.y * 3f / 4f, GameManager.topRight.y * 3f / 4f);
        transform.position = new Vector2(0, yCord);
    }

    //adds another ball into the game
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            FindObjectOfType<AudioManager>().Play("PowerUp");
            FindObjectOfType<GameManager>().addBall(true);
            Destroy(gameObject);
        }
    }
}
