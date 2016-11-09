using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuestionPicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Common.guestionPicker == null)
        {
            Common.guestionPicker = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public string PickNewGuestion()
    {
        string guestion=Common.guestions.guestions.RandomElement();
        string parsedGuestion=ParseGuestion.Parse(guestion);
        return parsedGuestion;
    }
}
