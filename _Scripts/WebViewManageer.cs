using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuplex.WebView;
public class WebViewManageer : MonoBehaviour
{
    // Start is called before the first frame update
    public string profileUrl = "https://twis.in/shop/test/api/iitd/";
    public CanvasWebViewPrefab canvasWebView;
    public async void OpenProfile()
    {
        canvasWebView.gameObject.SetActive(true);
        string playerName = PlayerPrefs.GetString("PlayerName");
        string name = string.Concat(playerName.Where(c => !char.IsWhiteSpace(c))).ToLower();
        await canvasWebView.WaitUntilInitialized();
        canvasWebView.WebView.LoadUrl(profileUrl + name);
    }
    public async void OpenUrl(string url)
    {
        canvasWebView.gameObject.SetActive(true);
        await canvasWebView.WaitUntilInitialized();
        canvasWebView.WebView.LoadUrl(url);
    }


}
