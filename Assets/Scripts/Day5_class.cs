using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 1.const:����
 *      ������������const����
 *      (1)�ⲿ����ʱ������ͨ���������з��ʣ��������޸�ֵ
 *      (2)ֻ��������ʱ��ʼ�����������κ������ط������ʼ��(�������캯��)
 *      (3)��ĳ�̶ֳȣ���const���εı���(����)�����ɱ�ֵ
 * 2.readonly:ֻ��
 *      ������������readonly����
 *      (1)readonly const�����ɹ�ͬ����һ������(������������+�Զ�����������)
 *      (2)readonly���ε����ͣ����Ա�����������з��ʣ��������޸�ֵ
 *      (3)readonly�ĳ�ʼ����ֻ�ܷ����ڹ��캯�����������С�
 * 3.�����������η��Ŀ���
 *      (1)��ͨ���������η����ɵ��﷨�죬��ʵ�������ⲿֻ����Ч��get set��һ��ѧϰ��value��ֵ�ͷ��ʴ��������
 * 4.��̬���캯��
 *      (1)��̬���캯��������Ҫ���ӷ������η�
 *      (2)��̬���캯�������۶���ʵ�� ��ֻ������һ�Σ�����ֻ��ϵͳ�Զ�����һ��
 * ��̬��:
 *      (1)��̬�಻������ʵ�����캯����ֻ�������һ����̬���캯����(����:��̬��ľ�̬���캯���������Է��֣�������ִ��)
 *      (2)��̬�࣬������ʵ����
 *      (3)��̬�࣬�ڲ���Ա�������Ǿ�̬��Ա/����
 *      (4)��̬���޷����̳�
 */



/*
 * ��̬��
 */
static class Day5_test
{
    // ��̬�಻������ʵ��
    public static int istaticValue = 0;
}

public class Day5_class : MonoBehaviour
{
    public const int ivalue = 1;
    public readonly int ivalue_readOnly = 2;
    public static int d = 0;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ivalue);
        Debug.Log(ivalue_readOnly);
        Day5_test2 day5_test2 = new Day5_test2();
        int a = day5_test2.iValueGet;

        day5_test2.ivalue_4 = 5;
        Debug.Log(day5_test2.ivalue_4);

        int c = Day5_test3.value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public Day5_class () {
    //    ivalue_readOnly = 20;
    //    Debug.Log("readOnly���ڹ��캯���︳ֵ");
    //}

    public class Day5_test2
    {
        Day5_class demo = new Day5_class();
        //int a = demo.d;
        public int iValueGet { get; private set; }// get/set����

        public int ivalue_4
        {
            get
            {
                return ivalue4;
            }
            set
            {
                ivalue4 = value;
            }
        }

        private int ivalue4;
    }

    public static class Day5_test3
    {
        public static int value;

        // ��̬���캯��ֻ�ᱻ����һ�Σ�����ϵͳ�Զ�����
        static Day5_test3()
        {
            Debug.Log("��̬���캯��ִ��һ��");
        }
    }
}
