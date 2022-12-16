using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Caller : MonoBehaviour
{
    /// <summary>
    /// ���ص�������Ϣ
    /// </summary>
    /// <param name="message">������Ϣ</param>
    /// <param name="pathName">������·��</param>
    /// <param name="name">�����߷�����</param>
    /// <param name="line">����������</param>
    [DebuggerStepThrough]// debug���Զ�������һ������
    public static void ShowMessage(string message, [CallerFilePath] string pathName = "null", [CallerMemberName] string name = "null" , [CallerLineNumber] int line = 0)
    {
        Debug.Log(message);
        Debug.Log(pathName);
        Debug.Log(name);
        Debug.Log(line);
    }

    void Start()
    {
        ShowMessage("test1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
