using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RequireComponent自动添加所需要的对应依赖，比如需要rigidbody的话，就会自动对挂载脚本的物体添加，有且只有一个rigidbody。
/// 而且无法删除，点击remove的话，会报错“(script)depend on it”
/// 但是注释后，组件还是要自己删除
/// </summary>
//[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]// 一次性添加多个组件
[RequireComponent(typeof(Rigidbody))]
public class RequireComponentClass : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
