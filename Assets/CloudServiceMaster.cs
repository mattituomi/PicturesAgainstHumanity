using UnityEngine;
using System.Collections;

public class CloudServiceMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Common.cloudServiceMaster=this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    public void AddPhotoToGalleryWithCallBackDelegate()
    {
        AddPhotoToGallery();
       // Common.galleryManager.updateDelegates += Common.playerInformation.PhotoLoadedSuccesfullyDelegate;
    }
    */

    public void PhotoAddWasSuccesfull()
    {
        Common.playerInformation.PhotoLoadedSuccesfull();
    }

    public void AddPhotoToGallery()
    {
        // cloudImageManagerToUse.UploadFileToCloud(fileName, "matti", filePath);
        PlayerInformation pi = Common.playerInformation;
        GameDataGooglePlay gameData = Common.roundInformation.gameData;
        Common.galleryManager.AddPhoto(gameData.hostID, gameData.matchID, GetPhotoName(pi),Common.gameMaster.currenctGuestion,pi.myLocalImagePath);
       

    }

    public string GetPhotoName(PlayerInformation pi)
    {
        return pi.myID + "Round" + Common.roundInformation.gameData.round.ToString();
    }

    public void LoadImageFromGallery(string filePath, string fileName)
    {
        //cloudImageManagerToUse.UploadFileToCloud(fileName, "matti", filePath);
    }

    public void CreateAlbumOnGameStarted()
    {
        GameDataGooglePlay gameData = Common.roundInformation.gameData;
        Common.galleryManager.CreateAlbum(gameData.hostID, gameData.matchID);
    }




    public void ImageSaved(string URL)
    {
        Debug.Log("IMAGE SAVED "+URL);
       // StartCoroutine(DownloadImg(URL));
    }

    public void ImageLoaded(string URL)
    {
        Debug.Log("IMAGE LOADED " + URL);
    }
    public void ImagePicked(string URL)
    {
        Debug.Log("ImagePicked");
    }

    Gallery galleryToUse;

    public Texture2D downloadedImage;
public string url;
    public bool loadingImage = false;
IEnumerator DownloadImg(string URL)
{
        loadingImage = true;
    Texture2D texture = new Texture2D(1, 1);
    WWW www = new WWW(URL);
    yield return www;
    www.LoadImageIntoTexture(texture);
        downloadedImage = texture;
        url = URL;
        loadingImage = false;
      //  Common.infoGUI.pickResult.color = Color.white;
}
}
