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
 * 1.匿名函数
 * 2.event事件
 *      (1) event事件 只允许作为类的成员变量 且仅在类的内部使用才可以，外部不得直接调用
 *      (2) 当作为A类的成员 event事件在外部类赋值是，只能通过 += -=的方法；而对于 普通的Action则可以=/ += -=的方式进行赋值
 * 3.多播委托
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
            Debug.Log("匿名函数被调用");
        };
        action1();
                                                                                         
        // 必须是泛型相同类型的函数---多播委托
        Action action2 = show1;
        action2 += show;
        action2 -= show;
        action2();

        action3 = show2;
        action3();

        MyEventClass myEventClass = new MyEventClass();
        //myEventClass.EventAction = show3;是错误的
        myEventClass.EventAction += show3;
        myEventClass.defaultAction = show4;
        //myEventClass.EventAction();这样是不能直接外部调用
        myEventClass.eventSend();
        myEventClass.defaultAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        Debug.Log("函数show方法被调用");
    }

    void show1()
    {
        Debug.Log("函数show1方法被调用");
    }
    void show2()
    {
        Debug.Log("event函数show2方法被调用");
    }
    void show3()
    {
        Debug.Log("event函数show3方法外部调用");
    }
    void show4()
    {
        Debug.Log("函数show4方法外部调用");
    }
}
