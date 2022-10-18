using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L10_3Resources : MonoBehaviour
{
    public GameObject gameObject;
    void Start()
    {
        // 这个resources取得是所有“Resources”底下的文件，如果有两个一样的文件夹，就合并一起
        gameObject = Resources.Load<GameObject>("Cube");
    }

    void Update()
    {
        
    }
}
