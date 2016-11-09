using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.gallery;
using System;
using System.Collections.Generic;

public class Gallery : MonoBehaviour {
    AlbumService albumService;
    PhotoService photoService;
    // Use this for initialization
    void Start () {
       // App42API.Initialize("0f8d9be1cad28cf2dd9b67c33555635f29f26992723be154a70e49083a68f952", "960640b896bf9ff26c6abbd7c854fc0dfc08ef65f56e98f5d7227015e662957b");
        albumService = App42API.BuildAlbumService();
        photoService = App42API.BuildPhotoService();
        Common.galleryManager=this;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //CreateAlbum();
        }
	}

    public void CreateAlbum(string userName,string albumName)
    {
        App42Log.SetDebug(true);
        albumService.CreateAlbum(userName, albumName, description, new UnityCallBack());
        
    }

    public void AddPhoto(string userName,string albumName,string photoName,string description,string filePath)
    {
        photoService.AddPhoto(userName, albumName, photoName+"new4", description, filePath, new AddPhotoCallback());

    }



    public class AddPhotoCallback : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Album album = (Album)response;
            App42Log.Console("userName is :" + album.GetUserName());
            App42Log.Console("albumName is :" + album.GetName());
            App42Log.Console("Description is :" + album.GetDescription());
            App42Log.Console("jsonResponse is :" + album.ToString());
            IList<Album.Photo> photoList = album.GetPhotoList(); ;
            for (int i = 0; i < photoList.Count; i++)
            {
                App42Log.Console("PhotoName is :" + photoList[i].GetName());
                App42Log.Console("Url is :" + photoList[i].GetUrl());
                App42Log.Console("TinyUrl is :" + photoList[i].GetTinyUrl());
                App42Log.Console("ThumbNailUrl is :" + photoList[i].GetThumbNailUrl());
                App42Log.Console("ThumbNailTinyUrl is :" + photoList[i].GetThumbNailTinyUrl());
                App42Log.Console("jsonResponse is :" + photoList[i].ToString());
                Common.playerInformation.SetURL(photoList[i].GetUrl(),0);
            }
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }

    string userName = "Matti2";
    string albumName = "My Album2";
    string description = "Room album all of the pictures of the room";
  
    class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Album album = (Album)response;
            App42Log.Console("userName is :" + album.GetUserName());
            App42Log.Console("albumName is :" + album.GetName());
            App42Log.Console("Description is :" + album.GetDescription());
            App42Log.Console("jsonResponse is :" + album.ToString());
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }
}
