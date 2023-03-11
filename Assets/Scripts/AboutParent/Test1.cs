using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����з��ֵ�����һ:�̳���Monobehaviour���࣬����ֹ���캯������������������̶��ᱻunity����ִ�ж��
/// �����������ֲ��Ը��๹�캯���ģ�����д�����Է���ȥ���ù���
/// �����:�������ֲ������⣬������addComponent����removeComponent������ʵ��������new�Ļ��ᱨ����ʾ������
/// </summary>
public class Test1 : MonoBehaviour
{
    AboutParentClass aboutParentClass1;
    AboutParentClass aboutParentClass2;
    AboutParentClass aboutParentClass3;

    AboutParentClassWithBase aboutParentClassWith1;
    AboutParentClassWithBase aboutParentClassWith2;
    AboutParentClassWithBase aboutParentClassWith3;

    AboutParentClassWithThis aboutParentClassThis1;
    AboutParentClassWithThis aboutParentClassThis2;
    AboutParentClassWithThis aboutParentClassThis3;
    void Start()
    {
        print("---------------------------------------with not Base");
        aboutParentClass1 = new AboutParentClass();
        aboutParentClass2 = new AboutParentClass(1);
        aboutParentClass3 = new AboutParentClass(1, 2);


        print("---------------------------------------with Base");
        aboutParentClassWith1 = new AboutParentClassWithBase();
        aboutParentClassWith2 = new AboutParentClassWithBase(1);
        aboutParentClassWith3 = new AboutParentClassWithBase(1, 2);

        print("---------------------------------------with this");
        aboutParentClassThis1 = new AboutParentClassWithThis();
        aboutParentClassThis2 = new AboutParentClassWithThis(1);
        aboutParentClassThis3 = new AboutParentClassWithThis(1, 2);

    }
}
