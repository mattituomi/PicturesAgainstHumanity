using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Common.gameLoader = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadGame()
    {
        Debug.Log("Starting to load game based on saved data");
        StartCoroutine(LoadGameRoutine());
        
    }

    void OnDestroy()
    {
        Debug.Log("game loader destroyed");
    }

    void LevelLoadUpdate()
    {
        Debug.Log("Level Loaded!");
        loadingScene = false;
    }

    bool loadingScene = false;
    void LoadSceneBasedOnState()
    {
        Debug.Log("Loading level at gamestate " + Common.roundInformation.gameData.gameState.ToString());
        loadingScene = true;
        Common.levelLoader.updateDelegates += this.LevelLoadUpdate;
        Common.levelLoader.LoadLevel(Common.roundInformation.gameData.gameState);
       
    }

    IEnumerator LoadGameRoutine()
    {
        Debug.Log("First lets load wanted scene based on gamestate");
        LoadSceneBasedOnState();
        while (loadingScene)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Scene has been loaded set values");
        GameDataGooglePlay gameData = Common.roundInformation.gameData;
        //FIrst load scene based on data
        RoundInformation ri = Common.roundInformation;
        PlayerInformation pi = Common.playerInformation;
        if (pi.isOnGame)
        {
            Common.infoGUI.setNewGuestion(ri.gameData.guestion);
            if (gameData.gameState == (int)GameStates.PickingPics)
            {
                if (pi.ready)
                {
                    if (pi.myCloudImagePath.Length > 5)
                    {
                        Common.infoGUI.DisableReadyPressing();
                        Debug.Log("Loading image from cloud");
                        pi.LoadMyImageFromURL();
                        pi.SetReady();
                        Common.infoGUI.SetReadyToggle(true);
                    }
                    else if (pi.myLocalImagePath.Length > 5)
                    {
                        Debug.Log("Loading image from local");
                        pi.LoadMyImageFromURL(pi.myLocalImagePath);
                        pi.ready = false;
                    }
                    else
                    {
                        pi.ready = false;
                    }
                }
            }
            if (gameData.gameState == (int)GameStates.PickingWinner)
            {
                Debug.Log("Creating picking winner scene");
                Common.showWinnerMaster.CreateElements(Common.roundInformation.gameData.playerAmount);
                Common.showWinnerMaster.LoadAllImagesFromPlayerURLS();
            }
        }
        else
        {
            //LOAD MENU
        }
        yield return new WaitForEndOfFrame();
    }
}
