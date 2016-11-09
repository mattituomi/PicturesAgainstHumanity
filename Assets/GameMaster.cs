using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Common.gameMaster = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            loadNewRound();
        }
	}
    public string currenctGuestion="Empty question";
    public void loadNewRound()
    {
        Debug.Log("LOADING new round");
        string newGuestion = Common.guestionPicker.PickNewGuestion();
        currenctGuestion = newGuestion;
        Common.infoGUI.setNewGuestion(newGuestion);

    }

    public string getNextGuestion()
    {
        string newGuestion = Common.guestionPicker.PickNewGuestion();
        return newGuestion;
    }

    public void PlayerReady(PlayerInformation player)
    {
        Debug.Log("Player is ready");
        string debugL ="PlayerNumber on player that is ready is "+ player.playerNumber.ToString();
        Common.DebugPopUp(debugL);
        Common.roundInformation.gameData.readyStates[player.playerNumber]=true;
        
    }

    public void GoToGameStateBasedOnData(GameDataGooglePlay givenData)
    {

    }

}
