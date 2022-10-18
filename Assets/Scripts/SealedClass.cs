using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 密封类
 *      1.不允许被继承
 *      2.sealed abstract不能共存
 *      3.密封类内的函数，不允许增加sealed关键字
 *      4.密封类 可以正常继承常见类、接口
 */
public class class8 {

}

public sealed class class9 : class8
{

}
public class SealedClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
