using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Caller : MonoBehaviour
{
    /// <summary>
    /// 返回调用者信息
    /// </summary>
    /// <param name="message">请求消息</param>
    /// <param name="pathName">调用者路径</param>
    /// <param name="name">调用者方法名</param>
    /// <param name="line">调用者行数</param>
    [DebuggerStepThrough]// debug会自动跳过这一个方法
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
