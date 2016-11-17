using UnityEngine;
using System.Collections;

public class TestDelegates : MonoBehaviour {
    public delegate void UpdateDelegate();
    public UpdateDelegate updateDelegates;
    // Use this for initialization


        //Muistiin delegateista. Jos delegate lisätään se täytyy myös poistaa. Jos monta samaa lisätään tulostetaan ne moneen kertaan ja jos halutaan ne poistaa täytyy poistaa metodi moneen kertaan.

    void Start()
    {
        StartCoroutine(LateStart(1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        updateDelegates += Common.gameLoader.printTestWhooo;
        //RunUpdateDelegate();
        updateDelegates += Common.printTestWhooo;

       // RunUpdateDelegate();
        updateDelegates += Common.gameLoader.printTestWhooo;
        updateDelegates += Common.printTestWhooo;
        updateDelegates += Common.printTestWhooo;
        updateDelegates += Common.printTestWhooo;
        updateDelegates -= Common.gameLoader.printTestWhooo;
        updateDelegates -= Common.printTestWhooo;
        //updateDelegates -= Common.gameLoader.printTestWhooo;
        RunUpdateDelegate();
    }


    void RunUpdateDelegate()
    {
        if (updateDelegates != null)
            updateDelegates();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
