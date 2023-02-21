using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovementGrappling : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    /// <summary>
    /// 冲刺速度
    /// </summary>
    public float sprintSpeed;
    /// <summary>
    ///  滑铲速度
    /// </summary>
    public float slideSpeed;
    public float wallRunSpeed;
    public float climbSpeed;
    public float vaultSpeed;
    public float airMinSpeed;
    /// <summary>
    /// 弹射速度
    /// </summary>
    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYSpeed;

    /// <summary>
    /// 设定的移速
    /// </summary>
    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

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

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode SprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;

    /// <summary>
    /// 地面判断
    /// </summary>
    [Header("Ground Check")]
    //[Tooltip("地面判断")]// 编辑器注释
    public float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Camera Effects")]
    public PlayerCam cam;
    public float grappleFov = 95f;

    [Header("References")]
    public Climbing climbingScript;

    public Transform orientation;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        unlimited,
        walking,
        sprinting,
        wallruning,
        climbing,
        vaulting,
        crouching,
        dashing,
        sliding,
        air
    }

    public bool sliding;
    public bool crouching;
    /// <summary>
    /// 是否在靠墙跑
    /// </summary>
    public bool wallrunning;
    public bool climbing;
    public bool vaulting;
    /// <summary>
    /// 静止
    /// </summary>
    public bool freeze;
    public bool unlimited;
    public bool dashing;

    public bool restricted;

    public bool activeGrapple;

    public TMP_Text text_speed;
    public TMP_Text text_mode;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    CapsuleCollider capsuleCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

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
        grounded = grounded ? grounded : Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsWall);

        MyInput();
        SpeedControl();
        StateHandler();
        TextStuff();

        // 判断如果在地面，同时没有用钩爪，则有地面摩擦
        if (grounded && !activeGrapple)
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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

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

    /// <summary>
    /// 保持动态延续
    /// 类似滑铲一样
    /// </summary>
    public bool keepMomentum;
    private MovementState lastState;
    public void StateHandler()
    {
        if (dashing)
        {
            state = MovementState.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangeFactor;
            //keepMomentum = true;// 这里放有问题，会导致冲刺前的速度被延续了
        }
        else if (freeze)
        {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
            desiredMoveSpeed = 0f;
        }
        else if (unlimited)
        {
            state = MovementState.unlimited;
            desiredMoveSpeed = 999f;
        }
        else if (vaulting)
        {
            state = MovementState.vaulting;
            desiredMoveSpeed = vaultSpeed;
        }

        else if (climbing)
        {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        else if (wallrunning)
        {
            state = MovementState.wallruning;
            desiredMoveSpeed = wallRunSpeed;
        }
        else if (sliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                desiredMoveSpeed = slideSpeed;
                keepMomentum = true;
            }
            else
            {
                desiredMoveSpeed = crouchSpeed;
            }
        }

        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(SprintKey))
        {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;

            if (moveSpeed < airMinSpeed)
            {
                desiredMoveSpeed = airMinSpeed;
            }
            else
            {
                desiredMoveSpeed = sprintSpeed;
            }
        }


        /*if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;*/
        bool desiredMoveSpeedHasChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementState.dashing) keepMomentum = true;

        if (desiredMoveSpeedHasChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;

        // deactivate keepMomentum
        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) keepMomentum = false;
    }

    private float speedChangeFactor;
    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            // 当推进时间 == 冲刺推进时间
            else if (boostFactor == dashSpeedChangeFactor)
            {
                time += Time.deltaTime * boostFactor;

                yield return null;
            }
            else
            {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }
            yield return null;
        }

        speedChangeFactor = 1f;
        moveSpeed = desiredMoveSpeed;
        keepMomentum = false;
    }

    public void MovePlayer()
    {
        if (activeGrapple) return;
        if (restricted) return;
        //if (state == MovementState.dashing) return;// 这里注释掉是因为如果在地上冲刺的话，由于第二帧就开始由于无法移动，然后就不知道为什么的开始减速了，然后又因为状态变回walking，所以速度又可以加了，就会从7/18，变成12/16，看起来效果就是冲刺了一点，然后又冲刺了一点，有种二段冲刺的感觉

        if (climbingScript != null && climbingScript.exitingWall)
        {
            return;
        }

        // 计算移动位置
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // 判断是否在斜面
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                // 这里是应为走上斜坡，然后会弹跳
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else if (grounded)
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

        if (!wallrunning)
        {
            rb.useGravity = !OnSlope();
        }
    }

    float test;

    /// <summary>
    /// 控制速度
    /// 这里有个问题，因为刚体施加力是在FixedUpdate里加的，然后控制速度却是在Update里加的，导致会有断断续续的速度超过moveSpeed
    /// 如果帧速过低，就会导致速度突破moveSpeed
    /// </summary>
    private void SpeedControl()
    {
        if (activeGrapple) return;
        bool v = OnSlope();
        if (v && !exitingSlope)
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

        if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }

        //if (rb.velocity.magnitude < test)
        //{
        //    Debug.Log("打印日志");
        //}
        //test = rb.velocity.magnitude;
        //SpeedText.text = "Speed:" + rb.velocity.magnitude.ToString("f1");
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

    public void ResetRestrictions()
    {
        activeGrapple = false;
        cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            // raycashhit.normal = 法线
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void TextStuff()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (OnSlope())
            text_speed?.SetText("Speed: " + Round(rb.velocity.magnitude, 1) + " / " + Round(moveSpeed, 1));

        else
            text_speed?.SetText("Speed: " + Round(flatVel.magnitude, 1) + " / " + Round(moveSpeed, 1));

        text_mode?.SetText(state.ToString());
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov);
    }

    /// <summary>
    /// 计算钩爪跳跃velocity
    /// </summary>
    /// <param name="starPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="trajectoryHeight"></param>
    /// <returns></returns>
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * MathF.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (MathF.Sqrt(-2 * trajectoryHeight / gravity)
            + MathF.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        // 计算跳跃点时，我加了一个身位，虽然可以稳定停在了墙上，但是必须要瞄准边角才可以
        return velocityXZ + velocityY + (velocityXZ + velocityY).normalized * capsuleCollider.radius * 2;
    }
}
