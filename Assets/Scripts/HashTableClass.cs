using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * hashTable���е�����java.hashmap�ģ������Լ�ֵ�Եļ���
 */
public class HashTableClass : MonoBehaviour
{
    Hashtable hashtable = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        hashtable.Add("1", 1);
        hashtable.Add(1, 2);
        // ȡֵ�ķ�ʽ�����������ƣ������������ڷŵ���key
        Debug.Log(hashtable[1]);
        Debug.Log(hashtable["1"]);

        foreach(var i in hashtable.Keys)
        {
            Debug.LogFormat("ѭ����ӡhashTable�����ݣ�[{0}]:{1}", i, hashtable[i]);
        }

        hashtable.Clear();
        bool v = hashtable.Contains("1");
        Debug.Log("v:" + v);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
