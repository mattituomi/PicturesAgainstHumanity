using UnityEngine;
using System.Collections;

public class PlayerInformation : MonoBehaviour {

    public bool isOnGame = false;
    public int gameCounter;
    

    public string name;
    public int points;
    public bool ready=false;
    public int playerNumber = -1;
    public bool isHost = false;
    public Texture myImg;
    public string myID="Disconnected";
    public string myGPID="Disconnected";
    public string myCloudImagePath;
    public string myLocalImagePath;
    public bool connectedToGooglePlay;
    public bool isMyTurn = false;
    

	// Use this for initialization
	void Start () {
        Common.playerInformation = this;
	}
	
	// Update is called once per frame
	void Update () {
        //DebugPanel.Log("PIUPDATE", "CATHEGORY", Time.time.ToString());
        MyDebug("myID", myID);
        MyDebug("isHost", isHost.ToString());
        MyDebug("ismyTurn", isMyTurn.ToString());
        MyDebug("myGPID", myGPID.ToString());
    }

    public void setGPID(string gpid)
    {
        myGPID = gpid;
    }

    void MyDebug(string name,string log)
    {
        Common.DebugLog("PlayerInformation", name,log);
    }

    public void SetURL(string url,int imgNumber)
    {
        myCloudImagePath = url;
        //TODO ADD more images!
    }

    public void SetReady()
    {
        ready = true;
        Common.gameMaster.PlayerReady(this);
    }

    public void SetMyPlayerInformation(string id)
    {
        myID = id;
        GameDataGooglePlay currentData = Common.roundInformation.gameData;
        int foundNumber=FindMyPlayerNumber(currentData, id);
        playerNumber = foundNumber; //FindMyPlayerNumber(currentData, myGPID);
      //  AN_PoupsProxy.showMessage("Playerinformation setmyplayerinfo", "myID "+myGPID+" player number "+playerNumber.ToString());

    }

    public int FindMyPlayerNumber(GameDataGooglePlay data, string idToFind)
    {
        int counter = 0;
        foreach(string id in data.playerIDS)
        {
            if (id.Contains(idToFind)){
                AN_PoupsProxy.showMessage("FindMyPlayerNumber in playerinformation", "found player number " + counter.ToString());
                return counter;
            }
            counter++;
        }
        AN_PoupsProxy.showMessage("FindMyPlayerNumber didn't find player number", "Giving new pn "+counter.ToString());
        return counter;
    }

    public void LoadMyImageFromURL(string url)
    {
        Common.getImageFromURL.loadImage(url, this.gameObject, "SetMyImage");
    }

    public void LoadMyImageFromURL()
    {
        Common.getImageFromURL.loadImage(myCloudImagePath, this.gameObject, "SetMyImage");
    }


    public void LocalImagePicked(string imagePickPath)
    {
        myLocalImagePath = imagePickPath;
        Common.infoGUI.ReadyButtonToInteractable();
    }

    public void ReadyPressedSendImages(bool isTrue)
    {
        Common.cloudServiceMaster.AddPhotoToGallery();
        ready = true;
        Common.infoGUI.DisableReadyPressing();
    }

    public void SetMyImage(Texture downloadedImage)
    {
        Common.infoGUI.pickResult.material.mainTexture = downloadedImage;
        Material newMaterial = new Material(Common.infoGUI.pickResult.material);
        //  newMaterial.SetTexture("_MainTex", newText); //sprite.texture);
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", downloadedImage);
        // Common.infoGUI.pickResult.GetComponent<CanvasRenderer>().SetPropertyBlock(block);
        newMaterial.SetTexture("_MainTex", downloadedImage);
        Common.infoGUI.pickResult.material = newMaterial;
        myImg = downloadedImage;
    }

    public void SetMyImage(Texture2D downloadedImage)
    {
        Common.infoGUI.pickResult.material.mainTexture = downloadedImage;
        Material newMaterial = new Material(Common.infoGUI.pickResult.material);
        //  newMaterial.SetTexture("_MainTex", newText); //sprite.texture);
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", downloadedImage);
        // Common.infoGUI.pickResult.GetComponent<CanvasRenderer>().SetPropertyBlock(block);
        newMaterial.SetTexture("_MainTex", downloadedImage);
        Common.infoGUI.pickResult.material = newMaterial;
        myImg = downloadedImage;
    }

}
