using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_5小球 : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private Rigidbody2D rigidbody2D;
    public bool onGround;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    private void jump()
    {
        // 跳跃，要判断是否在地上
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rigidbody2D.AddForce(Vector2.up * jumpPower);
        }
    }

    private void move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 移动坐标的方式
        //transform.Translate(new Vector3(h, v) * Time.deltaTime * speed);
        // 施加力的方式
        rigidbody2D.AddForce(new Vector2(h, v) * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 判断是否在地面上
        if(collision.transform.tag == "ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 判断是否离开地面
        if (collision.transform.tag == "ground")
        {
            onGround = false;
        }
    }
}
