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
        //Del handler = DelegateMethod;
       // MethodWithCallback(1, 2, handler);
    }


    public delegate void Del(string message);

    public void MethodWithC6allback(int param1, int param2, Del callback)
    {
        callback("The number is: " + (param1 + param2).ToString());
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
