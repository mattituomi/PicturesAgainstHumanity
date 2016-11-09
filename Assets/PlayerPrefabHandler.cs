using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class PlayerPrefabHandler : MonoBehaviour {

    // Use this for initialization
   public bool valuesLoaded = false;
    public bool loadValues = false;
	void Start () {
       
        if (Common.playerPrefabHandler)
        {
            Debug.Log("Destroying this");
            Destroy(this.gameObject);
        }
        else
        {
            Common.playerPrefabHandler = this;
        // DONT DESTROY TAKAISIN    DontDestroyOnLoad(this.gameObject);
        }
        Debug.Log("Common pph is " + Common.playerPrefabHandler.name);
        if (Common.valuesLoaded == false)
        {
            Common.valuesLoaded = true;
            if (loadValues)
            {
                LoadValues();
            }
            
        }
        

        
	}

    void LoadValues()
    {

        //turnAllToPrefabs ();
        string gameDataString = PlayerPrefs.GetString("GameData", "ERROR");
        bool isOnGame = false;
        isOnGame = PlayerPrefsX.GetBool("OnGame", false);

        Debug.Log("IS ON GAME IS " + isOnGame);
        if (isOnGame)
        {
            string JSONS = PlayerPrefs.GetString("GameData", "ERROR98989898989898123");
            if (JSONS.Contains("ERROR98989898989898123"))
            {
                isOnGame = false;
            }
            else
            {
                Common.playerInformation.playerNumber = PlayerPrefs.GetInt("PlayerNumber", Common.playerInformation.playerNumber);
                if (!Common.roundInformation.initializeTestData && Common.roundInformation.useTestDataAsGameData == false)
                {

                    Common.roundInformation.gameData = Common.roundInformation.GetGameDataFromString(JSONS);
                    Common.playerInformation.myID = Common.roundInformation.gameData.playerIDS[Common.playerInformation.playerNumber];
                }
                Common.playerInformation.isOnGame = isOnGame;

                //  Common.playerInformation.picChosen=PlayerPrefsX.GetBool("PicChosen", Common.playerInformation.picChosen);
                Common.playerInformation.myCloudImagePath = PlayerPrefs.GetString("ImageURL", Common.playerInformation.myCloudImagePath);
                Common.playerInformation.myLocalImagePath = PlayerPrefs.GetString("PickedImageLocalPath", Common.playerInformation.myCloudImagePath);
                Common.playerInformation.ready = PlayerPrefsX.GetBool("Ready", Common.playerInformation.ready);
                //    Common.playerInformation.myID = Common.roundInformation.gameData.playerIDS[Common.playerInformation.playerNumber];
                Common.gameLoader.LoadGame();
            }
        }
    }

    public void SavePlayerPrefabs()
    {
        PlayerPrefsX.SetBool("OnGame", Common.playerInformation.isOnGame);

        if (Common.playerInformation.isOnGame)
        {
            PlayerPrefs.SetString("GameData", Common.roundInformation.GetDataJSON());
            Debug.Log("SAVEDGAME DATA " + Common.roundInformation.GetDataJSON());
            PlayerPrefs.SetInt("PlayerNumber", Common.playerInformation.playerNumber);
           // PlayerPrefsX.SetBool("PicChosen", Common.playerInformation.picChosen);
            PlayerPrefs.SetString("ImageURL", Common.playerInformation.myCloudImagePath);
            PlayerPrefs.SetString("PickedImageLocalPath", Common.playerInformation.myLocalImagePath);
            PlayerPrefsX.SetBool("Ready", Common.playerInformation.ready);
        }
    }

    void OnApplicationQuit()
    {
        SavePlayerPrefabs();
    }
    /*
	void turnAllToPrefabs(){
		mapNames = maps.giveMapNames ();
		List<String> fullNames = maps.giveFullNames ();
		int counter = 0;
		foreach (string map in fullNames) {
			List<Vector3> toPrefab=tiled.getSpawnedThings(map);
			Vector3[] vecArray=toPrefab.ToArray();
			PlayerPrefsX.SetVector3Array(mapNames[counter],vecArray);
			counter++;
			}

		PlayerPrefsX.SetStringArray ("maps", mapNames.ToArray());

		//mapss = mapNames.ToArray (new String[mapNames.Count-1]);
		mapss=new string[mapNames.Count];
		mapss = mapNames.ToArray ();
		int n = 0;
		foreach (string mapName in mapNames) {
			Debug.Log("maaaap name on "+mapName);
			//mapss[n]=mapName;
			n++;
				}
		Debug.Log (" all has been turned sssssssss"+mapNames.Count);
		//string[] names = new string[3] {"Matt", "Joanne", "Robert"};
		//PlayerPrefsX.SetStringArray ("mapss", mapNames.ToArray());
		}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			//turnAllToPrefabs();
			mapNames = maps.giveMapNames ();
			//mapss=new string[mapNames.Count];
			mapss=PlayerPrefsX.GetStringArray("maps");
			Debug.Log("getting string array");
			Debug.Log("getingst ring assadsd "+PlayerPrefsX.GetStringArray ("mapss").Length);
				}
		if(Input.GetKeyDown(KeyCode.B)){
			turnAllToPrefabs ();
		}
	}
    */
}
