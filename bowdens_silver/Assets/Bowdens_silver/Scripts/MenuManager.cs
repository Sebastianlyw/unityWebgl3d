using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        Console.WriteLine("Loading main scene...");
        SceneManager.LoadSceneAsync("MainScene"); 
    }
}
