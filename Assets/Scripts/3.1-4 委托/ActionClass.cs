using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/**
 * Action委托
 *      Action<T>是.NET Framework内置的泛型委托，可以使用Action<T>委托哦以参数形式传递方法，而不用显示声明自定义的委托。
 */
public class ActionClass : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // 初始化
        Action Action = show;
        // 执行函数
        Action();
        //action.Invoke();

        Action<int, int> action1 = show1;
        action1.Invoke(1, 2);

        //Action<int, int> action2 = show2;// Action委托至少0个参数，至多16个参数，无返回值，表示无参，无返回值的委托。

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        Debug.Log("调用show方法");
    }

    public void show1(int a, int b)
    {
        Debug.Log("调用show1方法" + (a + b));
    }

    public int show2(int a, int b)
    {
        return 1000;
    }
}
