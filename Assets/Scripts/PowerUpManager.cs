using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] powerUps;

    int timeToPowerup;
    int totalTime;
    // Start is called before the first frame update
    void Start()
    {
        timeToPowerup = Random.Range(10, 20);
        timeToPowerup = 5;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<ScoreManager>().elapsedTimeInSeconds == timeToPowerup + totalTime)
        {
            totalTime += timeToPowerup;
            timeToPowerup = Random.Range(10, 20);
            timeToPowerup = 5;
            Instantiate(powerUps[1]);
            //Instantiate(powerUps[Random.Range(0, 3)]);
        }
    }
}
