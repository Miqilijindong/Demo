using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class AboutWhereClass : MonoBehaviour
{
    /// <summary>
    /// where�Ĺ���:���ƺ�����κͷ�������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public void M<T>(T? item) where T : struct
    {
        print(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        MyClass<UsingEnum<TestEnum>, TestStruct2> myClass = new MyClass<UsingEnum<TestEnum>, TestStruct2>();
        TestStruct2 testStruct = new TestStruct2();
        M<TestStruct2>(testStruct);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public enum TestEnum
{
    a, b
}

public struct TestStruct2
{
    public int a;
    public int b;
}

public class UsingEnum<T> where T : Enum { }

public class UsingDelegate<T> where T : Delegate { }

public class Multicaster<T> where T : MulticastDelegate { }

/// <summary>
/// where����:��������
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="U"></typeparam>
class MyClass<T, U>
    where T : class
    where U : struct
{ }