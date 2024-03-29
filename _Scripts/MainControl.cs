using UnityEngine.SceneManagement;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject creaditPanel;

    private void Start()
    {
        creaditPanel.SetActive(false);

    }
    public void playButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void creaditButton()
    {
        mainPanel.SetActive(false);
        creaditPanel.SetActive(true);
    }

    public void backButton()
    {
        creaditPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void exitbutton()
    {
        Application.Quit();
    }

}
