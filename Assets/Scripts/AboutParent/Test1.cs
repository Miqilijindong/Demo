using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 测试中发现的问题一:继承了Monobehaviour的类，并不止构造函数，而是整个构造过程都会被unity灵性执行多次
/// 所以像是这种测试父类构造函数的，建议写个测试方法去调用构造
/// 问题二:像是这种测试问题，尽量用addComponent或者removeComponent来调用实例化，用new的话会报错显示不允许
/// </summary>
public class Test1 : MonoBehaviour
{
    AboutParentClass aboutParentClass1;
    AboutParentClass aboutParentClass2;
    AboutParentClass aboutParentClass3;

    AboutParentClassWithBase aboutParentClassWith1;
    AboutParentClassWithBase aboutParentClassWith2;
    AboutParentClassWithBase aboutParentClassWith3;

    AboutParentClassWithThis aboutParentClassThis1;
    AboutParentClassWithThis aboutParentClassThis2;
    AboutParentClassWithThis aboutParentClassThis3;
    void Start()
    {
        print("---------------------------------------with not Base");
        aboutParentClass1 = new AboutParentClass();
        aboutParentClass2 = new AboutParentClass(1);
        aboutParentClass3 = new AboutParentClass(1, 2);


        print("---------------------------------------with Base");
        aboutParentClassWith1 = new AboutParentClassWithBase();
        aboutParentClassWith2 = new AboutParentClassWithBase(1);
        aboutParentClassWith3 = new AboutParentClassWithBase(1, 2);

        print("---------------------------------------with this");
        aboutParentClassThis1 = new AboutParentClassWithThis();
        aboutParentClassThis2 = new AboutParentClassWithThis(1);
        aboutParentClassThis3 = new AboutParentClassWithThis(1, 2);

    }
}
