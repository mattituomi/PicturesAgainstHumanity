using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActiveMatchesGridHandler : MonoBehaviour {
    public GameObject gameInfoPrefab;
    

	// Use this for initialization
	void Start () {
        testCreateMatches();
        lastParent = Common.usefulFunctions.findLastParent(this.transform);
        CancelGrid();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void testCreateMatches()
    {
        for(int n=0; n<4; n++)
        {
            GameObject created = (GameObject)Instantiate(gameInfoPrefab, this.gameObject.transform);
            Text textField = created.GetComponentInChildren<Text>();
            textField.text = n.ToString() + " " + "aika";
        }
    }
    public List<GameObject> createdCells;
    public Transform lastParent;
    public void CreateMatches(List<GP_TBM_Match> matches)
    {
        
        lastParent.gameObject.SetActive(true);
        createdCells = new List<GameObject>();
        int counter = 0;
        foreach(GP_TBM_Match match in matches)
        {
            GameObject created = (GameObject)Instantiate(gameInfoPrefab, this.gameObject.transform);
            createdCells.Add(created);
            Text textField=created.GetComponentInChildren<Text>();
            textField.text = counter.ToString()+" "+match.CreationTimestamp.ToString();
            counter++;
        }
    }

    public void MatchButtonPressed(Text text)
    {
        string index = text.text[0].ToString();
        int ind = int.Parse(index);
        Debug.Log("Match selected " + ind.ToString());
        Common.TBM_game.MatchSelectedFromData(ind);
        CancelGrid();
    }

    public void CancelGrid()
    {
        foreach(GameObject cell in createdCells)
        {
            Destroy(cell);
        }
        lastParent.gameObject.SetActive(false);
    }

    public void MatchButtonPressed2()
    {

    }
}
