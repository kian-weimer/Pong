﻿using System.Collections;
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
}
