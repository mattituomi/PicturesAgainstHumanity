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

    void Awake()
    {
        Debug.Log("CHanging state to default 0 for testing purposes. On GameLoader Awake. MIght need2 change this0");
        ChangeUIBasedOnState(0);
    }

    public void LoadGamePasedOnSavedData()
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
    public GameObject LobbyUI;
    public GameObject PickPictureUI;
    public GameObject ShowWinnerUI;
    public GameObject PickWinnerUI;
    public GameObject ChoosingQuestionsUI; 

    //Loads the wanted scene and needed values and changes UI.
    public void LoadSceneBasedOnState()
    {
        Debug.Log("Loading level at gamestate " + Common.roundInformation.gameData.gameState.ToString());
       // loadingScene = true;
        //Common.levelLoader.updateDelegates += this.LevelLoadUpdate;
        //Scenen vaihdon sijaan toteutetaan että eri menu tulee näkyviin. Helpottaa siinämielessä ettei tarvitse aina vaihdella scenejä ja miettiä onko objektit kohdallaan.
        int gameState = Common.roundInformation.gameData.gameState;
        ChangeUIBasedOnState(gameState);
        LoadNewState(gameState);
         //ADDED  15.11.2016
        // Common.levelLoader.LoadLevel(Common.roundInformation.gameData.gameState);

    }

    public void printTestWhooo()
    {
        Debug.Log("Game loader whoooo");
    }

    int previousState = -1;
    public void ChangeBetweenLobbyAndCurrentState()
    {
        if (previousState == -1)
        {
            ChangeUIBasedOnState(0);
            previousState = Common.roundInformation.gameData.gameState;
        }else
        {
            ChangeUIBasedOnState(Common.roundInformation.gameData.gameState);
            previousState = -1;
        }
    }

    public void ChangeUIBasedOnState(int gameState)
    {
        Debug.Log("CHanging UI to this game state int " + gameState.ToString());
        LobbyUI.SetActive(false);
        PickPictureUI.SetActive(false);
        ShowWinnerUI.SetActive(false);
        PickWinnerUI.SetActive(false);
        ChoosingQuestionsUI.SetActive(false);

        if (gameState == (int)GameStates.ChoosingQuestions)
        {
            ChoosingQuestionsUI.SetActive(true);
        }
        if (gameState == (int)GameStates.Lobby)
        {

            LobbyUI.SetActive(true);
        }
        if (gameState == (int)GameStates.PickingPics)
        {
            PickPictureUI.SetActive(true);
        }
        if (gameState == (int)GameStates.ShowingWinner)
        {
            ShowWinnerUI.SetActive(true);
        }
        if (gameState == (int)GameStates.PickingWinner)
        {
            PickWinnerUI.SetActive(true);
        }
    }

    void LoadNewState(int gameState)
    {
        GameDataGooglePlay gameData = Common.roundInformation.gameData;
        //FIrst load scene based on data
        RoundInformation ri = Common.roundInformation;
        PlayerInformation pi = Common.playerInformation;
        if (gameData.AreWeOnPictureState())
        {
            Debug.Log("Setting new guestion");
            Common.infoGUI.setNewGuestion(ri.gameData.guestion);
            Debug.Log("new guestion has been set");
            if (gameState == (int)GameStates.PickingPics)
            {
                Common.DebugPopUp("GameLoader loadingNewState routine","Picking pics is the game state gonna check if we are ready and preload image if it exists");
                if (pi.ready)
                {
                    if (pi.myCloudImagePath.Length > 5)
                    {
                        Common.infoGUI.DisableReadyPressing();
                        Debug.Log("Loading image from cloud");
                        pi.LoadMyImageFromURL();
                        pi.SetReady();
                        Common.infoGUI.ChangeReadyButtonState(true);
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
               // Common.showWinnerMaster.ClearPossiblePreviousElements();
                Debug.Log("Creating picking winner scene");
                Common.DebugPopUp("GameLoader loadinggameroutine gamestate is picking winner Gonna create elements and load all images");
                Common.showWinnerMaster.CreateElements(Common.roundInformation.gameData.playerAmount);
                Debug.Log("Loading all images from playuer URL");
                Common.showWinnerMaster.LoadAllImagesFromPlayerURLS();
            }
            if (gameData.gameState == (int)GameStates.ShowingWinner)
            {
                Common.gameMaster.DetermineWinnerAndLoadTheSceneUI();
            }
        }
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
                        Common.infoGUI.ChangeReadyButtonState(true);
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
                Common.DebugPopUp("GameLoader loadinggameroutine gamestate is picking winner Gonna create elements and load all images");
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
