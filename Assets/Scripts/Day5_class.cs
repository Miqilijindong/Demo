using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 1.const:常量
 *      在类内声明的const常量
 *      (1)外部访问时，必须通过类名进行访问；但不可修改值
 *      (2)只能在声明时初始化，不允许任何其他地方对其初始化(包括构造函数)
 *      (3)在某种程度，被const修饰的变量(常量)，不可变值
 * 2.readonly:只读
 *      在类内声明的readonly常量
 *      (1)readonly const，不可共同修饰一个类型(基本数据类型+自定义数据类型)
 *      (2)readonly修饰的类型，可以被类的势力进行访问；但不可修改值
 *      (3)readonly的初始化，只能发生在构造函数或者生命中。
 * 3.变量访问修饰符的控制
 *      (1)可通过访问修饰符构成的语法快，来实现类似外部只读的效果get set，一级学习下value赋值和访问代码的流程
 * 4.静态构造函数
 *      (1)静态构造函数，不需要增加访问修饰符
 *      (2)静态构造函数，无论多少实例 都只被调用一次，而且只被系统自动调用一次
 * 静态类:
 *      (1)静态类不允许有实例构造函数，只允许存在一个静态构造函数，(谨记:静态类的静态构造函数，经测试发现，并不会执行)
 *      (2)静态类，不允许被实例化
 *      (3)静态类，内部成员，必须是静态成员/常量
 *      (4)静态类无法被继承
 */



/*
 * 静态类
 */
static class Day5_test
{
    // 静态类不允许有实例
    public static int istaticValue = 0;
}

public class Day5_class : MonoBehaviour
{
    public const int ivalue = 1;
    public readonly int ivalue_readOnly = 2;
    public static int d = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ivalue);
        Debug.Log(ivalue_readOnly);
        Day5_test2 day5_test2 = new Day5_test2();
        int a = day5_test2.iValueGet;

        day5_test2.ivalue_4 = 5;
        Debug.Log(day5_test2.ivalue_4);

        int c = Day5_test3.value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public Day5_class () {
    //    ivalue_readOnly = 20;
    //    Debug.Log("readOnly能在构造函数里赋值");
    //}

    public class Day5_test2
    {
        Day5_class demo = new Day5_class();
        //int a = demo.d;
        public int iValueGet { get; private set; }// get/set方法

        public int ivalue_4
        {
            get
            {
                return ivalue4;
            }
            set
            {
                ivalue4 = value;
            }
        }

        private int ivalue4;
    }

    public static class Day5_test3
    {
        public static int value;

        // 静态构造函数只会被调用一次，且是系统自动调用
        static Day5_test3()
        {
            Debug.Log("静态构造函数执行一次");
        }
    }
}
