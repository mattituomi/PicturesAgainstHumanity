using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.gallery;
using System;
using System.Collections.Generic;

public class Gallery : MonoBehaviour {
    AlbumService albumService;
    PhotoService photoService;

    public delegate void UpdateDelegate();

    public UpdateDelegate updateDelegates;
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
        albumService.CreateAlbum(userName, albumName, description, new AlbumCallBack());
        
    }

    public void AddPhoto(string userName,string albumName,string photoName,string description,string filePath)
    {
        photoService.AddPhoto(userName, albumName, photoName+"new4", description, filePath, new AddPhotoCallback());

    }


    public class AddPhotoCallback : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Debug.Log("Adding photo succesfull");
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

            Common.cloudServiceMaster.PhotoAddWasSuccesfull();
           // updateDelegateForPhotoAddSuccesfull();
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
            Debug.Log("Adding photo FAILED Exception is "+e);
            Common.DebugPopUp("ADDING PHOTO FAILED error is :"+e,"Let matti know if this isnt due to network");
        }
    }

    string userName = "Matti2";
    string albumName = "My Album2";
    string description = "Room album all of the pictures of the room";
  
    class AlbumCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Debug.Log("Creating album was succesfull");
            Album album = (Album)response;
            App42Log.Console("userName is :" + album.GetUserName());
            App42Log.Console("albumName is :" + album.GetName());
            App42Log.Console("Description is :" + album.GetDescription());
            App42Log.Console("jsonResponse is :" + album.ToString());
        }
        public void OnException(Exception e)
        {
            Debug.Log("Creating Album was unsuccesfull On Gallery greation TODO: WHAT DO WE DO NOW? CLOSE GAME? TRY TO MAKE IT AGAIN?");
            Common.DebugPopUp("Creating Album was unsuccesfull On Gallery greation TODO: WHAT DO WE DO NOW? CLOSE GAME? TRY TO MAKE IT AGAIN? IF U ENCOUNTER THIS LET MATTI KNOW");
            App42Log.Console("Exception : " + e);
        }
    }
}
