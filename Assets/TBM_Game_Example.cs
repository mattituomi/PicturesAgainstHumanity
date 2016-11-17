//#define SA_DEBUG_MODE
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Diagnostics;

public class TBM_Game_Example : AndroidNativeExampleBase
{

    public ActiveMatchesGridHandler activeMatchesGridHandler;

    public GameObject avatar;
    public GameObject hi;
    public SA_Label playerLabel;
    public SA_Label gameState;
    public SA_Label parisipants;
    public Text playerConnectedStatus;

    Texture defaulttexture;

    public DefaultPreviewButton connectButton;


    public DefaultPreviewButton helloButton;
    public DefaultPreviewButton leaveRoomButton;
    public DefaultPreviewButton showRoomButton;


    public DefaultPreviewButton[] ConnectionDependedntButtons;
    public SA_PartisipantUI[] patrisipants;


    //private Texture defaulttexture;

    void Start()
    {
        // Debug_previous_method_name();
        Debug_previous_method_name();
        Common.TBM_game = this;
        playerConnectedStatus.text = "Player Disconnected";
        playerLabel.text = "Player Disconnected";
        defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;



        GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
        GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;

        GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;





        if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
        {
            //checking if player already connected
            OnPlayerConnected();
        }

        InitTBM();
        Init();

        Invoke("ConnectGooglePlay", 4f); //OTA POIS HELPOTTAA DEBUGAUSTA
    }




    public void Init()
    {
        AN_PoupsProxy.showMessage("Initializing action functions Init ", " Init called");
        GooglePlayTBM.ActionMatchUpdated += ActionMatchUpdated;
        GooglePlayTBM.ActionMatchDataLoaded += ActionMatchDataLoadedFunction;
        GooglePlayTBM.ActionMatchesResultLoaded += ActionMatchesDataLoadedFunction;
        GooglePlayTBM.ActionMatchReceived += ActionMatchReceivedFunction;
        GooglePlayTBM.ActionMatchTurnFinished += GooglePlayTBM_ActionMatchTurnFinished;
        GooglePlayTBM.ActionMatchInvitationAccepted += GooglePlayTBM_ActionMatchInvitationAccepted;
        GooglePlayTBM.ActionMatchCreationCanceled += ActionMatchCreationCanceled;
        GooglePlayTBM.ActionMatchInitiated += ActionMatchInitiated;
        GooglePlayInvitationManager.Instance.RegisterInvitationListener();
        GooglePlayInvitationManager.ActionInvitationReceived += ActionInvitationReceived;

        GooglePlayInvitationManager.ActionInvitationReceived += OnInvite;
    }
    private void ActionInvitationReceived(GP_Invite invitation)
    {
        Debug_previous_method_name();
        Common.DebugPopUp("Action invitation received function when app is running");
        GooglePlayTBM.Instance.AcceptInvitation(invitation.Id);
    }
    private void OnInvite(GP_Invite invitation)
    {
        Debug_previous_method_name();
        Common.DebugPopUp("OnInvite Function! Will soon acceptInvitation. Started OFFLINE");
        if (invitation.InvitationType != GP_InvitationType.INVITATION_TYPE_REAL_TIME)
        {
            return;
        }

        GooglePlayTBM.Instance.AcceptInvitation(invitation.Id);
    }
    //GooglePlayTBM.Action



    private void GooglePlayTBM_ActionMatchInvitationAccepted(string arg1, GP_TBM_MatchInitiatedResult arg2)
    {
        Debug_previous_method_name();
        Common.DebugPopUp("Action match invitation accepted arg1 is " + arg1, " starting to deal with data");
        //  AN_PoupsProxy.showMessage("Action match invitation accepted arg1 is "+arg1, " starting to deal with data");
        DealWithMatchData(arg2.Match);
    }

    private void GooglePlayTBM_ActionMatchTurnFinished(GP_TBM_UpdateMatchResult obj)
    {
        Debug_previous_method_name();
        Common.DebugPopUp("ActionMatch turn finished called ", " starting to deal with data. Next will check if obj.Match exists");
        Common.DebugPopUp("Checking what obj.Match.MatchNumber is :" + obj.Match.MatchNumber.ToString());
        DealWithMatchData(obj.Match);
    }

    private void ActionMatchReceivedFunction(GP_TBM_MatchReceivedResult result)
    {
        Debug_previous_method_name();
        Common.DebugPopUp("ActionMatchReceivedFunction called, ", " starting to deal with data");
        DealWithMatchData(result.Match);
    }

    void Debug_previous_method_name()
    {
        string name = Common.usefulFunctions.GetPreviousMethodName();
        string timenow = Common.usefulFunctions.GetMinuteHourTime();
        //System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(System.DateTime.Now);
        //string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        TBM_Debug(name, timenow);

    }

    //if UNITY_ANDROID
    public GP_TBM_Match mMatch = null;

    //...

    // Call this method when a player has completed his turn and wants to
    // go onto the next player, which may be himself.
    public void playTurn()
    {
        bool isAllready = Common.roundInformation.AllReady();
        string nextParticipantId = "";
        if (!isAllready)
        {
            nextParticipantId = Common.roundInformation.GetNextPlayerID(); //TODO SELVITÄ HALUTAANKOplayerID vai ID
        }
        else
        {
            Common.roundInformation.ChangeGameState();
            nextParticipantId = Common.roundInformation.gameData.playerIDS[Common.roundInformation.gameData.roundDeciderPID]; //Common.roundInformation.GetNextPlayerID();
        }
        // Get the next participant in the game-defined way, possibly round-robin.
        //nextParticipantId = getNextParticipantId();


        // Get the updated state. In this example, we simply use a
        // a pre-defined string. In your game, there may be more complicated state.
        //string mTurnData = "My turn data sample";

        // At this point, you might want to show a waiting dialog so that
        // the current player does not try to submit turn actions twice.



        // Invoke the next turn. We are converting our data to a byte array.

        //System.Text.UTF8Encoding  encoding = new System.Text.UTF8Encoding();
        AndroidNativeUtility.ShowPreloader("Loading..", "Sending the turn data newxt player " + nextParticipantId + " matchis " + mMatch.Id);
        byte[] byteArray = Common.roundInformation.GetData();

        unShowTurnUI();
        Invoke("unShowLoadingPopUp", 4f);
        GooglePlayTBM.Instance.TakeTrun(mMatch.Id, byteArray, nextParticipantId);

    }

    //#endif

    bool specificAccountDelegateGiven = false;
    public void ConnectSpecificAccount(string accountToConnectTo)
    {
        // Debug.Log("connecting to account " + accountToConnectTo);
        // GooglePlayConnection.Instance.Connect(accountToConnectTo);
        if (!specificAccountDelegateGiven)
        {
            specificAccountDelegateGiven = true;
            GooglePlayManager.ActionAvailableDeviceAccountsLoaded += ActionAvaliableDeviceAccountsLoaded;

        }
        GooglePlayManager.Instance.RetrieveDeviceGoogleAccounts();
        //Invoke("ActionAvaliableDeviceAccountsLoaded",5f);
    }



private void ActionAvaliableDeviceAccountsLoaded(List<string> result)
    {
        string msg = "Device contains following google accounts:" + "\n";
        foreach (string acc in GooglePlayManager.Instance.deviceGoogleAccountList)
        {
            msg += acc + "\n";
        }
        string accountToConnectTo = GooglePlayManager.Instance.deviceGoogleAccountList[0];
        Debug.Log("connecting to account " + accountToConnectTo);
        GooglePlayConnection.Instance.Connect(accountToConnectTo);
       // Common.DebugPopUp("Accounts Loaded", msg);
    }

    public void ClearAccount()
    {
        GooglePlusAPI.Instance.ClearDefaultAccount();

    }

    public void ClearAccountAndConnect()
    {
        ClearAccount();
        Invoke("ConnectGooglePlay",3f);
    }

    void ActionMatchUpdated(GP_TBM_UpdateMatchResult result)
    {
        // Match Date updated
        Debug_previous_method_name();
        unShowLoadingPopUp();
        AN_PoupsProxy.showMessage("ACTION match updated", "We got updated");
        DealWithMatchData(result.Match);
        //result.Match.Data
        // Common.roundInformation.gameData
    }


    public void InitTBM()
    {
        int variant = 1;
        GooglePlayTBM.Instance.SetVariant(variant);

        int ROLE_WIZARD = 0x4; // 100 in binary
        GooglePlayTBM.Instance.SetExclusiveBitMask(ROLE_WIZARD);

        GooglePlayTBM.Instance.RegisterMatchUpdateListener();

    }

    public void ShowInboxUI()
    {
        GooglePlayTBM.Instance.ShowInbox();
    }

    public void FinishMathc()
    {

        //  GooglePlayTBM.Instance.FinishMatch();

    }


    public void findMatch()
    {



        int minPlayers = 1;
        int maxPlayers = 5;
        bool allowAutomatch = true;

        GooglePlayTBM.Instance.StartSelectOpponentsView(minPlayers, maxPlayers, allowAutomatch);




    }

    public void ShowPendingInvitations()
    {

        GooglePlayTBM.Instance.ShowInbox();
        // GooglePlayTBM.Instance.
    }

    void ActionMatchCreationCanceled(AndroidActivityResult result)
    {
        Debug_previous_method_name();
        AndroidMessage.Create("Match craetion cancelled", "Cancelled");
        // Match Creation was cnaceled by user
    }

    void ActionMatchInitiated(GP_TBM_MatchInitiatedResult result)
    {
        Debug_previous_method_name();
        unShowLoadingPopUp();

        if (!result.IsSucceeded)
        {
            AndroidMessage.Create("Match Initi Failed", "Status code: " + result.Response);
            return;
        }


        // If this player is not the first player in this match, continue.
        AndroidMessage.Create("Match Initiated ", "Status code: " + result.Response);
        int counter = 0;

        GP_TBM_Match Match = result.Match;
        mMatch = result.Match;
        DealWithMatchData(mMatch);

    }
    
    public string FindIDfromConnectedPlayerID(GP_TBM_Match match)
    {
        string playerIDToFind = GooglePlayManager.Instance.player.playerId;
        int counter = 0;
        foreach (GP_Participant participant in match.Participants)
        {
            string id = participant.playerId;
            if (id.Contains(playerIDToFind))
            {
                // AN_PoupsProxy.showMessage("FindMyPlayerNumber in playerinformation", "found player number " + counter.ToString());
                TBM_Debug("FindMyPlayerNumber in TBM_Game/FindIDfromPlayerID", "found player number " + participant.id.ToString());
                return participant.id;
            }
            counter++;
        }
        Common.DebugLog("Warning", "Error", "DIDNT FIND PLAYER ID in TBM/FindIDFromPLayerID");
        return "ERROR_FindingID_Didnt_Find_ID";
    }

    void TBM_Debug(string name, string log)
    {
        Common.DebugLog(name, "TBM_GAME_EXAMPLE", log);
    }

    void DealWithMatchData(GP_TBM_Match match)
    {
        mMatch = match;

       // Common.DebugPopUp("Dealing with match nata", "Participant info " + mMatch.PendingParticipantId);
        if (mMatch.Data != null)
        {

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            string stringData = encoding.GetString(mMatch.Data, 0, mMatch.Data.Length);
            if (stringData.Length > 10)
            {
                Common.roundInformation.SetNewGameDataFromGooglePlay(mMatch.Data);
                // AndroidMessage.Create("match data not empty", "it is  " + stringData);
                StartDoingTurn(mMatch);
                LoadAllParticipantInfoStartRoutine();
                return;
            }
        }

        // Otherwise, this is the first player. Initialize the game state.
        initGame(mMatch);


        // Let the player take the first turn
        StartDoingTurn(mMatch);
        LoadAllParticipantInfoStartRoutine();
        //SetGameStateBased on gameState


    }

    void LoadAllParticipantInfoStartRoutine()
    {
        StartCoroutine(LoadAllParticipantsIntoUI());
    }
    bool loadingIcon = false;
    IEnumerator LoadAllParticipantsIntoUI()
    {
        int counter = 0;
        foreach (GP_Participant part in mMatch.Participants)
        {
            // AndroidMessage.Create("Participant", "Participant info " + part.DisplayName + " +" +  " id " + part.id + " pid " + part.playerId);
            loadingIcon = true;
            ParticipantInfoUI piui = icons[counter].gameObject.GetComponent<ParticipantInfoUI>();
            bool isMyP = false;
            bool isMyT = false;
            int myNumber = Common.roundInformation.FindNumberBasedOnID(part.id);
            bool isReady = Common.roundInformation.gameData.readyStates[myNumber];
            if (Common.playerInformation.myID == part.id)
            {
                isMyP = true;
            }
            if (mMatch.PendingParticipantId == part.id)
            {
                isMyT = true;
            }
            piui.setParticipantValues(part.id, part.playerId, part.DisplayName, isReady, isMyP, isMyT);
            Debug.Log("Set new piuiu" + counter.ToString() + " values are :" + "id " + part.id + " pid " + part.playerId + " dn " + part.DisplayName + " isr " + isReady + " ismp " + isMyP + "is myT" + isMyT);
            LoadParticipantIcon(counter);
            while (loadingIcon)
            {
                yield return new WaitForSeconds(0.1f);
            }
            counter++;

            string turnStatus = mMatch.TurnStatus.ToString();
            TBM_Debug("tunStatus", turnStatus);
            TBM_Debug("LastUpdated", mMatch.LastUpdatedTimestamp.ToString());
            TBM_Debug("LastUpdaterID", mMatch.LastUpdaterId.ToString());

        }
    }

    void LoadParticipantIcon(int index)
    {
        string ulr = mMatch.Participants[index].IconImageUrl;
        toChoose = index;
        Common.getImageFromURL.loadImage(ulr, this.gameObject, "IconLoadFinished");

    }
    public Image[] icons;
    int toChoose;
    void IconLoadFinished(Texture2D texture)
    {
        Common.SetTexture2DToImage(icons[toChoose], texture);
        loadingIcon = false;
    }

    public void ActiveMatchPressed()
    {

    }


    void StartDoingTurn(GP_TBM_Match match)
    {
        string id = FindIDfromConnectedPlayerID(match); //
        Common.playerInformation.SetMyPlayerInformationAndItsValues(id);
        Common.gameLoader.LoadSceneBasedOnState();  //Loads the wanted scene and changes UI.

        return;
        //string id=match.PendingParticipantId; //TODO CHECK ID.

        //   if (Common.playerInformation.playerNumber == -1)
        //  {

        //   Common.playerInformation.SetMyPlayerInformation();
        // Common.playerInformation.SetMyPlayerInformation(id);
        //}
        // hi.SetActive(true);
    }

    void unShowTurnUI()
    {
        hi.SetActive(false);
    }

    void unShowLoadingPopUp()
    {
        AndroidNativeUtility.HidePreloader();
    }

    void initGame(GP_TBM_Match match)
    {
        AndroidMessage.Create("Initializing match data ", " match data initialization");
        List<string> playerIDS = new List<string>();
        foreach (GP_Participant participant in match.Participants)
        {
            playerIDS.Add(participant.id); //TODO katso onko playerID vai joku toinen ID   
        }
        Common.roundInformation.InitializeGameData(playerIDS, match.Id);
        Common.cloudServiceMaster.CreateAlbumOnGameStarted();

    }
    public void LoadAllMatchersInfo()
    {
        //  GooglePlayTBM.a += ActionMatchDataLoadedFunction;
        AndroidMessage.Create("Loading all matches ", "loaad ");
        GooglePlayTBM.Instance.LoadAllMatchesInfo(GP_TBM_MatchesSortOrder.SORT_ORDER_MOST_RECENT_FIRST);

    }

    public void LoadActiveMatchesInfo()
    {
        AndroidMessage.Create("Loading active matches info ", "loaad ");
        //takas  GooglePlayTBM.ActionMatchDataLoaded += ActionMatchDataLoadedFunction;
        // GooglePlayTBM.ActionMatchesResultLoaded += ActionMatchesDataLoadedFunction;
        GooglePlayTBM.Instance.LoadMatchesInfo(GP_TBM_MatchesSortOrder.SORT_ORDER_MOST_RECENT_FIRST, GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_MY_TURN, GP_TBM_MatchTurnStatus.MATCH_TURN_STATUS_THEIR_TURN);
    }

    private void ActionMatchDataLoadedFunction(GP_TBM_LoadMatchResult result)
    {
        Debug_previous_method_name();
        AndroidMessage.Create("Loaded match data ", "loaad ");
        foreach (GP_Participant part in result.Match.Participants)
        {
            AndroidMessage.Create("Participant", "Participant info " + part.DisplayName + " +" + part.IconImageUrl + " id " + part.id + " pid " + part.playerId);
        }
    }

    List<GP_TBM_Match> matchDatasLoaded;
    private void ActionMatchesDataLoadedFunction(GP_TBM_LoadMatchesResult result)
    {

        AndroidMessage.Create("Loaded all matches", "will show all matches");
        bool loaded = false;
        List<GP_TBM_Match> matchDatas = new List<GP_TBM_Match>();
        foreach (KeyValuePair<string, GP_TBM_Match> dict in result.LoadedMatches)
        {
            //   AndroidMessage.Create("KEY VALUE PAIR ", "key "+dict.Key+" value mnumber"+dict.Value.MatchNumber.ToString());
            if (loaded == false)
            {
                //  DealWithMatchData(dict.Value);
            }
            loaded = true;
            matchDatas.Add(dict.Value);
            /* foreach (GP_Participant part in result.Match.Participants)
             {
                 AndroidMessage.Create("Participant", "Participant info " + part.DisplayName + " +" + part.IconImageUrl + " id " + part.id + " pid " + part.playerId);
             }*/
        }
        matchDatasLoaded = matchDatas;
        // matchUI.SetActive(true);
        activeMatchesGridHandler.CreateMatches(matchDatas);
    }

    public void MatchSelectedFromData(int counter)
    {
        GP_TBM_Match match = matchDatasLoaded[counter];
        DealWithMatchData(match);
        //   matchUI.SetActive(false);
    }

    private void ConncetButtonPress()
    {
        Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State.ToString());
        if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
        {
            DisconnectGooglePLay();
        }
        else
        {
            ConnectGooglePlay();
        }
    }
    public bool WaitingForGooglePlayResult = false;
    public void DisconnectGooglePLay()
    {
        if (WaitingForGooglePlayResult == false)
        {
            WaitingForGooglePlayResult = true;
            SA_StatusBar.text = "Disconnecting from Play Service...";
            GooglePlayConnection.Instance.Disconnect();
        }
    }

    public void ConnectGooglePlay()
    {
        if (WaitingForGooglePlayResult == false)
        {
            WaitingForGooglePlayResult = true;
            SA_StatusBar.text = "Connecting to Play Service...";
            GooglePlayConnection.Instance.Connect();
        }
    }







    private void DrawParticipants()
    {




    }


    void FixedUpdate()
    {
        DrawParticipants();
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            matchUI.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            matchUI.SetActive(false);
        }
        */
        DebugPanel.Log("TBMFIXED", "ERROR", Time.time.ToString());


        string title = "Connect";
        if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
        {
            title = "Disconnect";

            foreach (DefaultPreviewButton btn in ConnectionDependedntButtons)
            {
                btn.EnabledButton();
            }


        }
        else
        {
            foreach (DefaultPreviewButton btn in ConnectionDependedntButtons)
            {
                btn.DisabledButton();

            }
            if (GooglePlayConnection.State == GPConnectionState.STATE_DISCONNECTED || GooglePlayConnection.State == GPConnectionState.STATE_UNCONFIGURED)
            {

                title = "Connect";
            }
            else
            {
                title = "Connecting..";
            }
        }

        connectButton.text = title;
    }

    private void OnPlayerDisconnected()
    {
        SA_StatusBar.text = "Player Disconnected";
        playerLabel.text = "Player Disconnected";
        playerConnectedStatus.text= "Player Disconnected";
        avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
    }

    private void OnPlayerConnected()
    {
        SA_StatusBar.text = "Player Connected";
        playerLabel.text = GooglePlayManager.Instance.player.name;
        playerConnectedStatus.text = GooglePlayManager.Instance.player.name;
        Debug.Log("ON PLAYER CONNECTED");
        if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
        {
            Debug.Log("SETTING PLAYER ICON");
            if (GooglePlayManager.Instance.player.icon != null)
            {
                //GooglePlayManager.instance.Sho
                Debug.Log("FOUND ICON");
                avatar.GetComponent<Renderer>().material.mainTexture = GooglePlayManager.Instance.player.icon;
            }
        }
        // avatar.GetComponent<Renderer>().material.mainTexture = GooglePlayManager.Instance.player.icon; //TODO OTA POIS
        //GooglePlayManager.instance.player.id
        TBM_Debug("OnPlayerCOnnected", "player id " + GooglePlayManager.Instance.player.playerId.ToString());
        Common.playerInformation.setGPID(GooglePlayManager.Instance.player.playerId);
    }

    private void OnConnectionResult(GooglePlayConnectionResult result)
    {
        WaitingForGooglePlayResult = false;
        SA_StatusBar.text = "ConnectionResul:  " + result.code.ToString();
        Debug.Log(result.code.ToString());
    }

}
