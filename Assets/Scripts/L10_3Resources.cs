using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10_3Resources : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        // ���resourcesȡ�������С�Resources�����µ��ļ������������һ�����ļ��У��ͺϲ�һ��
        gameObject = Resources.Load<GameObject>("Cube");
    }

    void Update()
    {
        
    }
}
