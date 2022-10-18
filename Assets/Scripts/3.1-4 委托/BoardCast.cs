using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEventClass
{
    public event Action EventAction;
    public Action defaultAction;

    public void eventSend()
    {
        EventAction.Invoke();
    }
}
/**
 * 1.��������
 * 2.event�¼�
 *      (1) event�¼� ֻ������Ϊ��ĳ�Ա���� �ҽ�������ڲ�ʹ�òſ��ԣ��ⲿ����ֱ�ӵ���
 *      (2) ����ΪA��ĳ�Ա event�¼����ⲿ�ำֵ�ǣ�ֻ��ͨ�� += -=�ķ����������� ��ͨ��Action�����=/ += -=�ķ�ʽ���и�ֵ
 * 3.�ಥί��
 */
public class BoardCast : MonoBehaviour
{
    event Action action3;
    // Start is called before the first frame update
    void Start()
    {
        Action action = show;
        action();

        Action action1 = delegate ()
        {
            Debug.Log("��������������");
        };
        action1();
                                                                                         
        // �����Ƿ�����ͬ���͵ĺ���---�ಥί��
        Action action2 = show1;
        action2 += show;
        action2 -= show;
        action2();

        action3 = show2;
        action3();

        MyEventClass myEventClass = new MyEventClass();
        //myEventClass.EventAction = show3;�Ǵ����
        myEventClass.EventAction += show3;
        myEventClass.defaultAction = show4;
        //myEventClass.EventAction();�����ǲ���ֱ���ⲿ����
        myEventClass.eventSend();
        myEventClass.defaultAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        Debug.Log("����show����������");
    }

    void show1()
    {
        Debug.Log("����show1����������");
    }
    void show2()
    {
        Debug.Log("event����show2����������");
    }
    void show3()
    {
        Debug.Log("event����show3�����ⲿ����");
    }
    void show4()
    {
        Debug.Log("����show4�����ⲿ����");
    }
}
