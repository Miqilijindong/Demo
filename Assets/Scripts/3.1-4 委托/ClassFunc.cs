using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 系统内置func委托
 *      System.Func可以不带参数， 但是必须带一个返回值
 *      System.Func 若是调用的多个泛型的委托定义，最后的参数的数据类型是函数的返回值类型，需要保持一致；非最后一个泛型T的声明，需要与实现函数的参数个数及类型保持一致
 */
public class ClassFunc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Func<string> func = show;
        Debug.Log(func());

        // 最后一个泛型是返回类型，非最后一个泛型T要与入参一致
        Func<int, string> func1 = show1;
        Debug.Log(func1(1000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string show()
    {
        return "调用show函数";
    }

    public string show1(int a)
    {
        return a.ToString();
    }
}
