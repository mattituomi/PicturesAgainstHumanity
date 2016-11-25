
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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
         //   StartCoroutine(RemoveAlbumsFromUser());
            albumService.GetAlbums(userName, new UnityCallBack());
        }
#endif
    }

    public String userName = "p_1";
    public static IList<Album> albumsLoaded;
    public static bool albumsLoading = false;
    public static bool albumRemoving = false;


    class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            IList<Album> album = (IList<Album>)response;
            albumsLoaded = album;
            albumsLoading = false;
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

    IEnumerator RemoveAlbumsFromUser()
    {
        albumsLoading = true;
        albumService.GetAlbums(userName, new UnityCallBack());
        while (albumsLoading)
        {
            yield return new WaitForSeconds(1f);
        }

        for (int i = 0; i < albumsLoaded.Count; i++)
        {
            yield return StartCoroutine(RemoveAlbumRoutine(albumsLoaded[i].GetName()));
        }

    }

    IEnumerator RemoveAlbumRoutine(string albumName)
    {
        Debug.Log("Removing Album");
        albumRemoving = true;
        RemoveAlbum(userName,albumName);
        while (albumRemoving)
        {
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    void RemoveAlbum(string nuserName, string nalbumName)
    {
        albumService.RemoveAlbum(nuserName, nalbumName, new RemoveAlbumCallBack());
    }

    
    public class RemoveAlbumCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            albumRemoving = false;
            Debug.Log("AlbumRemoved");
            App42Response app42Response = (App42Response)response;
            App42Log.Console("app42Response is :" + app42Response.ToString());
        }
        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }
}
