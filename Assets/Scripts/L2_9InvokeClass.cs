using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_9InvokeClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // time���ִ��"methodName"����
        //Invoke("Demo", 1);
        // time���ִ��"methodName"������ÿrepeatRate���ִ��һ��
        InvokeRepeating("Demo", 1, 1);
        // ȡ������CancelInvoke()---����޲ξ���ȫ��ȡ��
        //CancelInvoke();
        // 5���ȡ��ѭ��
        Invoke("CancelInvoke", 5);
    }

    void Demo()
    {
        Debug.Log("ִ��Demo");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
