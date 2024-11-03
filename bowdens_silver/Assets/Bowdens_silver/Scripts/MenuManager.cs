using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        Console.WriteLine("Loading main scene...");
        // Load the main scene asynchronously to avoid freezing
        SceneManager.LoadSceneAsync("MainScene"); // replace "MainScene" with the actual name of your main scene
    }
}
