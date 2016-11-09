using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GPCManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    public bool IsInitialized;
    public static GPConnectionState State;
    public string myID;
    // Update is called once per frame
    void Update () {
      //  Debug.Log(GooglePlayConnection.State);
        if(GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED) {
            //Player is connected
           // Debug.Log("");
            currentPlayer = GooglePlayManager.Instance.player;
            
            if (currentPlayer != null)
            {
             //   Debug.Log(currentPlayer.name);
               // currentPlayer.LoadIcon();
             //   Common.infoGUI.SetProfilePic(currentPlayer.icon);
            }
        } 
	}
    GooglePlayerTemplate currentPlayer;
    public void ConnectPlayer()
    {
        GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;
        GooglePlayConnection.Instance.Connect();
        Debug.Log("Connecting");

        }

    public void WhatAccountsAvailable()
    {
       // GooglePlayManager.ActionAvailableDeviceAccountsLoaded +="ActionAvaliableDeviceAccountsLoaded";
      //  GooglePlayManager.Instance.RetrieveDeviceGoogleAccounts();//RetriveDeviceGoogleAccounts();
    }


private void ActionAvaliableDeviceAccountsLoaded()
    {
        string msg = "Device contains following google accounts:" + "\n";
        foreach (string acc in GooglePlayManager.Instance.deviceGoogleAccountList)
        {
            msg += acc + "\n";
            Debug.Log(acc);
        }
        //Debug.Log("Accounts loaded")
        //AndroidNative.Show("Accounts Loaded", msg);
    }



private void ActionConnectionResultReceived(GooglePlayConnectionResult result)
    {
        if (result.IsSuccess)
        {
            Debug.Log("Connected!");
            Debug.Log("ID IS " + GooglePlayManager.Instance.player.playerId);
            Debug.Log("Name is " + GooglePlayManager.Instance.player.name);
            myID = GooglePlayManager.Instance.player.playerId;
            Common.playerInformation.myID = myID;

        }
        else {
            Debug.Log("Cnnection failed with code: " + result.code.ToString());
        }
    }






}
