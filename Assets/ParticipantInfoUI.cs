using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParticipantInfoUI : MonoBehaviour {
    public Text myIDinGame;
    public Text my_id;
    public Image myImage;
    public Text display_name;
    public Toggle readyToggle;
    public Toggle myPlayerIndicator;
    public Toggle onTurnIndicator;
    public Text myPoints;

    public void setParticipantValues(string mgID, string mID, string displayName, bool isReady, bool isMyPlayer, bool isMyTurn)
    {
        myIDinGame.text = mgID;
        my_id.text = mID;
        display_name.text = displayName;
        readyToggle.isOn = isReady;
        myPlayerIndicator.isOn = isMyPlayer;
        onTurnIndicator.isOn = isMyTurn;
    }

    public void SetImage(Texture2D texture)
    {
        Common.SetTexture2DToImage(myImage, texture);
    }

    public void SetPoints(int newPoints)
    {
        myPoints.text = newPoints.ToString();
    }
	// Use this for initialization
	void Start () {
// setParticipantValues("MGID", "myID", "displayName", false, false, false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
