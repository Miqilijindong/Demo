using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    /// <summary>
    /// 冲刺速度
    /// </summary>
    public float sprintSpeed;

    /// <summary>
    /// 地面阻力
    /// </summary>
    public float groundDrag;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybings")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    /// <summary>
    /// 地面判断
    /// </summary>
    [Header("Ground Check")]
    //[Tooltip("地面判断")]// 编辑器注释
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    public TMP_Text SpeedText;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerHeight = GetComponent<CapsuleCollider>().height;

        readyToJump = true;

        startYScale = transform.localScale.y;

        rb.freezeRotation = true;
        //-None          不应用插值。
        //-Interpolate   根据前一帧的变换来平滑变换。
        //-Extrapolate   根据下一帧的估计变换来平滑变换。
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        //Collision Detection 用于防止快速移动的对象穿过其他对象而不检测碰撞。
        //-Discrete                 对场景中的所有其他碰撞体使用离散碰撞检测。其他碰撞体在测试碰撞时会使用离散碰撞检测。用于正常碰撞（这是默认值）。
        //-Continuous               对动态碰撞体（具有刚体）使用离散碰撞检测，并对静态碰撞体（没有刚体）使用基于扫掠的连续碰撞检测。设置为__连续动态(Continuous Dynamic)__ 的刚体将在测试与该刚体的碰撞时使用连续碰撞检测。其他刚体将使用离散碰撞检测。用于__连续动态(Continuous Dynamic)__ 检测需要碰撞的对象。（此属性对物理性能有很大影响，如果没有快速对象的碰撞问题，请将其保留为 Discrete 设置）
        //-Continuous Dynamic       对设置为__连续(Continuous)__ 和__连续动态(Continuous Dynamic)__ 碰撞的游戏对象使用基于扫掠的连续碰撞检测。还将对静态碰撞体（没有刚体）使用连续碰撞检测。对于所有其他碰撞体，使用离散碰撞检测。用于快速移动的对象。
        //-Continuous Speculative   对刚体和碰撞体使用推测性连续碰撞检测。这也是可以设置运动物体的唯一 CCD 模式。该方法通常比基于扫掠的连续碰撞检测的成本更低。
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void Update()
    {
        // 地面射线判断
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        // 开始蹲下
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // 停止蹲下
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    public void StateHandler()
    {
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(SprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }

    public void MovePlayer()
    {
        // 计算移动位置
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // 判断是否在斜面
        if (OnSlope() && !exitingSlope)
        {
            Vector3 slopeVec = GetSlopeMoveDirection(moveDirection);
            rb.AddForce(slopeVec.normalized * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                // 这里是应为走上斜坡，然后会弹跳
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (grounded)
        {
            // Acceleration、Force、Impulse 和 VelocityChange
            //Acceleration：     无视物体刚体质量给其施加加速度。
            //Force：            向刚体施加连续的力（意味着物体受到force参数的力，时间为一帧的时间），考虑其质量，即同样的力施加在越重的物体上产生的加速度越小。
            //Impulse：          向刚体施加瞬时的力（相当于物体瞬间获得受到force参数的力影响一秒钟时间的效果），考虑其质量，即同样的力施加在越重的物体上产生的加速度越小。
            //VelocityChange：   无视刚体质量在物体原有速度的基础上给物体施加一个速度向量。
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }

    /// <summary>
    /// 控制速度
    /// 这里有个问题，因为刚体施加力是在FixedUpdate里加的，然后控制速度却是在Update里加的，导致会有断断续续的速度超过moveSpeed
    /// 如果帧速过低，就会导致速度突破moveSpeed
    /// </summary>
    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }


        SpeedText.text = "Speed:" + rb.velocity.magnitude.ToString("f0");
    }

    public void Jump()
    {
        exitingSlope = true;

        // 重置速率
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // 施加一个瞬时的力
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        exitingSlope = false;

        readyToJump = true;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            // raycashhit.normal = 法线
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            if (angle < maxSlopeAngle && angle != 0)
            {
                return true;
            }
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
