using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(800, 720, false);    
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Room");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
