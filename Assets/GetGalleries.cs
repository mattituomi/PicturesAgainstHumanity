
using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.gallery;
using System;
using System.Collections.Generic;

public class GetGalleries : MonoBehaviour {
    AlbumService albumService;
    // Use this for initialization
    void Start () {
        App42Log.SetDebug(true);        //Prints output in your editor console  
                                        //   App42API.Initialize("API_KEY", "SECRET_KEY");
        albumService = App42API.BuildAlbumService();
     //   albumService.GetAlbums(userName, new UnityCallBack());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            albumService.GetAlbums(userName, new UnityCallBack());
        }
    }

    String userName = "Matti";
   
    class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            IList<Album> album = (IList<Album>)response;
            for (int i = 0; i < album.Count; i++)
            {
                App42Log.Console("userName is :" + album[i].GetUserName());
                App42Log.Console("albumName is :" + album[i].GetName());
                App42Log.Console("Description is :" + album[i].GetDescription());
                App42Log.Console("jsonResponse is :" + album[i].ToString());
            }
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }
}
