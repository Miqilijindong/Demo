using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RequireComponent�Զ��������Ҫ�Ķ�Ӧ������������Ҫrigidbody�Ļ����ͻ��Զ��Թ��ؽű���������ӣ�����ֻ��һ��rigidbody��
/// �����޷�ɾ�������remove�Ļ����ᱨ��(script)depend on it��
/// ����ע�ͺ��������Ҫ�Լ�ɾ��
/// </summary>
//[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]// һ������Ӷ�����
[RequireComponent(typeof(Rigidbody))]
public class RequireComponentClass : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
