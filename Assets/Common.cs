using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public static class Common
{
    public static PlayerDatas playerDatas;
    public static ParseGuestion parseGuestion;
    public static Guestions guestions;
    public static GuestionPicker guestionPicker;
    public static InfoGUI infoGUI;
    public static GameMaster gameMaster;
    public static CameraMaster cameraMaster;
    public static App42 app42;
    public static CloudImageManager cloudImageManager;
    public static RoundInformation roundInformation;
    public static PlayerInformation playerInformation;
    public static PlayerPrefabHandler playerPrefabHandler;
    public static GameLoader gameLoader;
    public static GetImageFromURL getImageFromURL;
    public static CloudServiceMaster cloudServiceMaster;
    public static Gallery galleryManager;
    public static LevelLoader levelLoader;
    public static ShowWinnerMaster showWinnerMaster;
    public static TBM_Game_Example TBM_game;
    public static UsefulFunctions usefulFunctions;



    public static bool valuesLoaded = false;
    

    public static void SetTexture2DToImage(Image img, Texture2D result)
    {
        Rect rec = new Rect(0, 0, result.width, result.height);
        img.sprite = Sprite.Create(result, rec, new Vector2(0.5f, 0.5f), 100);
    }

    public static void DebugPopUp(string debugLog)
    {
        DebugPopUp("DEBUGLogPopUP", debugLog);
       // AN_PoupsProxy.showMessage("DEBUGPOPUP", debugLog);
    }
    public static void DebugPopUp(string title,string log)
    {
        Debug.Log("Dpop " + title + " " + log);
        AndroidMessage.Create(title, log);
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            foreach(Transform ct in tr)
            {
                if (ct.tag == tag)
                {
                    return ct.gameObject;
                }
            }
            Debug.Log(tr.name);
            if (tr.tag == tag)
            {
                return tr.gameObject;
            }
        }
        return null;
    }

    public static Image FindPickedImageFromChild(GameObject parent)
    {
        Debug.Log("Finding go from parent :" + parent.name);
        GameObject go = FindGameObjectInChildWithTag(parent, "PickedImage");
        Image img = go.GetComponent<Image>();
        return img;
    }

    //DEBUGGAUS OHJELMA JOTA KÄYTETÄÄN, KÄYTÄ TÄTÄ AIAN KUN heität debuggeja
    public static void DebugLog(string name,string cathegory,string log)
    {
        DebugPanel.Log(name, cathegory,log);
    }
}
