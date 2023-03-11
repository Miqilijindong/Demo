using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 测试中发现的问题一:继承了Monobehaviour的类，并不止构造函数，而是整个构造过程都会被unity灵性执行多次
/// 所以像是这种测试父类构造函数的，建议写个测试方法去调用构造。可以去看看Assets/Scripts/AboutParent/Test1.cs的代码
/// </summary>
public class ParentClass : MonoBehaviour
{
    public ParentClass()
    {
        // 无参构造函数
        print("ParentClass has no parameter");
    }

    public ParentClass(int a)
    {
        print("ParentClass(int a) has a parameter");
    }

    public ParentClass(int a, int b)
    {
        print("ParentClass(int a, int b) has a and b parameter");
    }
}
