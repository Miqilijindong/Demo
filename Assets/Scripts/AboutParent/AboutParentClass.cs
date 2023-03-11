using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关于父类与子类之间的关系
/// </summary>
public class AboutParentClass : ParentClass
{
    #region 像是这种构造函数，默认就是调用分类的无参构造函数
    public AboutParentClass()
    {
        print("AboutParentClass() : base(){}  has no parameter");
    }

    public AboutParentClass(int a)
    {
        print("AboutParentClass(int a) {} and a = " + a);
    }

    public AboutParentClass(int a, int b)
    {
        print("AboutParentClass(int a, int b) {} and a + b = " + (a + b));
    }
    #endregion
}

public class AboutParentClassWithBase : ParentClass
{
    #region 像是这种构造函数，base就是调用对应父类的构造函数
    public AboutParentClassWithBase() : base()
    {
        print("AboutParentClassWithBase() : base(){}  has no parameter");
    }

    public AboutParentClassWithBase(int a) : base(a)
    {
        print("AboutParentClassWithBase(int a) {} and a = " + a);
    }

    public AboutParentClassWithBase(int a, int b) : base(a, b)
    {
        print("AboutParentClassWithBase(int a, int b) {} and a + b = " + (a + b));
    }
    #endregion
}

public class AboutParentClassWithThis : ParentClass
{
    #region 像是这种构造函数，this就是调用本类的构造函数
    public AboutParentClassWithThis() : base()
    {
        print("AboutParentClassWithThis() : base(){}  has no parameter");
    }

    public AboutParentClassWithThis(int a) : this()
    {
        print("AboutParentClassWithThis(int a) {} and a = " + a);
    }

    public AboutParentClassWithThis(int a, int b) : this(a)
    {
        print("AboutParentClassWithThis(int a, int b) {} and a + b = " + (a + b));
    }
    #endregion
}