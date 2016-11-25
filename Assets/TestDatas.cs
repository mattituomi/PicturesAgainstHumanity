using UnityEngine;
using System.Collections;

public class TestDatas : MonoBehaviour {
    public GameDataGooglePlay showWinnerTestData;
	// Use this for initialization
	void Start () {

    #if UNITY_EDITOR
            Debug.Log("Unity Editor");
            Invoke("StartTest", 1f);
    #endif

    }

    void StartTest()
    {
        Debug.Log("REMOVE THIS FROM NORMAL GAME THIS SHOULD NEVER BE CALLED ON NORMAL VERSIOn");
        Common.showWinnerTestData = showWinnerTestData;
        Common.roundInformation.gameData = showWinnerTestData;
        Common.gameLoader.LoadSceneBasedOnState();
        TestDelegates td = FindObjectOfType<TestDelegates>();
       // td.MethodWithCallback(1, 2, DelegateMethod);
        
    }

    public void DelegateMethod(string message)
    {
        Debug.Log("Delegate message from TestData");
        Common.DebugLog("del", "del", message);
    }



    string filePathTest = "Assets//Resources//4.png";//"C:\\Users\\Matti\\Documents\\HoorayProto\\Pictures against humanity\\Assets\\Resources";
    string filePathTest1 = "Assets//Resources//5.png";
    string filePathTest2 = "Assets//Resources//3.png";

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Common.roundInformation.gameData = showWinnerTestData;
            Common.gameLoader.LoadSceneBasedOnState();
           // TestUploadPics();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Common.playerInformation.ReadyButtonTest();
           // Common.TBM_game.CreateParticipantsInfos(showWinnerTestData.playerAmount);
            //Common.roundInformation.ChangeGameState();
            //Common.gameLoader.LoadSceneBasedOnState();
        }
	}

    void TestUploadPics()
    {
        string filePath = System.IO.Path.GetFullPath(filePathTest);
        string filePath1 = System.IO.Path.GetFullPath(filePathTest1);
        string filePath2 = System.IO.Path.GetFullPath(filePathTest2);
        string userName="testeri";
        string albumName="AATest";
       // Common.galleryManager.CreateAlbum(userName, albumName);
        Common.galleryManager.AddPhoto(userName, albumName, "test4", "CasuallyTesting", filePath);
        Common.galleryManager.AddPhoto(userName, albumName, "test5", "CasuallyTesting", filePath1);
        Common.galleryManager.AddPhoto(userName, albumName, "test3", "CasuallyTesting", filePath2);
    }
}
