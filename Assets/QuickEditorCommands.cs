using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;


//[ExecuteInEditMode]
public class QuickEditorCommands : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      //  Debug.Log("update");
	}
}


 public static class MyMenuCommands
{

    [MenuItem("My Commands/Special Command #0")]
    static void SpecialCommand()
    {
        LoadScene(0);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #5")]
    static void SpecialCommand5()
    {
        LoadScene(0);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #1")]
    static void SpecialCommand1()
    {
        LoadScene(1);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #2")]
    static void SpecialCommand2()
    {
        LoadScene(2);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #3")]
    static void SpecialCommand3()
    {
        LoadScene(3);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #4")]
    static void SpecialCommand4()
    {
        LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #R")]
    static void SelectRoundInformation()
    {
        Debug.Log("Selecting roundInformation");
        Selection.activeGameObject = GameObject.FindObjectOfType<RoundInformation>().gameObject;
        //LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #T")]
    static void SpecialCommandT()
    {
        Debug.Log("Selecting roundInformation");
        Selection.activeGameObject = GameObject.FindObjectOfType<TestDatas>().gameObject;
        //LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }

    [MenuItem("My Commands/Special Command #P")]
    static void SpecialCommandP()
    {
        Selection.activeGameObject = GameObject.FindObjectOfType<PlayerInformation>().gameObject;
        //LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }
    [MenuItem("My Commands/Special Command #G")]
    static void SpecialCommandG()
    {
        Selection.activeGameObject = GameObject.FindObjectOfType<TBM_Game_Example>().gameObject;
        //LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }
    [MenuItem("My Commands/Special Command #S")]
    static void SpecialCommandS()
    {
        Selection.activeGameObject = GameObject.FindObjectOfType<ShowWinnerMaster>().gameObject;
        //LoadScene(4);
        //Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
    }




    static void LoadScene(int scene)
    {
        GameLoader loader = GameObject.FindObjectOfType<GameLoader>();
        loader.ChangeUIBasedOnState(scene);
    }
}
#endif
