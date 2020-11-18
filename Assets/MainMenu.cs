﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayOffline()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // if (UnityEditor.EditorApplication.isPlaying)
        //     UnityEditor.EditorApplication.isPlaying = false;
        // else
            Application.Quit();
    }
}
