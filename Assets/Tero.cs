using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tero : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<string> testiLista = new List<string>();
        testiLista.Add("1");
        testiLista.Add("2");
        testiLista.Add("3");
        testiLista.Add("4");
        testiLista.Add("5");
        aloitus(3, testiLista);
	}
    void function(int howManyToGoTrough, List<string> lista)
    {
        int size = lista.Count;
        for (int n = size - howManyToGoTrough; n < size; n++)
        {
            //TEE JOTAIN
        }
    }

    public void aloitus(int howMany, List<string> lista)
    {
        recursio(lista.Count - howMany, lista);
    }

    void recursio(int n, List<string> lista)
    {
        if (n < lista.Count)
        {
            //Lopeta
        }
        Debug.Log("N on " + n.ToString());
        //tee jotain
        n = n + 1;
        recursio(n, lista);
    }
    // Update is called once per frame
    void Update () {
	
	}
}
