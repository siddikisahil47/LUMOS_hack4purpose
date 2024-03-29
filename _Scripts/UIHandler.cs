using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject PanelScore;
    public GameObject PanelGamePlay;
    public ScriptManagement scriptManagement;
    public TextMeshProUGUI scoreText;

    public leaderboard lb;
    private void Start()
    {
        PanelScore.SetActive(false);
        StartCoroutine( lb.SubmitScore(scriptManagement.score));
    }

    public void backMainButton()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void GameOver()
    {
        scoreText.text = scriptManagement.Score.ToString();
        PanelGamePlay.SetActive(false);
        PanelScore.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        StartCoroutine(lb.SubmitScore(scriptManagement.score));
    }
}

