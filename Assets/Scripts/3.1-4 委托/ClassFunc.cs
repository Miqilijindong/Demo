using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ϵͳ����funcί��
 *      System.Func���Բ��������� ���Ǳ����һ������ֵ
 *      System.Func ���ǵ��õĶ�����͵�ί�ж��壬���Ĳ��������������Ǻ����ķ���ֵ���ͣ���Ҫ����һ�£������һ������T����������Ҫ��ʵ�ֺ����Ĳ������������ͱ���һ��
 */
public class ClassFunc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Func<string> func = show;
        Debug.Log(func());

        // ���һ�������Ƿ������ͣ������һ������TҪ�����һ��
        Func<int, string> func1 = show1;
        Debug.Log(func1(1000));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string show()
    {
        return "����show����";
    }

    public string show1(int a)
    {
        return a.ToString();
    }
}
