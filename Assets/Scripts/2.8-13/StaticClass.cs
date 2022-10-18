using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// const
///     在类内声明的const常量
///     1.在外部访问时，必须通过类名进行访问
///     2.只能在声明时初始化，不允许在任何其他地方对其初始化(包括构造函数)
///     3.在某种程度，被const修饰的变量(常量)，不可变值
/// readonly
///     在类内声明的readonly常量
///     1.readonly const,不可共同修饰一个数据类型(基础数据类型+自定义数据类型)
///     2.可以被类的实例进行访问
///     3.可以在构造函数和声明的时候赋值
/// readonly
/// 变量访问修饰符的控制
/// 
/// 静态类
///     1.静态类不允许有实例构造函数，只允许存在一个静态构造函数
///     2.无法创建静态类的实例化
///     3.静态类，内部成员必须是静态成员+常量成员
///     4.静态无法作为基类派生
///     
/// </summary>
public class StaticClass : MonoBehaviour
{
    public class MyClass {
        public const int mValue = 20;
        public readonly int mValue2 = 20;

    }

    private void Start()
    {
        MyClass myClass = new MyClass();
        // const 必须要通过类才能访问
        Debug.Log(MyClass.mValue);
        // readonly 类的实例可以访问
        Debug.Log(myClass.mValue2);
    }
}
