using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowScreenPos : MonoBehaviour
{
    private void OnGUI()
    {
        GUIStyle gUIStyle = new GUIStyle();
        gUIStyle.fontSize = 50;
        gUIStyle.normal.textColor = Color.black;
        float v = Input.mousePosition.y;
        GUI.Label(new Rect(Input.mousePosition.x, Screen.height - v, 100, 100), Input.mousePosition.x + "," + v, gUIStyle);
    }
}
