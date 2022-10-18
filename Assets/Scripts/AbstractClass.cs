using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ������
 *      1.������ʵ����
 *      2.֧�ֹ��캯��
 *      3.������ɼ̳г�����
 *      4.��̬���캯�� ִֻ��һ�Σ����������Ĺ��캯�������ݲ�ͬʵ�����ֱ��ٴε���
 *      5.����ʹ��virtual�麯��
 *      6.����������Ϊabstract�������к����壬���������override�÷���
 */

public abstract class AbstractClass1
{
    public AbstractClass1()
    {
        Debug.Log("��������ù��캯��");
    }

    static AbstractClass1()
    {
        Debug.Log("��������þ�̬���캯��");
    }
}

public abstract class AbstractClass2 : AbstractClass1
{
    public AbstractClass2 ()
    {
        Debug.Log("������2���ù��캯��");
    }

    public virtual void test ()
    {
        Debug.Log("���ó�����2��test����");
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
        Debug.Log("���ó�����3�ĳ�����");
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
