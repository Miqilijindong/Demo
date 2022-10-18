using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 泛型
///     1.在声明时可以不指定具体的类型，但是在 new 实例化时必须指定T类型；
///     2.可指定泛型类型约束
///     3.如果子类也是泛型的，那么继承的时候可以不指定具体类型
///     4.可以通过where限定类型
///     5.泛型约束：where，一共有五种
///     where T:class   T必须是一个;类 
///     where T:struct  T必须是一个构造类型
///     where T:new()   T必须要有一个无参数的构造函数
///     where T:NameOfBasClass  T必须要继承名为NameOfBaseClass的类
///     where T:NameOfInterfact T必须实现名为NameOfInterface的接口
/// </summary>
public class TClass : MonoBehaviour
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
