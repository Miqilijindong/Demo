using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Demo3 : MonoBehaviour
{

    public EmAction emAction = new EmAction();
    // Start is called before the first frame update
    void Start()
    {
        //fun1();
    }

    // Update is called once per frame
    void Update()
    {
        fun2();
    }

    void fun1()
    {
        emAction = EmAction.Eat;
        string strPlay = "Play";
        emAction = (EmAction)Enum.Parse(typeof(EmAction), strPlay);
        Debug.Log("string->enum:" + emAction);
        Debug.LogFormat("enum->string:{0}", emAction.ToString());

        emAction = (EmAction)1;
        Debug.Log("int->enum:" + emAction.ToString());

        Debug.LogFormat("enum->int:{0}", (int)emAction);
    }

    void fun2()
    {
        //emAction = EmAction.Play;
        switch(emAction)
        {
            case EmAction.Play:
                Debug.Log("玩");
                break;
            case EmAction.Eat:
                Debug.Log("吃");
                break;
            case EmAction.getUp:
                Debug.Log("起床");
                break;
            case EmAction.Wash:
                Debug.Log("洗澡");
                break;
            default:
                Debug.Log("啥也不做");
                break;
        }
    }

    public enum EmAction
    {
        None,
        getUp,
        Wash,
        Eat,
        Play,
    }
}
