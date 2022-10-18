using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6_7刚体移动 : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Update is called once per frame
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        Vector3 vector3 = new Vector3(h, 0, v) * Time.deltaTime * 3;

        rigidbody.MovePosition(transform.position + vector3);
    }
}
