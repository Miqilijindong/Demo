using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * hashTable是有点类似java.hashmap的，都是以键值对的集合
 */
public class HashTableClass : MonoBehaviour
{
    Hashtable hashtable = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        hashtable.Add("1", 1);
        hashtable.Add(1, 2);
        // 取值的方式，跟数组类似，但是中括号内放的是key
        Debug.Log(hashtable[1]);
        Debug.Log(hashtable["1"]);

        foreach(var i in hashtable.Keys)
        {
            Debug.LogFormat("循环打印hashTable的内容，[{0}]:{1}", i, hashtable[i]);
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
