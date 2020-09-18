using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //main menu buttons
    public void StartSolo()
    {
        SceneManager.LoadScene(sceneName: "SoloGame");
    }
    public void StartMultiplayer()
    {
        SceneManager.LoadScene(sceneName: "InGameScene");
    }

    //return button
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
        FindObjectOfType<AudioManager>().Stop("Countdown");
        GameManager.gameStartTimer.Stop();
        GameManager.gameStartTimer.Reset();
    }

    //restarts the game
    public void Rematch()
    {
        FindObjectOfType<AudioManager>().Stop("Countdown");
        if (SceneManager.GetActiveScene().name == "SoloGame")
        {
            StartSolo();
        }
        else
        {
            StartMultiplayer();
        }
    }

    //resets the stats
    public void resetStats()
    {
        FindObjectOfType<ScoreManager>().Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}