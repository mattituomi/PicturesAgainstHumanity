using UnityEngine;
using System.Collections;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.upload;
public class CloudImageManager : MonoBehaviour {
    UploadService uploadService;
    // Use this for initialization
    void Start () {
        App42Log.SetDebug(true);        //Prints output in your editor console  
        uploadService = App42API.BuildUploadService();
        Common.cloudImageManager = this;

    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            string filePath = System.IO.Path.GetFullPath(filePathTest);
            string filePath1 = System.IO.Path.GetFullPath(filePathTest1);
            string filePath2 = System.IO.Path.GetFullPath(filePathTest2);
            UploadFileToCloud("a","AAA",filePath);

        }
        */
	}
    public void UploadFileToCloud(string fileName,string userName,string filePath)
    {
        String fileType = UploadFileType.IMAGE;

        //Debug.Log(filePath);
        //filePath = Common.cameraMaster.path;
        uploadService.UploadFileForUser(fileName,"Matti", filePath, fileType, description, new uploadFileCallBack());
    }

    public void GetFile()
    {

        uploadService.GetFileByUser(filenNameTest,"Matti", new UnityCallBack2());
    }

    String filenNameTest = "image2.jpg";
    String description = "File Description";
    String filePathTest = "Assets//Resources//1.png";//"C:\\Users\\Matti\\Documents\\HoorayProto\\Pictures against humanity\\Assets\\Resources";
    String filePathTest1 = "Assets//Resources//2.png";
    String filePathTest2 = "Assets//Resources//3.png";
    public string callBackToGetFile;
    public string callBackToUploadFile;
    public GameObject cbgetFileGO;
    public GameObject cbuploadFileGO;

    public class uploadFileCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Upload upload = (Upload)response;
            IList<Upload.File> fileList = upload.GetFileList();
            for (int i = 0; i < fileList.Count; i++)
            {
                App42Log.Console("fileName is " + fileList[i].GetName());
                App42Log.Console("fileType is " + fileList[i].GetType());
                App42Log.Console("fileUrl is " + fileList[i].GetUrl());
                App42Log.Console("TinyUrl Is  : " + fileList[i].GetTinyUrl());
                App42Log.Console("fileDescription is " + fileList[i].GetDescription());
                Common.cloudImageManager.cbuploadFileGO.SendMessage(Common.cloudImageManager.callBackToUploadFile, fileList[i].GetUrl());//cbuploadFileGO.SendMessage(callbBackToGetFile, fileList[i].GetUrl());
            }
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
            Common.cloudImageManager.cbuploadFileGO.SendMessage(Common.cloudImageManager.callBackToGetFile, "ERROR"+e.ToString());
        }
    }
 
public class UnityCallBack2 : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Upload upload = (Upload)response;
            IList<Upload.File> fileList = upload.GetFileList();
            for (int i = 0; i < fileList.Count; i++)
            {
                App42Log.Console("fileName is " + fileList[i].GetName());
                App42Log.Console("fileType is " + fileList[i].GetType());
                App42Log.Console("fileUrl is " + fileList[i].GetUrl());
                App42Log.Console("TinyUrl Is  : " + fileList[i].GetTinyUrl());
                App42Log.Console("fileDescription is " + fileList[i].GetDescription());
                Common.cloudImageManager.cbgetFileGO.SendMessage(Common.cloudImageManager.callBackToUploadFile, fileList[i].GetUrl());
            }
        }

        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
            Common.cloudImageManager.cbgetFileGO.SendMessage(Common.cloudImageManager.callBackToUploadFile, "ERROR"+e.ToString());
        }
    }
}
