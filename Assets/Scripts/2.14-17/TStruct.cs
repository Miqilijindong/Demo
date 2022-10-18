using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 结构体
 * 
 * 结构体与类的区别
 *  相同点：
 *      1.静态构造函数都支持
 *      2.自定义函数都支持
 *      3.const修饰的变量:结构体和类 对于const修饰的变量的使用方式是 一样的
 *  不同点:
 *      1.构造函数 结构体不允许定义无参构造函数，但是类可以
 *      2.析构函数 不允许定义析构函数；但是类可以
 *      3.函数的修饰符 结构体函数不允许声明为virtual(虚函数); 但是类可以
 *      4.类型修饰符 结构体类型不允许声明为abstract；但是类是可以的
 *      5.关于变量
 *          (1)普通变量---结构体声明的全局变量(不带修饰符)，不能再声明时直接赋值，只能在构造函数里边赋值；但是类哪里都可以的
 *          (2)readonly---结构体声明的全局realonly变量，只能在构造函数里边赋值;而class都可以
 *      6.关于继承
 *          结构体之间不可以相互继承；但是类与类之间可以相互继承(sealed除外)
 *      7.使用上
 *          (1)访问变量---结构体访问成员变量，给变量显示赋值，就可直接访问；而类是必须要实例化对象才可以访问
 *              结构体如果不通过new初始化，是不可以直接访问内部变量的(const)
 *          (2)访问函数---结构体变量和类对象 必须要进行初始化，才可以访问
 *      8.new
 *          (1)结构体属于值类型，结构体的new，并不会在堆上分配内存，仅仅是调用结构体的构造函数初始化而已
 *          (2)类属于引用类型，类的new，会在堆上分配内存，而且也会调用类的构造函数进行初始化
 *          
 */
/*struct TestStruct1 : TestStruct
{

}*/

/*abstract*/ struct TestStruct
{
    public int b;
    public const int a = 20;
    /*string str = "Hello World";*/

    static TestStruct(/*int value*/)
    {
    }

    /*public abstract void Fun3();*/

    public void fun1()
    {

    }

   /* public virtual void fun2()
    {

    }*/
    
    /*~TestStruct()
    {

    }*/
}

class class6 {
    ~class6 () {

    }

    public virtual void fun1()
    {

    }
}

abstract class class2 
{
    public abstract void fun2();
}


public class TStruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = TestStruct.a;
        TestStruct testStruct = new TestStruct();
        //TestStruct testStruct;
        testStruct.fun1(); // 都必须要初始化才可以访问
        testStruct.b = 1;
        int b = testStruct.b;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
