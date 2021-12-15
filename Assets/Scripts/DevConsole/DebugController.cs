using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugController : MonoBehaviour
{
    bool showConsole;

    string input;

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
    }

    private void Update()
    {
        OnGUI();
        if (!showConsole)
            return;

        float y = 0f;

        if (Input.GetKey(KeyCode.F8))
        {
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }
    }

    private void OnGUI()
    {
        if (!showConsole)
            return;

        float y = 0f;

        if (Input.GetKey(KeyCode.F8))
        {
            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }
    }
}
