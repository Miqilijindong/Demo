using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * �ṹ��
 * 
 * �ṹ�����������
 *  ��ͬ�㣺
 *      1.��̬���캯����֧��
 *      2.�Զ��庯����֧��
 *      3.const���εı���:�ṹ����� ����const���εı�����ʹ�÷�ʽ�� һ����
 *  ��ͬ��:
 *      1.���캯�� �ṹ�岻�������޲ι��캯�������������
 *      2.�������� ���������������������������
 *      3.���������η� �ṹ�庯������������Ϊvirtual(�麯��); ���������
 *      4.�������η� �ṹ�����Ͳ���������Ϊabstract���������ǿ��Ե�
 *      5.���ڱ���
 *          (1)��ͨ����---�ṹ��������ȫ�ֱ���(�������η�)������������ʱֱ�Ӹ�ֵ��ֻ���ڹ��캯����߸�ֵ�����������ﶼ���Ե�
 *          (2)readonly---�ṹ��������ȫ��realonly������ֻ���ڹ��캯����߸�ֵ;��class������
 *      6.���ڼ̳�
 *          �ṹ��֮�䲻�����໥�̳У�����������֮������໥�̳�(sealed����)
 *      7.ʹ����
 *          (1)���ʱ���---�ṹ����ʳ�Ա��������������ʾ��ֵ���Ϳ�ֱ�ӷ��ʣ������Ǳ���Ҫʵ��������ſ��Է���
 *              �ṹ�������ͨ��new��ʼ�����ǲ�����ֱ�ӷ����ڲ�������(const)
 *          (2)���ʺ���---�ṹ������������ ����Ҫ���г�ʼ�����ſ��Է���
 *      8.new
 *          (1)�ṹ������ֵ���ͣ��ṹ���new���������ڶ��Ϸ����ڴ棬�����ǵ��ýṹ��Ĺ��캯����ʼ������
 *          (2)�������������ͣ����new�����ڶ��Ϸ����ڴ棬����Ҳ�������Ĺ��캯�����г�ʼ��
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
        testStruct.fun1(); // ������Ҫ��ʼ���ſ��Է���
        testStruct.b = 1;
        int b = testStruct.b;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
