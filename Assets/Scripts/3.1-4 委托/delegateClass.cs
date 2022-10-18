using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ί��
 *      ���Ҫ�ѷ�����Ϊ���������д��ݵĻ�����Ҫ�õ�ί�С�����˵��ί����һ�����ͣ�������Ϳ��Ը���һ�����������á�C#��ί�� ͨ�� delegate�ؼ���������
 *      ����ί��:
 *          1��delegate void MyDelegate1(int x);
 *          2��delegate void MyDelegate2<T>(T x);
 *      ʹ��ί��:
 *          1��MyDelegate1 mydelegate = new MyDelegate1(func)
 *          2��MyDelegate1 mydelegate = func;
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
        // ��ʼ��
        myDelegate myDelegate = new myDelegate(show1);
        // ���÷�ʽһ
        myDelegate();
        // ���÷�ʽ��
        myDelegate.Invoke();

        myDelegate1 myDelegate1 = show2;
        myDelegate1(1, 2);

        myDelegate2<string> myDelegate2 = show3;
        myDelegate2.Invoke("Hello World");

        myDelegate3 myDelegate3 = show4;
        int v = myDelegate3();
        Debug.Log("show4����" + 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void show1()
    {
        Debug.Log("show1������");
    }

    private void show2(int a, int b)
    {
        Debug.Log("show2�����ã� a+b=" + (a + b));
    }

    private void show3(string a)
    {
        Debug.Log("show3������" + a);
    }
    private int show4()
    {
        return 0;
    }
}
