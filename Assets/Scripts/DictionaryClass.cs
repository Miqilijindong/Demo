using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 字典表
 *      泛型容器，存储键值对数据的集合
 */
public class DictionaryClass : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, string> keyValues = new Dictionary<string, string>();
    void Start()
    {
        keyValues.Add("1", "1000");
        keyValues.Add("2", "2000");
        keyValues.Add("3", "3000");
        if(keyValues.ContainsKey("1"))
        {
            Debug.Log("keyValues[1]存在键");
        }
        foreach(var i in keyValues.Keys)
        {
            Debug.LogFormat("keyValues[{0}], value={1}", i, keyValues[i]);
        }
        foreach (KeyValuePair<string, string> kvp in keyValues)
        {
            Debug.LogFormat("keyValues[{0}], value={1}", kvp.Key, kvp.Value);
        }

        keyValues.Remove("3");
        keyValues.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
