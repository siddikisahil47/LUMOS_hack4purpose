using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameInfo : MonoBehaviour
{
    public GameItemSo so;
    public TMP_Text gameName;
    public TMP_Text steps;
    public TMP_Text currentStepstext;
    public Button unlockButton;
    public Button gameButton;
    public long currentSteps;
    public WebViewManageer web;
    private void Start()
    {
        currentSteps = (long)Pedometer.instance.steps;
        if (so.unlocked) 
        { unlockButton.gameObject.SetActive(false); 
            gameButton.onClick.AddListener(LoadGame);
        }
        else
        {
            gameButton.onClick.AddListener(() => Debug.Log("no"));
        }
        gameName.text = so.gameName;
        steps.text = so.stepsToUnlock.ToString();
        currentStepstext.text = currentSteps.ToString();
        unlockButton.onClick.AddListener(UnlockGame);
        
    }
    public void LoadGame()
    {
        web.OpenUrl(so.url);
    }
    public void UnlockGame()
    {

        if(so.stepsToUnlock > currentSteps)
        {
            Debug.Log("Locked");
        }
        else
        {
            Debug.Log("Unlocked");
            so.unlocked = true;
            unlockButton.gameObject.SetActive(false);
            gameButton.onClick.AddListener(LoadGame);
        }
    }   
}
