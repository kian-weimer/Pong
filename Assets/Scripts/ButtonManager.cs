using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void StartSolo()
    {
        SceneManager.LoadScene(sceneName: "SoloGame");
    }

    public void StartMultiplayer()
    {
        SceneManager.LoadScene(sceneName: "InGameScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
        GameManager.gameStartTimer.Stop();
        GameManager.gameStartTimer.Reset();
    }

    public void Rematch()
    {
        if (SceneManager.GetActiveScene().name == "SoloGame")
        {
            StartSolo();
        } else
        {
            StartMultiplayer();
        }
    }
}
