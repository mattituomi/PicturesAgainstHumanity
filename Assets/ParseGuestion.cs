using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class specialCharacter
{
    string variableName = "x";
    string[] options = new string[3]{ "player1", "player2","player3" };
}

public class ParseGuestion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //  Debug.Log(Parse("SDASDASD {x} asdasdas"));
        /*
        List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
        List<string> stringList = new List<string>() { "1", "2" };
        List<GameObject> gos = new List<GameObject>();
        gos.Add(this.gameObject);
        gos.Add(this.gameObject);
        gos.Add(this.gameObject);
        Debug.Log(intList.GetString());
        Debug.Log(stringList.GetString());
        Debug.Log(gos.GetString());
        */
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public static string Parse(string guestion)
    {
        ParseSpecialCharacter(ref guestion);
        return guestion.Trim();
    }

    private static void ParseSpecialCharacter(ref string guestion)
    {

        if (guestion.Contains("{x}"))
        {
            Debug.Log("GUESTION CONTAINED XXX");
                string newChar = PickCharacter();
            guestion=guestion.Replace("{x}", newChar);
        }

    }

    private static string PickCharacter()
    {
        /*
        PlayerDatas data = Common.playerDatas;
        List<PlayerInformation> infos = data.playerInfoScripts;
       PlayerInformation randomInfo = infos.RandomElement();
       */
        return Common.roundInformation.gameData.playerIDS.RandomElement();
        //return randomInfo.name;
    }


}

public static class ColectionExtension
{

    public static T RandomElement<T>(this IList<T> list)
    {
        return list[Random.Range(0,list.Count)];
    }




    public static string GetString<T>(this IList<T> list)
    {
        string result = "";
        int counter = 0;
        foreach(T element in list)
        {
            result += " " + counter.ToString() + ":" + element.ToString() + " ";
            counter++;
        }
        
        return result; 
    }

    public static T RandomElement<T>(this T[] array)
    {
        return array[Random.Range(0,array.Length)];
    }

}
