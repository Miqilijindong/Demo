using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 抽象类
 *      1.不允许被实例化
 *      2.支持构造函数
 *      3.抽象类可继承抽象类
 *      4.静态构造函数 只执行一次；但是其他的构造函数，根据不同实例，分别再次调用
 *      5.运行使用virtual虚函数
 *      6.若函数声明为abstract，则不能有函数体，且子类必须override该方法
 */

public abstract class AbstractClass1
{
    public AbstractClass1()
    {
        Debug.Log("抽象类调用构造函数");
    }

    static AbstractClass1()
    {
        Debug.Log("抽象类调用静态构造函数");
    }
}

public abstract class AbstractClass2 : AbstractClass1
{
    public AbstractClass2 ()
    {
        Debug.Log("抽象类2调用构造函数");
    }

    public virtual void test ()
    {
        Debug.Log("调用抽象类2的test函数");
    }
    public abstract void AbstractFunc();
}

public class AbstractClass3 : AbstractClass2
{
    public AbstractClass3()
    {
    }

    public override void AbstractFunc()
    {
        Debug.Log("调用抽象类3的抽象函数");
    }

    public override void test()
    {
        base.test();
    }

}
public class AbstractClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AbstractClass3 abstractClass3 = new AbstractClass3();
        AbstractClass3 abstractClass3_test = new AbstractClass3();

        abstractClass3.test();
        abstractClass3.AbstractFunc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
