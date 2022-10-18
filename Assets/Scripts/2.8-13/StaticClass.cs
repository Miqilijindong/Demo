using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// const
///     ������������const����
///     1.���ⲿ����ʱ������ͨ���������з���
///     2.ֻ��������ʱ��ʼ�������������κ������ط������ʼ��(�������캯��)
///     3.��ĳ�̶ֳȣ���const���εı���(����)�����ɱ�ֵ
/// readonly
///     ������������readonly����
///     1.readonly const,���ɹ�ͬ����һ����������(������������+�Զ�����������)
///     2.���Ա����ʵ�����з���
///     3.�����ڹ��캯����������ʱ��ֵ
/// readonly
/// �����������η��Ŀ���
/// 
/// ��̬��
///     1.��̬�಻������ʵ�����캯����ֻ�������һ����̬���캯��
///     2.�޷�������̬���ʵ����
///     3.��̬�࣬�ڲ���Ա�����Ǿ�̬��Ա+������Ա
///     4.��̬�޷���Ϊ��������
///     
/// </summary>
public class StaticClass : MonoBehaviour
{
    public class MyClass {
        public const int mValue = 20;
        public readonly int mValue2 = 20;

    }

    private void Start()
    {
        MyClass myClass = new MyClass();
        // const ����Ҫͨ������ܷ���
        Debug.Log(MyClass.mValue);
        // readonly ���ʵ�����Է���
        Debug.Log(myClass.mValue2);
    }
}
