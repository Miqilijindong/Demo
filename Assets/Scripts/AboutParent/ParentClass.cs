using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����з��ֵ�����һ:�̳���Monobehaviour���࣬����ֹ���캯������������������̶��ᱻunity����ִ�ж��
/// �����������ֲ��Ը��๹�캯���ģ�����д�����Է���ȥ���ù��졣����ȥ����Assets/Scripts/AboutParent/Test1.cs�Ĵ���
/// </summary>
public class ParentClass : MonoBehaviour
{
    public ParentClass()
    {
        // �޲ι��캯��
        print("ParentClass has no parameter");
    }

    public ParentClass(int a)
    {
        print("ParentClass(int a) has a parameter");
    }

    public ParentClass(int a, int b)
    {
        print("ParentClass(int a, int b) has a and b parameter");
    }
}
