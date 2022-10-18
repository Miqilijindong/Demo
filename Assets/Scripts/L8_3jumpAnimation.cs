using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L8_3jumpAnimation : MonoBehaviour
{
    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        jump();
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
    }

    public void Demo(int a)
    {
        Debug.Log("¶¯»­ÊÂ¼þ" + a);
    }
}
