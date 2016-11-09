using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum definedLevels { menu, game}

public class LevelLoader : MonoBehaviour {

    // Use this for initialization
    public delegate void UpdateDelegate();

    public UpdateDelegate updateDelegates;

    // Update is called once per frame

    void Awake()
    {
        if (Common.levelLoader)
        {
            Destroy(this.gameObject);
        }
        else {
            Common.levelLoader= this;
            //DontDestroyOnLoad(this.gameObject);
        }

    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("OLL: " + level);

        if (updateDelegates != null)
            updateDelegates();
    }


    public void LoadLevel(int level)
    {
        Debug.Log("Loading level " + level);
        SceneManager.LoadScene(level);
    }

    public void LoadMenu()
    {
        Debug.Log("LOAD MENU");
        LoadLevel((int)definedLevels.menu);
    }

    public void LoadGame()
    {
        Debug.Log("Load GAME");
        LoadLevel((int)definedLevels.game);
    }

}
