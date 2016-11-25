using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Common.cameraMaster = this;
    }
    public string EditorTestLocalImagePath = "Assets//Resources//image3.jpg";
    public void GetImageFromCamera()
    {
        #if UNITY_EDITOR
                Debug.Log("Unity Editor");
        Common.playerInformation.LocalImagePicked(EditorTestLocalImagePath);
        #endif
        Debug.Log("Getting image from camera");
        AndroidCamera.Instance.OnImagePicked += OnImagePicked;
        AndroidCamera.Instance.GetImageFromCamera();
    }
    public string path;
    public void GetImageFromGallery()
    {
        #if UNITY_EDITOR
                Debug.Log("Unity Editor");
                Common.playerInformation.LocalImagePicked(EditorTestLocalImagePath);
        #endif
        Debug.Log("Getting image from gallery");
        AndroidCamera.Instance.OnImagePicked += OnImagePicked;
        AndroidCamera.Instance.GetImageFromGallery();
    }

    private void OnImagePicked(AndroidImagePickResult result)
    {
        Debug.Log("OnImagePicked");
        if (result.IsSucceeded)
        {
            //  AN_PoupsProxy.showMessage("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
            Common.DebugPopUp("CameraMaster fun OnImagePicked", "Succeeded, path: " + result.ImagePath);
            
            // Common.infoGUI.SetImage(result.Image);//.GetComponent<Renderer>().material.mainTexture = result.Image;
            Common.playerInformation.SetMyImage(result.Image);
            path = result.ImagePath;
            Common.playerInformation.LocalImagePicked(path);

        }
        else {
            Common.DebugPopUp("CameraMaster fun OnImagePicked", "Image pick result failed");
            //  AN_PoupsProxy.showMessage("Image Pick Rsult", "Failed");
        }

        AndroidCamera.Instance.OnImagePicked -= OnImagePicked;
    }



    // Update is called once per frame
    void Update () {
	
	}

    public void SaveScreenShotToGallery()
    {
        Debug.Log("Getting screenshot");
        AndroidCamera.Instance.SaveScreenshotToGallery();
    }



/*
    void OnImageSaved(GallerySaveResult result)
    {
        AndroidCamera.Instance.OnImageSaved -= OnImageSaved;
        if (result.IsSucceeded)
        {
            Debug.Log("Image saved to gallery \n" + "Path: " + result.imagePath);
        }
        else {
            Debug.Log("Image save to gallery failed");
        }
    }
    //int OnImagePicked;
    private void OnImagePicked(AndroidImagePickResult result)
    {
        Debug.Log("OnImagePicked");
        if (result.IsSucceeded)
        {
            AN_PoupsProxy.showMessage("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
            image.GetComponent<Renderer>().material.mainTexture = result.Image;
        }
        else {
            AN_PoupsProxy.showMessage("Image Pick Rsult", "Failed");
        }

        AndroidCamera.instance.OnImagePicked -= OnImagePicked;
    }

    void OnImageSaved(GallerySaveResult result)
    {
        AndroidCamera.instance.OnImageSaved -= OnImageSaved;
        if (result.IsSucceeded)
        {
            Debug.Log("Image saved to gallery \n" + "Path: " + result.imagePath);
        }
        else {
            Debug.Log("Image save to gallery failed");
        }
    }

    AndroidCamera.instance.OnImageSaved += OnImageSaved;
AndroidCamera.instance.SaveImageToGalalry(helloWorldTexture);


    */



}
