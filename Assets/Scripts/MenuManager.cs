using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text debug;
    public void ButtonClicked(string buttonName)
    {
        if (buttonName == "Exit")
        {
            Application.Quit();
        }
        else
        {
            OpenBuild(buttonName);
        }
    }

    private void OpenBuild(string name)
    {
        string buildPath = $"{Application.streamingAssetsPath}/{name}/Problem Solving.exe";
        debug.text = "Opening " + name;

        try
        {
            System.Diagnostics.Process.Start(buildPath);
        }
        catch (System.Exception e)
        {
            debug.text = "Open failed\n" + e.Message;
        }
    }
}
