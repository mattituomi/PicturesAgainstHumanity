using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Guestions : MonoBehaviour {

	// Use this for initialization
	void Start () {
        readed = Read(fileNameToLoad);
        guestions = new List<string>();
        guestions=ParseFile(readed);
        if (Common.guestions==null)
        {
            Common.guestions = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
	}

    List<string> ParseFile(string toParse)
    {
        List<string> parsedNames = new List<string>();
        string[] words = toParse.Split(';');
        allGuestions = new string[words.Length];
        int counter = 0;
        foreach (string word in words)
        {
            string wordtrimmed = word.Trim();
           // Debug.Log("Sana parsessa on " + word);

            if (wordtrimmed.Length > 3)
            {
                parsedNames.Add(wordtrimmed);
                string newWord = string.Copy(wordtrimmed);
                guestions.Add(newWord);
                allGuestions[counter] = newWord;
                counter++;
            }
            
        }

        test3 = parsedNames;
        return parsedNames;
    }
    List<string> test3;
	// Update is called once per frame
	void Update () {
    }
    public string readed;
    public List<string> guestions;
    public string fileNameToLoad = "guestions";
    public string[] allGuestions;
    public static string Read(string filename)
    {
        //Load the text file using Reources.Load
        TextAsset theTextFile = Resources.Load<TextAsset>(filename);

        //There's a text file named filename, lets get it's contents and return it
        if (theTextFile != null)
            return theTextFile.text;

        //There's no file, return an empty string.
        return string.Empty;
    }


}
