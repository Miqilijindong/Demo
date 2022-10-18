using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象类
///     1.不允许被实例化
///     2.支持构造函数
///     3.可以被抽象类继承 
///     4.静态构造函数只执行一次，其他构造函数则根据不同实例，分别再次调用
///     5.运行使用虚函数virtual
///     6.若函数声明为abstract，则不允许包含函数体；子类必须覆盖父类的该方法
/// </summary>
public abstract class Abstract : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void func1();
}
