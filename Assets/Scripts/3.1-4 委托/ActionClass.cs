using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/**
 * Actionί��
 *      Action<T>��.NET Framework���õķ���ί�У�����ʹ��Action<T>ί��Ŷ�Բ�����ʽ���ݷ�������������ʾ�����Զ����ί�С�
 */
public class ActionClass : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // ��ʼ��
        Action Action = show;
        // ִ�к���
        Action();
        //action.Invoke();

        Action<int, int> action1 = show1;
        action1.Invoke(1, 2);

        //Action<int, int> action2 = show2;// Actionί������0������������16���������޷���ֵ����ʾ�޲Σ��޷���ֵ��ί�С�

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        Debug.Log("����show����");
    }

    public void show1(int a, int b)
    {
        Debug.Log("����show1����" + (a + b));
    }

    public int show2(int a, int b)
    {
        return 1000;
    }
}
