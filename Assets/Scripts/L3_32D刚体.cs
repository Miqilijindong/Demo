using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_32D刚体 : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 根据坐标移动
        //rigidbody2D.MovePosition(transform.position + new Vector3(0, 0.01f, 0) * Time.deltaTime);

        // 改变力
        rigidbody2D.velocity = new Vector2(0, 1);

        // 施加力---如果是跟刚体相关的话，最好用FixedUpdate，跟物理逻辑相关的代码
        //rigidbody2D.AddForce(Vector2.up);

    }
}
