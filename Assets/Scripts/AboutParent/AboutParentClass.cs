using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ڸ���������֮��Ĺ�ϵ
/// </summary>
public class AboutParentClass : ParentClass
{
    #region �������ֹ��캯����Ĭ�Ͼ��ǵ��÷�����޲ι��캯��
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
    #region �������ֹ��캯����base���ǵ��ö�Ӧ����Ĺ��캯��
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
    #region �������ֹ��캯����this���ǵ��ñ���Ĺ��캯��
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