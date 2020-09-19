using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] powerUps = new GameObject[3];

    int timeToPowerup;
    int totalTime;

    // sets a random amount of time till a powerupo appears and also sets the total elapsed time to be 0
    void Start()
    {
        timeToPowerup = Random.Range(10, 30);
        totalTime = 0;
    }

    // if the game has reached the next time to powerup it will spawn a new random powerup out of a selection of 3
    void Update()
    {
        if (totalTime + timeToPowerup > 60)
        {
            totalTime -= 60;
        }


        if ((int)FindObjectOfType<ScoreManager>().elapsedTimeInSeconds == timeToPowerup + totalTime)
        {
            if(totalTime + timeToPowerup > 60)
            {
                totalTime += timeToPowerup;
            }
            else
            {
                totalTime += timeToPowerup;
            }

            timeToPowerup = Random.Range(10, 30);
            Instantiate(powerUps[Random.Range(0, 3)]);
        }
    }
}
