using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LobbyGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame

    public Button connectButton;
    public Button findMatch;
    public Button startMatch;







    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
             GoNextMenu();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GoBackMenu();
        }
    }
    public string playerName = "player1";
    public Text ConnectedStatus;
    public int menuGOer = 0;

    public void ActivateMenu(int index)
    {

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        this.transform.GetChild(index).gameObject.SetActive(true);
        menuGOer = index;
    }

    public void Initialize(string newPlayer, int newplayerNumber)
    {

        ActivateMenu(menuGOer);

    }





    string FindFirstDigitAndChangeIt(string str, int newDigit)
    {
        int index = str.IndexOfAny(new char[]
            { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });
        return str.Replace(str[index], newDigit.ToString()[0]);
    }


    public void GoNextMenu()
    {
        menuGOer++;
        if (menuGOer == transform.childCount)
        {
            EndOfMenus();
            return;
        }
        ActivateMenu(menuGOer);
    }
    public void GoBackMenu()
    {
        menuGOer--;
        if (menuGOer == transform.childCount)
        {
            EndOfMenus();
            return;
        }
        ActivateMenu(menuGOer);
    }



    void ChangeBetText(int newBet)
    {
        string newText = FindFirstDigitAndChangeIt(ConnectedStatus.text, newBet);
        ConnectedStatus.text = newText;
    }



    void EndOfMenus()
    {
        StartGame();
    }

    void StartGame()
    {
        int sceneToLoad = (int)definedLevels.game;
        Common.levelLoader.LoadLevel(sceneToLoad);
        // SceneManager.LoadScene(sceneToLoad);

    }



    void SetAllPlayerNameTittle(string newTittle)
    {
        string tagToFind = "PlayerNameTittle";
        Transform[] allChilds = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChilds)
        {
            if (child.tag == tagToFind)
            {
                Text textToChange = child.GetComponentInChildren<Text>();
                textToChange.text = newTittle;
            }
        }
    }


}

