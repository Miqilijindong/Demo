using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 字典表
/// 泛型容器，存储键值对数据的集合
/// 取值的话，最好引入Linq，
/// </summary>
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
            Debug.LogFormat("1---keyValues[{0}], value={1}", i, keyValues[i]);
        }
        foreach (KeyValuePair<string, string> kvp in keyValues)
        {
            Debug.LogFormat("2---keyValues[{0}], value={1}", kvp.Key, kvp.Value);
        }
        for (int i = 0; i < keyValues.Count; i++)
        {
            Debug.LogFormat("3---keyValue[{0}], value={1}", keyValues.Keys.ToList()[i], keyValues.Values.ToList()[i]);
        }
        keyValues.Remove("3");
        keyValues.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
