using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 数据存储
 * 数据存储位置:存储在注册表的
 * HKEY_CURRENT_USER\Software[company name][product name]下
 * 打开方式:cmd中regedit.exe
 */
public class L10_4数据存档 : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.SetString("String", "123");
        //PlayerPrefs.SetFloat("Float", 123.2f);
        //PlayerPrefs.SetInt("Int", 123);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            string s = PlayerPrefs.GetString("String");
            float f = PlayerPrefs.GetFloat("Float");
            int i = -1;
            // 判断是否存在Int的数据，如果没有就是-1
            if(PlayerPrefs.HasKey("Int"))
            {
                i = PlayerPrefs.GetInt("Int");
            }
            Debug.Log(s + ", " + f + ", " + i);
        }
        if(Input.GetMouseButtonDown(1)) {
            // 删除所有
            PlayerPrefs.DeleteAll();
        }
    }
}
