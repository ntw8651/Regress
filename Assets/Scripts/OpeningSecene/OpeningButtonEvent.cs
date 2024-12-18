using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningButtonEvent : MonoBehaviour
{
    // Function to load a specific scene
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Function to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}