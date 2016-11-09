using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadSigningAlias
{

    static PreloadSigningAlias()
    {
        PlayerSettings.Android.keystorePass = "mattituomi1990";
        PlayerSettings.Android.keyaliasName = "matti";
        PlayerSettings.Android.keyaliasPass = "mattituomi1990";
    }

}