using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject UIHandler;
    private UIHandler UIHandlerScript;

    private void Start()
    {
        UIHandlerScript = UIHandler.GetComponent<UIHandler>();
        isPaused = false;
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0; 
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; 
    }

    public void GameOver()
    {
        PauseGame(); 
        UIHandlerScript.GameOver();
    }
}