using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameMaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Common.gameMaster = this;
       // DetermineWinner();
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

    public void DetermineWinner()
    {
        Debug.Log("determining winner");
        List<int> winnerVotes = Common.roundInformation.gameData.winnerVotes;

       // Debug.Log(Common.roundInformation.gameData.ChangeListintToString(winnerVotes));//ChangeStringListToInt(winnerVotes));
     //   Debug.Log(Common.roundInformation.gameData.ChangeListintToString(winnerVotes));
        List<int> votesPerPlayer = Common.usefulFunctions.InitializeListIntWithVarialbe(0,winnerVotes.Count);
        foreach(int vote in winnerVotes)
        {
            votesPerPlayer[vote]++;
        }
        List<int> voteWinners = Common.usefulFunctions.BiggestElement(votesPerPlayer);
        Debug.Log("Common round winners are "+voteWinners.GetString());
        int hostChoice = winnerVotes[Common.roundInformation.gameData.roundDeciderPID];
        Debug.Log("Host choice " + hostChoice.ToString());
        //Debug.Log(Common.roundInformation.gameData.ChangeListintToString(votesPerPlayer));
        
    }

    public void GoToGameStateBasedOnData(GameDataGooglePlay givenData)
    {

    }

}
