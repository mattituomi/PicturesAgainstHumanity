using UnityEngine;
using System.Collections;
using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System.Collections.Generic;
using SimpleJSON;
using NiceJson;
[System.Serializable]
public class GameData
{
    public string[] participantIDs;
    public bool[] readyStates;
    public string gameStartTime;
}

public class CreateGameData : MonoBehaviour {
    StorageService storageService;
    public void CreateGame(GameData data)
    {

    }
    // Use this for initialization
    void Start() {

        storageService = App42API.BuildStorageService();
    }

    //RUN THIS WHEN THE GAME STARTS
    public void CreateGameDataToStorage(GameData data)
    {
        JSONClass json = new JSONClass();
        int counter = 0;
        string nameGoer = "ID";
        JsonArray arrayExample = new JsonArray();
        JsonObject names = new JsonObject();
        foreach (string id in data.participantIDs)
        {
            string name= nameGoer+counter.ToString();
            names[name] = id;
            counter++;
        }
        foreach (bool state in data.readyStates)
        {
            string name = "ISReady" + counter.ToString();
            names[name] = state;
            counter++;
        }
        names["StartTime"] = Time.time.ToString();
        //      arrayExample.Add(names);
        // arrayExample.Add(names);
        Debug.Log(arrayExample.ToJsonString());
        App42Log.SetDebug(true);        //Print output in your editor console  
        storageService.InsertJSONDocument(dbName, Common.roundInformation.gameData.hostID, names.ToJsonString(), new UnityCallBack());
    }

    // Update is called once per frame
    public GameData data;
    void Update() {
        if (Input.GetKeyDown(KeyCode.D))
        {
            CreateGameDataToStorage(data);
        }
    }


    String dbName = "PICTURESAGAINSTHUMANITY";
    String collectionName = "test";
    String employeeJSON = "{\"test\":\"InsertJSONDocument\",\"data\":\"inserted in App42 data base\"}";
    string docID= "575024f1e4b0220cfa7f0f9f";

   

    public class UnityCallBack : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Storage storage = (Storage)response;
            IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
            for (int i = 0; i < jsonDocList.Count; i++)
            {
                App42Log.Console("objectId is " + jsonDocList[i].GetDocId());
                App42Log.Console("Created At " + jsonDocList[i].GetCreatedAt());
                Common.roundInformation.savedGameDataID = jsonDocList[i].GetDocId();
            }
        }

        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }

    public void createGameWithFbFriend(String friendId, String friendName) {
        JSONClass json = new JSONClass();
    }


    public void GetGameData(){
        storageService.FindDocumentById(dbName, Common.roundInformation.gameData.hostID, Common.roundInformation.savedGameDataID, new UnityCallBack());   
    }


    public class UnityCallBack2 : App42CallBack
    {
        public void OnSuccess(object response)
        {
            Storage storage = (Storage)response;
            IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
            for (int i = 0; i < jsonDocList.Count; i++)
            {
                App42Log.Console("objectId is " + jsonDocList[i].GetDocId());
                App42Log.Console("jsonDoc is " + jsonDocList[i].GetJsonDoc());
            }
        }

        public void OnException(Exception e)
        {
            App42Log.Console("Exception : " + e);
        }
    }



}
