using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Linq;

public class Cameraa : MonoBehaviour
{
	public string phpServerEndpoint;
	bool isPicturePermission;
	public WebViewManageer webManager;
	public RawImage rawImage;
	byte[] bytes;
	public string imgpath;
	private async void RequestPermissionAsynchronously(bool isPicturePermission)
	{
		NativeCamera.Permission permission = await NativeCamera.RequestPermissionAsync(isPicturePermission);
		Debug.Log("Permission result: " + permission);
	}

	public void TakePicture(int maxSize = 512)
	{
		RequestPermissionAsynchronously(isPicturePermission);
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);	
			if (path != null)
			{
				// Create a Texture2D from the captured image
				string pathJpeg = Path.ChangeExtension(path, ".jpg");
				Debug.Log("Image path after jpeg: " + pathJpeg);
				byte[] data = System.IO.File.ReadAllBytes(pathJpeg);
				imgpath = path;
				bytes = data;
				if (data == null)
				{
					Debug.Log("Couldn't load data from " + pathJpeg);
					return;
				}

				UploadImage(data, pathJpeg);
				Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}
				rawImage.texture = texture;
		
			}
		}, maxSize);

		Debug.Log("Permission result: " + permission);
	}
	public void UploadImage(byte[] imageBytes, string path)
	{
		StartCoroutine(PostImage(imageBytes, path, phpServerEndpoint));
	}

	public void FetchPrompt()
    {
		SahilKiApi.instance.FetchData();
    }
	public void SetLoading()
    {

		Loading.instance.SetLoadingText("Fetching the analysis");
	}
	public void CloseWeb()
    {
		webManager.gameObject.SetActive(false);
		webManager.OpenUrl("google.com");
    }
	public void OpenWeb()
    {
		webManager.OpenUrl("https://food-kappa-teal.vercel.app");
	}
	IEnumerator PostImage(byte[] imageBytes, string path, string url)
	{
		Loading.instance.EnableLoading();
		Loading.instance.SetLoadingText("Uploading");
		WWWForm form = new WWWForm();
		form.AddBinaryData("file", imageBytes, Path.GetFileName(path), "image/jpeg");
				using (UnityWebRequest www = UnityWebRequest.Post(url, form))
		{
			Loading.instance.EnableLoading();
			yield return www.SendWebRequest();

			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Error uploading image: " + www.error);
			}
			else
			{
				Debug.Log("Image uploaded successfully");
				Debug.Log("Response: " + www.downloadHandler.text);
				Loading.instance.SetLoadingText("Image Uploaded");
				Invoke(nameof(OpenWeb), 10f);


			}
		}
		Invoke(nameof(SetLoading), 1.5f);
		Invoke(nameof(FetchPrompt), 7f);
		Invoke(nameof(CloseWeb), 20f);

	}

	public void UploadToProfile()
    {
		string playerName = PlayerPrefs.GetString("PlayerName");
		string name = string.Concat(playerName.Where(c => !char.IsWhiteSpace(c))).ToLower();
		string url = $"https://twis.in/shop/test/api/iitd/{name}/index.php";
		Debug.Log(url);
		StartCoroutine(PostImage(bytes, imgpath, url));
	}

}
