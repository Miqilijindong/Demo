using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test
{
    private static test _value;
    public static test value
    {
        get
        {
            if(_value != null)
            {
                return _value;
            } else
            {
                return new test();
            }
        }
        set
        {
            _value = value;
        }
    }

    private test() { }

}

public class test2
{
    public int a;
}

/**
 * ���ģʽ����ģʽ
 */
public class SingletonClass : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        test test;
        int test2 = new test2().a;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
