using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public GameObject loadingPrefab;
    public static Loading instance;
    public TMPro.TMP_Text text;
    private void Awake()
    {
        instance = this;
    }
    public void EnableLoading()
    {
        SetLoadingText("Loading");
        loadingPrefab.SetActive(true);
    }    
    public void DisableLoading()
    {
        loadingPrefab.SetActive(false);
    }
    public void SetLoadingText(string text)
    {
        this.text.text = text;
    }
}
