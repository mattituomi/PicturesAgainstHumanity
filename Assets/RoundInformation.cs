using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NiceJson;
using System;

public enum GameDataVariables : int { playerAmount,hostID, roundImageURLs, readyStates ,gameStartTime, gameState, playerPoints ,playerIDS,round,matchID,roundWinnerPlayerNumber,roundDeciderPID,guestion,winnerVotes}
public enum GameStates: int { Lobby,ChoosingQuestions,PickingPics,PickingWinner,ShowingWinner}
[System.Serializable]
public class GameDataGooglePlay
{
    public int playerAmount;
    public string hostID;
    public List<string> roundImageURLs;
    public List<bool> readyStates;
    public string gameStartTime="0";
    public int gameState=0;
    public List<int> playerPoints;
    public List<string> playerIDS;
    public int round=0;
    public string matchID;
    public int roundWinnerPlayerNumber;
    public int roundDeciderPID;
    public string guestion;
    public List<int> winnerVotes;

    public GameDataGooglePlay()
    {
        roundImageURLs = new List<string>();
        readyStates = new List<bool>();
        playerPoints = new List<int>();
        winnerVotes = new List<int>();
        playerIDS = new List<string>();
    }

    public void PrintGameData()
    {
        //DebugPanel.Log("TBMFIXED", "ERROR", Time.time.ToString());
        PrintGD(Time.time.ToString(), "LAST UPDATED");
        PrintGD(playerAmount.ToString(), "playerAmount");
        PrintGD(hostID,"hostID");
        PrintGD(ChangeStringListToString(roundImageURLs), "roundImageURLs");
        PrintGD(readyStates.GetString(), "readyStates");
        PrintGD(gameStartTime, "gameStartTime");
        PrintGD(gameState.ToString(), "gameState");
        PrintGD(ChangeListintToString(playerPoints), "playerPoints");
        PrintGD(ChangeListintToString(winnerVotes), "WinnerVotes");
        PrintGD(playerIDS.Count.ToString(), "playerIDS");
        PrintGD(round.ToString(), "round");
        PrintGD(matchID, "matchID");
        PrintGD(roundWinnerPlayerNumber.ToString(), "roundWinnerPlayerNUmber");
        PrintGD(roundDeciderPID.ToString(), "roundDeciderPID");
        PrintGD(guestion, "guestion");


    }

    string ChangeStringListToString(List<string> list)
    {
        string result ="";
        int counter = 0;
        foreach(string member in list)
        {
            result +=" "+counter.ToString()+":" + member+" ";
            counter++;
        }
        return result;
    }

    public string ChangeListintToString(List<int> list)
    {
        string result = "";
        int counter = 0;
        foreach (int member in list)
        {
            result += " " + counter.ToString() + ":" + member.ToString() + " ";
            counter++;
        }
        return result;
    }

    void PrintGD(string log, string name)
    {
        Common.DebugLog(name, "gameData", log);
    }

    public bool AreWeOnPictureState()
    {
        if(gameState != (int)GameStates.Lobby && gameState != (int)GameStates.ChoosingQuestions)
        {
            return true;
        }
        return false;
    }
}
public class RoundInformation : MonoBehaviour {
    public float timer;
    public float maxTime;
    public GameDataGooglePlay gameData;
    public string savedGameDataID;
    public int playerAmount = 0;

    public GameDataGooglePlay testData;
    public string cst;
    public GameDataGooglePlay createdTestData;
    public bool initializeTestData = true;
    public bool useTestDataAsGameData = false;

    public int hostChoice;
    public List<int> voteWinners;


    // Use this for initialization
    void Start () {
        
        Common.roundInformation = this;
        /*
        cst = CreateGameDataString(testData);
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        
        byte[] byteArray = encoding.GetBytes(cst);
        JsonArray objects = (JsonArray)JsonNode.ParseJsonString(cst);
        createdTestData = GetGameDataFromJson(byteArray);
        */
        if (initializeTestData)
        {
            Invoke("CreateTestData", 0.3f);
        }
        else
        {
            if (useTestDataAsGameData)
            {
                gameData = testData;
            }
        }


    }
    
    void ClearReadyStates()
    {
        for (int n = 0; n < gameData.readyStates.Count; n++)
        {
            gameData.readyStates[n] = false;
            //gameData.roundImageURLs[n]
        }
    }

    public int FindNumberBasedOnID(string idToFind)
    {
        int counter = 0;
        foreach (string id in gameData.playerIDS)
        {
            if (id.Contains(idToFind))
            {
              //  AN_PoupsProxy.showMessage("FOUND NUBMER Finding pnumber "+idToFind+ "in Roundinformation", "found number " + counter.ToString());
                return counter;
            }
            counter++;
        }
        Common.DebugPopUp("NO NUMBER! Finding pnumber " + idToFind + "in Roundinformation", "returning 0");
        return 0;
    }

    void EndOneRound()
    {
        Debug.Log("Ending round current winners are " + hostChoice.ToString() + " current pending is " + gameData.roundDeciderPID);
        Common.DebugLog("winner", "ENDROUND", hostChoice.ToString());
        Common.DebugLog("decider","ENDROUND", gameData.roundDeciderPID.ToString());
        gameData.playerPoints[hostChoice] += 1;
        foreach(int wp in voteWinners)
        {
            gameData.playerPoints[wp] += 1;
        }
    }

    void CreateTestData()
    {
        //   #if UNITY_EDITOR
        string platform = "Windows";
        if (Application.platform == RuntimePlatform.Android)
            platform = "Android";
        InitializeGameData(new List<string>() { "1111", "2222", "3333" }, "TESTMATCHID"+platform+GetUniqueTestMatchID());
               Common.cloudServiceMaster.CreateAlbumOnGameStarted();
                 Common.playerInformation.playerNumber = 0;
             Common.roundInformation.gameData.gameState = (int)GameStates.PickingPics;
//#endif

    }

    public string GetPlayerURL(int playerID)
    {
        return gameData.roundImageURLs[playerID];
    }

    string GetUniqueTestMatchID()
    {
        int counter = PlayerPrefs.GetInt("TestMatchID", 0);
        counter++;
        PlayerPrefs.SetInt("TestMatchID", counter);
        return counter.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        gameData.PrintGameData();

    }

    public GameDataGooglePlay GetGameDataFromString(string jsonString)
    {
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

        byte[] byteArray = encoding.GetBytes(jsonString);
        return GetGameDataFromJson(byteArray);
    }

    public bool AllReady()
    {
        foreach(bool isReady in gameData.readyStates)
        {
            if (isReady ==false)
            {
                return false;
            }
        }
        return true;
    }

    public int ChangeGameState()
    {
        EndGameState(gameData.gameState);
        int newState = gameData.gameState + 1;
        if (newState > 4)
        {
            newState = 1;
        }
        //Skipataaan choosing guestion, koska tämä feature adataan myöhemmin
        if (newState == (int)GameStates.ChoosingQuestions)
        {
            SetNewDecider();
            gameData.guestion = Common.gameMaster.getNextGuestion();
            newState = 2;
        }
        Common.DebugPopUp("Changing game state", "new state is "+(GameStates)newState);
        gameData.gameState = newState;
        gameData.round++;
        
        InitializeNewState(newState);
        return newState;
    }
    public void SetNewGameDataFromGooglePlay(byte[] newData)
    {
        Debug.Log("setting new game data from googleplay gamedata"); //TODO maybe need to add some parsin to see if it contradicts with myown data.
        gameData=GetGameDataFromJson(newData);
    }
    void InitializeNewState(int state)
    {
        GameStates currentState=(GameStates)state;
        switch (currentState)
        {
            case GameStates.PickingPics:
                
                break;
            case GameStates.ChoosingQuestions:
                SetNewDecider();
                gameData.guestion=Common.gameMaster.getNextGuestion();
                break;

        }
    }

    void SetNewDecider()
    {
        int newDecider = gameData.roundDeciderPID + 1;
        if (newDecider >= gameData.playerIDS.Count)
        {
            newDecider = 0;
        }
        gameData.roundDeciderPID = newDecider;
    }

    void EndGameState(int state)
    {
        GameStates currentState = (GameStates)state;
        ClearReadyStates();
        switch (currentState)
        {
            case GameStates.PickingPics:

                break;
            case GameStates.ChoosingQuestions:
               // Common.gameMaster.DetermineWinnerAndLoadTheSceneUI();
                break;
            case GameStates.ShowingWinner:
                EndOneRound();
                break;

        }
    }
        

    public string GetNextPlayerID()
    {
        int counter = 0;

        foreach(bool isReady in gameData.readyStates)
        {
          //  AN_PoupsProxy.showMessage("GameState on GetNextPlayerID", "count "+counter.ToString()+" state: "+isReady.ToString());
          //  Common.DebugPopUp("GameState on GetNextPlayerID", "count " + counter.ToString() + " state: " + isReady.ToString());
            if (isReady == false)
            {
                return gameData.playerIDS[counter];
            }
            counter++;
        }
        Debug.Log("ERROR getting next playerID and all players are ready");
        Common.DebugLog("GETNextPlayerID", "RoundInformation", "All players ready");
        foreach(string id in gameData.playerIDS)
        {
            if (id == Common.playerInformation.myID)
            {

            }else
            {
                return id;
            }
        }



        return gameData.playerIDS[0];
        /*
        foreach (string playerId in gameData.playerIDS)
        {
            return playerId;
        }
        

        return Common.playerInformation.myID;
        */
    }

    public byte[] InitializeGameData(List<string> playerIdS, string matchID)
    {
        GameDataGooglePlay newData = new GameDataGooglePlay();
        newData.playerAmount = playerIdS.Count;
        newData.hostID =playerIdS[0] ;
        newData.gameStartTime = Common.usefulFunctions.GetLongTime();
        newData.roundDeciderPID =0;
        newData.roundWinnerPlayerNumber = -1;
        newData.guestion = "Haven't been set yet";
        foreach(string id in playerIdS)
        {
            newData.playerPoints.Add(0);
            newData.winnerVotes.Add(0);
            newData.roundImageURLs.Add("9");
            newData.readyStates.Add(false);
            newData.playerIDS.Add(id);
            newData.matchID = matchID;
            //AndroidMessage.Create("Addingthingies initializing game data", "playerIDS "+id);
        }
        newData.gameState = 0; //(int)GameStates.ChoosingQuestions;
        gameData = newData;
        Common.playerInformation.myID = newData.hostID;
       // Common.playerInformation.playerNumber = 0;

        return GetByteArrayFromData(newData);
        

    }

    public byte[] GetData()
    {
        return GetByteArrayFromData(gameData);
    }
    public string GetDataJSON()
    {
        return CreateGameDataString(gameData);
    }

    private byte[] GetByteArrayFromData(GameDataGooglePlay data)
    {
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        string mTurnData = CreateGameDataString(data);
        byte[] byteArray = encoding.GetBytes(mTurnData);
        return byteArray;
    }

    string CreateGameDataString(GameDataGooglePlay data)
    {
        JsonArray jsonArray = new JsonArray();
        JsonObject obj0 = Create1Object(data.playerAmount);
        JsonObject obj1 = Create1Object(data.hostID);
        JsonObject obj2 = Create1Object(data.roundImageURLs);
   JsonObject obj3 = Create1Object(data.readyStates);
     JsonObject obj4 = Create1Object(data.gameStartTime);
    JsonObject obj5 = Create1Object(data.gameState);
    JsonObject obj6 = Create1Object(data.playerPoints);
        JsonObject obj7 = Create1Object(data.playerIDS);
        JsonObject obj8 = Create1Object(data.round);
        JsonObject obj9 = Create1Object(data.matchID);
        JsonObject obj10 = Create1Object(data.roundWinnerPlayerNumber);
        JsonObject obj11 = Create1Object(data.roundDeciderPID);
        JsonObject obj12 = Create1Object(data.guestion);
        JsonObject obj13 = Create1Object(data.winnerVotes); //added 17.11
        jsonArray.Add(obj0);
        jsonArray.Add(obj1);
        jsonArray.Add(obj2);
        jsonArray.Add(obj3);
        jsonArray.Add(obj4);
        jsonArray.Add(obj5);
        jsonArray.Add(obj6);
        jsonArray.Add(obj7);
        jsonArray.Add(obj8);
        jsonArray.Add(obj9);
        jsonArray.Add(obj10);
        jsonArray.Add(obj11);
        jsonArray.Add(obj12);
        jsonArray.Add(obj13);
        Debug.Log(obj1.ToString());
        Debug.Log(jsonArray.ToJsonPrettyPrintString());
        return jsonArray.ToJsonString();
    }

    GameDataGooglePlay GetGameDataFromJson(byte[] gameData)
    {
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        string stringData = encoding.GetString(gameData,0,gameData.Length);
        GameDataGooglePlay newGameData = new GameDataGooglePlay();
        JsonArray objects = (JsonArray)JsonNode.ParseJsonString(stringData);
        int counter = 0;
        foreach (JsonObject valueObject in objects)
        {
            int caseSwitch = counter;
            Debug.Log(valueObject.ToJsonString());
           // Debug.Log(valueObject[0]);
            GameDataVariables gdv = (GameDataVariables)counter;
            switch (gdv)
            {
                case GameDataVariables.playerAmount:
                    newGameData.playerAmount = valueObject["value"];
                    playerAmount = newGameData.playerAmount;
                    break;
                case GameDataVariables.hostID:
                    newGameData.hostID=valueObject["value"];
                    Debug.Log("Host ID is " + newGameData.hostID);
                    break;
                case GameDataVariables.gameState:
                    newGameData.gameState = valueObject["value"];
                    break;
                case GameDataVariables.gameStartTime:
                    newGameData.gameStartTime = valueObject["value"];
                    break;
                case GameDataVariables.matchID:
                    newGameData.matchID = valueObject["value"];
                    break;
                case GameDataVariables.roundWinnerPlayerNumber:
                    newGameData.roundWinnerPlayerNumber = valueObject["value"];
                    break;
                case GameDataVariables.round:
                    newGameData.round = valueObject["value"];
                    break;
                case GameDataVariables.guestion:
                    newGameData.guestion = valueObject["value"];
                    break;
                case GameDataVariables.roundDeciderPID:
                    newGameData.roundDeciderPID = valueObject["value"];
                    break;
                case GameDataVariables.readyStates:
                    List<string> stringList = getListValue(playerAmount,valueObject);
                    newGameData.readyStates = ListStringConvert(stringList,true);
                    break;
                case GameDataVariables.playerIDS:
                    List<string> stringList2 = getListValue(playerAmount, valueObject);
                    newGameData.playerIDS = stringList2;
                    break;
                case GameDataVariables.playerPoints:
                    List<string> stringList3 = getListValue(playerAmount, valueObject);
                    newGameData.playerPoints = ListStringConvert(stringList3);
                    break;
                case GameDataVariables.roundImageURLs:
                    List<string> stringList4 = getListValue(playerAmount, valueObject);
                    newGameData.roundImageURLs = stringList4;
                    break;
                case GameDataVariables.winnerVotes:
                    List<string> stringList5 = getListValue(playerAmount, valueObject);
                    newGameData.winnerVotes = ListStringConvert(stringList5);
                    break;
            }
            counter++;
        }

        return newGameData;
    }

    List<int> ListStringConvert(List<string> stringList)
    {
        List<int> toreturn = new List<int>();
        foreach(string value in stringList)
        {
            int val= Int32.Parse(value);
            toreturn.Add(val);
        }
        return toreturn;
    }

    List<bool> ListStringConvert(List<string> stringList,bool valdef)
    {
        List<bool> toreturn = new List<bool>();
        foreach (string value in stringList)
        {
            bool val = Convert.ToBoolean(value);
            toreturn.Add(val);
        }
        return toreturn;
    }

    List<string> getListValue(int amount, JsonObject json)
    {
        List<string> toReturn = new List<string>();
        for(int n=0; n<amount; n++)
        {
            toReturn.Add(json[n.ToString()]);
        }
        return toReturn;

    }

    JsonObject Create1Object(string[] stringArray)
    {
        JsonObject createdObj=new JsonObject();
        int counter = 0;
        foreach (string value in stringArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }
    JsonObject Create1Object(List<string> stringArray)
    {
        JsonObject createdObj = new JsonObject();
        int counter = 0;
        foreach (string value in stringArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }
    JsonObject Create1Object(List<bool> stringArray)
    {
        JsonObject createdObj = new JsonObject();
        int counter = 0;
        foreach (bool value in stringArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }
    JsonObject Create1Object(List<int> stringArray)
    {
        JsonObject createdObj = new JsonObject();
        int counter = 0;
        foreach (int value in stringArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }


    JsonObject Create1Object(int[] intArray)
    {
        JsonObject createdObj = new JsonObject();
        int counter = 0;
        foreach (int value in intArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }

    JsonObject Create1Object(bool[] boolArray)
    {
        JsonObject createdObj = new JsonObject();
        int counter = 0;
        foreach (bool value in boolArray)
        {
            string name = counter.ToString();
            createdObj[name] = value;
            counter++;
        }
        return createdObj;
    }

    JsonObject Create1Object(int intValue,string name="value")
    {
        JsonObject createdObj = new JsonObject();
        createdObj[name] = intValue;
        return createdObj;
    }
    JsonObject Create1Object(string stringValue, string name = "value")
    {
        JsonObject createdObj = new JsonObject();
        createdObj[name] = stringValue;
        return createdObj;
    }
    JsonObject Create1Object(bool boolValue, string name = "value")
    {
        JsonObject createdObj = new JsonObject();
        createdObj[name] = boolValue;
        return createdObj;
    }

}
