using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using System;

public class App42 : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Common.app42 = this;
        // App42API.Initialize("0f8d9be1cad28cf2dd9b67c33555635f29f26992723be154a70e49083a68f952", "960640b896bf9ff26c6abbd7c854fc0dfc08ef65f56e98f5d7227015e662957b");
        InitializeApp42();
        UserService userService = App42API.BuildUserService();

      //  userService.CreateUser(userName, pwd, emailId, new UnityCallBack());
    }
	public void InitializeApp42()
    {
        App42API.Initialize("0f8d9be1cad28cf2dd9b67c33555635f29f26992723be154a70e49083a68f952", "960640b896bf9ff26c6abbd7c854fc0dfc08ef65f56e98f5d7227015e662957b");
    }
	// Update is called once per frame
	void Update () {
	
	}

    string userName = "matti2";
    string pwd = "mattituomi1990";
    string emailId = "matti2.tuomi@shephertz.co.in";
 
    class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            User user = (User)response;
            Debug.Log("userName is " + user.GetUserName());
            Debug.Log("emailId is " + user.GetEmail());
        }
        public void OnException(Exception e)
        {
            Debug.Log("Exception : " + e);
        }

    }
}


