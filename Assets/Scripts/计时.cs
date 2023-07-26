using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class 计时 : MonoBehaviour
{

    private float timer = 0f;
    private int h = 0;
    private int m = 0;
    private int s = 0;
    private string timeStr = string.Empty;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f) { s++; timer = 0; }
        if (s >= 60) { m++; s = 0; }
        if (m >= 60) { h++; m = 0; }
        if (h >= 99) { h = 0; }
    }
    void OnGUI()
    {
        string v = DateTime.Now.ToString();
        GUI.Label(new Rect(10, 25, 500, 500), v);

        string v1 = DateTime.Now.ToString();
        GUI.Label(new Rect(10, 40, 500, 500), v1);
        string v2 = DateTime.Now.ToString("d");
        GUI.Label(new Rect(10, 55, 500, 500), v2);
        string v3 = DateTime.Now.ToString("D");
        GUI.Label(new Rect(10, 70, 500, 500), v3);
        string v4 = DateTime.Now.ToString("f");
        GUI.Label(new Rect(10, 85, 500, 500), v4);
        string v5 = DateTime.Now.ToString("F");
        GUI.Label(new Rect(10, 100, 500, 500), v5);

        string v6 = DateTime.Now.ToString("g");
        GUI.Label(new Rect(10, 115, 500, 500), v6);
        string v7 = DateTime.Now.ToString("G");
        GUI.Label(new Rect(10, 130, 500, 500), v7);
        string v8 = DateTime.Now.ToString("t");
        GUI.Label(new Rect(10, 145, 500, 500), v8);
        string v9 = DateTime.Now.ToString("T");
        GUI.Label(new Rect(10, 160, 500, 500), v9);
        string v0 = DateTime.Now.ToString("u");
        GUI.Label(new Rect(10, 175, 500, 500), v0);
        string v11 = DateTime.Now.ToString("U");
        GUI.Label(new Rect(10, 190, 500, 500), v11);
        string v12 = DateTime.Now.ToString("m");
        GUI.Label(new Rect(10, 205, 500, 500), v12);
        string v13 = DateTime.Now.ToString("M");
        GUI.Label(new Rect(10, 220, 500, 500), v13);
        string v14 = DateTime.Now.ToString("r");
        GUI.Label(new Rect(10, 235, 500, 500), v14);
        string v15 = DateTime.Now.ToString("R");
        GUI.Label(new Rect(10, 250, 500, 500), v15);
        string v16 = DateTime.Now.ToString("y");
        GUI.Label(new Rect(10, 265, 500, 500), v16);
        string v17 = DateTime.Now.ToString("Y");
        GUI.Label(new Rect(10, 280, 500, 500), v17);
        string v18 = DateTime.Now.ToString("o");
        GUI.Label(new Rect(10, 295, 500, 500), v18);
        string v19 = DateTime.Now.ToString("O");
        GUI.Label(new Rect(10, 310, 500, 500), v19);
        string v10 = DateTime.Now.ToString("s");
        GUI.Label(new Rect(10, 325, 500, 500), v10);

        timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", h, m, s);
        GUI.Label(new Rect(10, 10, 500, 500), timeStr);
    }

    Stopwatch sw;
    public void TestTime()
    {
        sw = new Stopwatch();
        sw.Start();
    }

    public void TestTimeStop()
    {
        sw.Stop();
        Debug.Log(sw.ElapsedMilliseconds);
    }
}