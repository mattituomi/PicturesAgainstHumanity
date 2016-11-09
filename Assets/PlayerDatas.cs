using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDatas : MonoBehaviour
{

    GameObject[] playerInfos;
    private List<int> takenNumbers;
    public List<GameObject> playerInfoGos = new List<GameObject>();
    public List<PlayerInformation> playerInfoScripts = new List<PlayerInformation>();
    // Use this for initialization
    void Start()
    {
        if (Common.playerDatas==null)
        {
            Common.playerDatas = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
       // DontDestroyOnLoad(this);
        //playerInfos = GameObject.FindGameObjectsWithTag("playerInfo");
        //playerInfoGos = new List<GameObject>();
        Debug.Log("player data start");

    }



    // Update is called once per frame
    void Awake()
    {
        takenNumbers = new List<int>();
    }

    public int getNextPlayerNumber()
    {
        int i = 1;
        while (takenNumbers.Contains(i))
        {
            i++;
        }
        takenNumbers.Add(i);
        return i;
    }
    

    public int getPlayerAmount()
    {
        return this.playerInfoGos.Count;
    }

    [RPC]
    public void addPlayer(NetworkViewID nwID)
    {
        Debug.Log("adding player in player datas");
        NetworkView newPlayerInfo = NetworkView.Find(nwID);
        playerInfoGos.Add(newPlayerInfo.gameObject);
    }

    [RPC]
    public void removePlayer(NetworkViewID nwID)
    {
        Debug.Log("removing player in player datas");
        NetworkView newPlayerInfo = NetworkView.Find(nwID);
        if (Network.isServer)
        {
            int freedNumber = newPlayerInfo.gameObject.GetComponent<PlayerInformation>().playerNumber;
            Debug.Log("Removing reserved player number " + freedNumber + " from list");
            takenNumbers.Remove(freedNumber);
        }
        playerInfoGos.Remove(newPlayerInfo.gameObject);
    }
    
    public List<GameObject> getPlayerInfos()
    {
        return playerInfoGos;
    }

    void removePlayerObjects()
    {
        /*
        Debug.Log("removing player objects (player datas)");
        for (int i = 0; i < playerInfoGos.Count; i++)
        {
            GameObject go = playerInfoGos[i];
            if (go.GetComponent<NetworkView>().isMine)
            {
                GetComponent<NetworkView>().RPC("removePlayer", RPCMode.All, go.GetComponent<NetworkView>().viewID);
            }
        }
        GameObject[] players;
        Debug.Log("finding all players");
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in players)
        {
            if (go.GetComponent<NetworkView>().isMine)
            {
                Debug.Log("found my own player info");
                Network.Destroy(go.GetComponent<NetworkView>().viewID);
            }
            
        }
        GameObject master = GameObject.FindGameObjectWithTag("Master");
        if (master)
        {
            master.GetComponent<NetworkView>().RPC("removePlayer", RPCMode.All);
        }
        GameObject lobby = GameObject.Find("Lobby");
        if (lobby)
        {
            //lobby.networkView.RPC("removePlayer", RPCMode.All);
        }
        */

    }

    void cleanPlayerDatas(NetworkPlayer nwp)
    {
        for (int i = 0; i < playerInfoGos.Count; i++)
        {
            GameObject go = playerInfoGos[i];
            if (go.GetComponent<NetworkView>().owner == nwp)
            {
                GetComponent<NetworkView>().RPC("removePlayer", RPCMode.All, go.GetComponent<NetworkView>().viewID);
            }
        }
    }

    void OnPlayerDisconnected(NetworkPlayer nwp)
    {
        //called on server when player disconnects
        Debug.Log("on player disc (player datas)");
        Debug.Log("Clean up after diconnected player " + nwp);
        cleanPlayerDatas(nwp);
        Network.RemoveRPCs(nwp);
        Network.DestroyPlayerObjects(nwp);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("on disc from server (player datas)");
        if (Network.isServer)
        {
            Debug.Log("Local server connection disconnected");
        }
        else {
            if (info == NetworkDisconnection.LostConnection)
            {
                Debug.Log("Lost connection to the server");
            }
            else {
                Debug.Log("Successfully diconnected from the server");
            }
            removePlayerObjects();
            destroyNetworkObjects();
            Application.LoadLevel("mainMenu");
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("on appl quit (player datas)");
        removePlayerObjects();
    }

    void destroyNetworkObjects()
    {
        GameObject.Destroy(GameObject.FindWithTag("settings"));
        GameObject.Destroy(GameObject.FindWithTag("playerDatas"));
        GameObject.Destroy(GameObject.FindWithTag("LevelLoader"));
        while (GameObject.FindWithTag("playerInfo") != null)
        {
            GameObject.DestroyImmediate(GameObject.FindWithTag("playerInfo"));
            Debug.Log("Destroyed player info because leaving room");
        }
    }



}
