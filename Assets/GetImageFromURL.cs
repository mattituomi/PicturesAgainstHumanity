using UnityEngine;
using System.Collections;

public class GetImageFromURL : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Common.getImageFromURL = this; ;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
           // string url = "https://... Your url (Internet url)";
           // StartCoroutine(DownloadImg(url));
        }
	}
    public Texture2D downloadedImage;
    public string url;
    public bool loadingImage = false;
    public void loadImage(string url,GameObject toNotify,string function)
    {
        StartCoroutine(DownloadImg(url, toNotify, function));
    }




    IEnumerator DownloadImg(string URL,GameObject toNotify,string function)
    {
        Debug.Log("Loading image " + URL);
        
        AN_PoupsProxy.showMessage("Starting to load", "Succeeded, path: " + URL);
        loadingImage = true;

        GameObject loading = GameObject.FindWithTag("LoadingPopUp");
        loading.transform.GetChild(0).gameObject.SetActive(true);


        Texture2D texture = new Texture2D(1, 1);
        WWW www = new WWW(URL);
        yield return www;
        www.LoadImageIntoTexture(texture);
        downloadedImage = texture;
        url = URL;
        loadingImage = false;
        loading.transform.GetChild(0).gameObject.SetActive(false);

        toNotify.SendMessage(function, texture);
        //  Common.infoGUI.pickResult.color = Color.white;
    }
}
