using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 靠墙跑
/// </summary>
public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float walljumpUpForce;
    public float wallJumpSideForce;
    public float wallClimbSpeed;
    /// <summary>
    /// 靠墙跑的最大时间
    /// </summary>
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    public KeyCode jumpKey = KeyCode.Space;
    /// <summary>
    /// 靠墙向上爬
    /// </summary>
    private bool upwardsRunning;
    /// <summary>
    /// 爬墙向下
    /// </summary>
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    /// <summary>
    /// 墙体距离判断
    /// </summary>
    [Header("Detection")]
    public float wallCheckDistance;
    /// <summary>
    /// 最小跳跃高度
    /// </summary>
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    public PlayerCam cam;
    private PlayerMovement pm;
    private Rigidbody rb;
    private LedgeGrabbing lg;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
        lg = GetComponent<LedgeGrabbing>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        stateMachine();
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning)
        {
            WallRunningMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }


    /// <summary>
    /// 判断是否远离地面
    /// </summary>
    /// <returns></returns>
    public bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    public void stateMachine()
    {
        // 获取按键输入
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // 状态一 : wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if (!pm.wallrunning)
            {
                StartWallRun();
            }

            // wallrun timer
            if (wallRunTimer > 0)
            {
                wallRunTimer -= Time.deltaTime;
            }

            if (wallRunTimer <= 0 && pm.wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }

            // 爬墙跳跃
            if (Input.GetKeyDown(jumpKey))
            {
                WallJump();
            }
        }
        // 状态二 : 爬墙中
        else if (exitingWall)
        {
            if (pm.wallrunning)
            {
                StopWallRun();
            }

            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }

            if (exitWallTimer <= 0)
            {
                exitingWall = false;
            }
        }
        // 状态三 : 结束
        else
        {
            if (pm.wallrunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        pm.wallrunning = true;

        wallRunTimer = maxWallRunTime;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        cam.DoFov(90f);
        if (wallLeft)
        {
            cam.DoTile(-2.5f);
        }
        if (wallRight)
        {
            cam.DoTile(2.5f);
        }
    }

    /// <summary>
    /// 靠墙壁跑
    /// </summary>
    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        if (upwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        }
        if (downwardsRunning)
        {
            rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);
        }

        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
        {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        // 减轻重力
        if (useGravity)
        {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRun()
    {
        pm.wallrunning = false;

        cam.DoFov(80);
        cam.DoTile(0);
    }

    /// <summary>
    /// 爬墙跳跃
    /// </summary>
    private void WallJump()
    {
        if (lg.holding || lg.exitingLedge)
        {
            return;
        }

        // 进入跳出爬墙状态
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 forceToApply = transform.up * walljumpUpForce + wallNormal * wallJumpSideForce;

        // 重置Y轴速率
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
