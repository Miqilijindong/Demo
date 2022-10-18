using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySpace
{
    public class MyClass
    {

        public MyClass () {
            Debug.Log("初始化MyClass");
        }

        ~MyClass () {
            Debug.Log("析构函数MyClass");
        }
    }

    
}

public class Day4 : MonoBehaviour
{
    public int a = 1;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log(i);
        //}
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        //MySpace.MyClass myClass = new MySpace.MyClass();
        //Debug.Log(myClass.a);
        if(Input.GetMouseButton(0))
        {
            MySpace.MyClass myClass = new MySpace.MyClass();
            DestroyObject(gameObject);
        }
    }
}
