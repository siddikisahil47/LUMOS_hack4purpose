using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
public class SahilKiApi : MonoBehaviour
{
    public string apiUrl;
    public TMP_Text promptText;
    public GameObject promptBox;
    public Button camButton;
    public static SahilKiApi instance;
    private void Awake()
    {
        instance = this;
    }
    [System.Serializable]
    public class TextData
    {
        public string text;
    }
    private void OnEnable()
    {
        promptBox.SetActive(false);
    }
    public void FetchData()
    {
        StartCoroutine(FetchJSON());
    }
    IEnumerator FetchJSON()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to fetch JSON: " + www.error);
            }
            else
            {
                string jsonString = www.downloadHandler.text;
                ParseJSON(jsonString);
            }
        }
    }
    void ParseJSON(string json)
    {
        promptBox.SetActive(true);
        List<TextData> textDataList = JsonConvert.DeserializeObject<List<TextData>>(json);

        // Iterate through the list to fetch and print the text data
        foreach (TextData data in textDataList)
        {
            Debug.Log("Text: " + data.text);
            promptText.text = data.text;
        }
            DisableLoad();
        
    }

    void DisableLoad()
    {
      
        Loading.instance.DisableLoading();
    }
}
