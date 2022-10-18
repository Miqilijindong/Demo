using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 委托
 *      如果要把方法作为函数来进行传递的话，就要用到委托。简单来说，委托是一个类型，这个类型可以复制一个方法的引用。C#的委托 通过 delegate关键字来声明
 *      声明委托:
 *          1、delegate void MyDelegate1(int x);
 *          2、delegate void MyDelegate2<T>(T x);
 *      使用委托:
 *          1、MyDelegate1 mydelegate = new MyDelegate1(func)
 *          2、MyDelegate1 mydelegate = func;
 */
public class delegateClass : MonoBehaviour
{

    public delegate void myDelegate();
    public delegate void myDelegate1(int a, int b);
    public delegate void myDelegate2<T>(T a);
    public delegate int myDelegate3();
    // Start is called before the first frame update
    void Start()
    {
        // 初始化
        myDelegate myDelegate = new myDelegate(show1);
        // 调用方式一
        myDelegate();
        // 调用方式二
        myDelegate.Invoke();

        myDelegate1 myDelegate1 = show2;
        myDelegate1(1, 2);

        myDelegate2<string> myDelegate2 = show3;
        myDelegate2.Invoke("Hello World");

        myDelegate3 myDelegate3 = show4;
        int v = myDelegate3();
        Debug.Log("show4返回" + 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void show1()
    {
        Debug.Log("show1被调用");
    }

    private void show2(int a, int b)
    {
        Debug.Log("show2被调用， a+b=" + (a + b));
    }

    private void show3(string a)
    {
        Debug.Log("show3被调用" + a);
    }
    private int show4()
    {
        return 0;
    }
}
