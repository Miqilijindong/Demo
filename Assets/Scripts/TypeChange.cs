using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      
 *      ��������    string int[] class interface---���������Ǵ��ڶ��ϵ�
 *      ֵ����     byte (u)short (u)int (u)long bool enum struct---ֵ�����Ǵ洢��ջ�ϵ�,��Ȼ��Ҳ�������������й�ϵ�ģ�����ǳ�Ա�����Ļ������Ǵ洢�ڶ���
 *      
 *      short   -32768��32767
 *      int     -2147383648��214748647
 *      folat   7λС��
 *      double  15-16λ
 *      
 * is
 *      �ɼ��ֵ���ͺ��������ͣ��ɹ�����true�����򷵻�false
 * as
 *      as���Ȼ��ж� Դ���������Ƿ���Ŀ���������ͣ����ǵĻ�������ֱ�ӱ���
 *      asװ���ɹ�������Դ�������ʹ洢�����ݣ����򷵻�null
 *      
 * ǿ������ת��(��ʽ����ת��)
 *      (1)�߾���->�;��� ��Ҫǿ������ת��
 *      ע�⣺�߾�����������ת��Ϊ�;�������ʱ����Ҫת���ɹ���Ҫ�������������λ��<=�;�����������ʱ
 * �Զ�����ת��(��ʽ����ת��)
 *      (1)�;���->�߾��� ϵͳ�Զ�ת��
 */

public class class33
{

}

public class class55 : class33
{

}
public class TypeChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int a = 10;
        string str = "Hello World";
        bool v = a is string;
        Debug.Log(v);

        class55 test = new class55();
        class33 newtest = test as class33;
        bool check = test is class33;

        int[] b = { 1, 2, 3 };
        //int[] c = a as int[];
        int[] c = b as int[];
        if (c != null)
        {
            Debug.Log(c.Length);
        }

        Debug.Log(check);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
