using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class L8案例_角色移动 : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;
    int speed = 6;
    int roSpeed = 15;
    float v;
    float h;

    public Transform camTransform;
    Vector3 movement;
    Vector3 camForward;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //move2();

        // 这个移动方式不能用characterController
        Move(); characterController.enabled = false;
    }

    /**
     * 侠盗猎车罪恶都市的移动方式
     */
    void move1()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        if (v != 0 || h != 0)
        {
            animator.SetBool("run", true);
            Vector3 vector3 = new Vector3(h, 0, v);
            characterController.SimpleMove(vector3 * speed);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    /**
     * 生化危机4移动方式
     */
    void move2()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v != 0 || h != 0)
        {
            Vector3 vector3 = new Vector3(0, 0, v);
            vector3 = transform.TransformDirection(vector3);
            characterController.SimpleMove(vector3 * speed);

            transform.eulerAngles = transform.eulerAngles + new Vector3(0, h, 0) * roSpeed;

            if (animator != null)
            {
                animator.SetBool("run", true);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("run", false);
            }
        }
    }

    /**
     * 辐射4的移动方式
     */
    void Move()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(camTransform.right * h * speed * Time.deltaTime + camForward * v * speed * Time.deltaTime, Space.World);
        if (h != 0 || v != 0)
        {
            if (animator != null && !animator.GetBool("run"))
            {
                animator.SetBool("run", true);
            }
            Rotating(h, v);
        }
        else
        {
            if (animator != null && animator.GetBool("run"))
            {
                animator.SetBool("run", false);
            }
        }

    }

    private void Rotating(float h, float v)
    {
        camForward = Vector3.Cross(camTransform.right, Vector3.up);

        Vector3 targetDir = camTransform.right * h + camForward * v;

        Quaternion quaternion = Quaternion.LookRotation(targetDir, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, roSpeed * Time.deltaTime);
    }
}
