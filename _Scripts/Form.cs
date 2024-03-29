using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Linq;
using TMPro;

public class Form : MonoBehaviour
{
public string playerName;
    public GameObject loadPanel;
    public string nameURL = "https://twis.in/shop/test/api/text.php";
    public string registrationURL = "https://twis.in/shop/test/registration.php";
    public bool setUpComplete;
    public TMP_InputField inputField;
    public TMP_InputField ageInputField;
    public TMP_InputField wieghtInputField;
    public TMP_InputField heightInputField;
    public TMP_InputField otherInputField;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown diabetesDropdown;
    public TMP_Dropdown bpDropdown;
    public Button saveForm;


    private void Start()
    {
        setUpComplete = PlayerPrefs.GetInt("setUpComplete", 0) >= 1 ? true : false;
        GameManager.instance.OpenForm();
        playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        if (!setUpComplete)
        {
            Debug.Log(" setup panel");
        }
        else
        {
            Debug.Log("already setup");
            Debug.Log(playerName);

        }
    }
    public void SaveFormData()
    {
        PlayerPrefs.SetString("PlayerName", inputField.text);
        PlayerPrefs.SetString("PlayerAge", ageInputField.text);
        PlayerPrefs.SetString("PlayerWeight", wieghtInputField.text);
        PlayerPrefs.SetString("PlayerHeight", heightInputField.text);
        PlayerPrefs.SetString("PlayerGender", genderDropdown.captionText.text);
        PlayerPrefs.SetString("PlayerDiabetic", diabetesDropdown.captionText.text);
        PlayerPrefs.SetString("PlayerBP", bpDropdown.captionText.text);
        PlayerPrefs.SetString("PlayerOther", otherInputField.text);

        StartCoroutine(UploadDetails());

    }
    public void SetName()
    {
        PlayerPrefs.SetString("PlayerName", inputField.text);
    }


    // Start is called before the first frame update
    public void StartUploadName()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
       
       StartCoroutine(UploadName());
    
    }

    IEnumerator UploadName()
    {

        Loading.instance.EnableLoading();
        string name = string.Concat(playerName.Where(c => !char.IsWhiteSpace(c))).ToLower();
        WWWForm form = new WWWForm();
        form.AddField("textData", name);
        using (UnityWebRequest www = UnityWebRequest.Post(nameURL, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("name uploaded successfully: " + www.downloadHandler.text);
                playerName = name;
                Debug.Log(playerName);
                PlayerPrefs.SetInt("setUpComplete", 2);
            }
        }
                Loading.instance.DisableLoading();
    }

    IEnumerator UploadDetails()
    {
             Loading.instance.EnableLoading();
        playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        string name = string.Concat(playerName.Where(c => !char.IsWhiteSpace(c))).ToLower();
        WWWForm form = new WWWForm();
        form.AddField("textData", name); // Sending player name without whitespaces
        form.AddField("age", PlayerPrefs.GetString("PlayerAge", "18"));
        form.AddField("gender", PlayerPrefs.GetString("PlayerGender", "male"));
        form.AddField("diabetes", PlayerPrefs.GetString("PlayerDiabetes", "no"));
        form.AddField("bp", PlayerPrefs.GetString("PlayerBP", "no"));
        form.AddField("weight", PlayerPrefs.GetString("PlayerWeight", "0"));
        form.AddField("height", PlayerPrefs.GetString("PlayerHeight", "0"));
        form.AddField("other", PlayerPrefs.GetString("PlayerOther", "no"));

        using (UnityWebRequest www = UnityWebRequest.Post(registrationURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error uploading data: " + www.error);
            }
            else
            {
                Debug.Log("Data uploaded successfully");
                PlayerPrefs.SetInt("setUpComplete", 2);
            }
        }
        Loading.instance.DisableLoading();
        loadPanel.SetActive(false);
    }
}
