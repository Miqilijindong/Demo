using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    /// <summary>
    /// 2023��2��7��15:53:06 ����ط��ź�����������ͻ����ʱ�򿴿�����ô��
    /// </summary>
    [Header("Input")]
    public KeyCode upwardsRunKey = KeyCode.LeftShift;
    public KeyCode downwardsRunKey = KeyCode.LeftControl;
    /// <summary>
    /// ��ǽ������
    /// </summary>
    private bool upwardsRunning;
    /// <summary>
    /// ��ǽ����
    /// </summary>
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    /// <summary>
    /// ǽ������ж�
    /// </summary>
    [Header("Detection")]
    public float wallCheckDistance;
    /// <summary>
    /// ��С��Ծ�߶�
    /// </summary>
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
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
    /// �ж��Ƿ�Զ�����
    /// </summary>
    /// <returns></returns>
    public bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    public void stateMachine()
    {
        // ��ȡ��������
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // ״̬һ - wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!pm.wallrunning)
            {
                StartWallRun();
            }
        }
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
    }

    /// <summary>
    /// ��ǽ����
    /// </summary>
    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

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
    }

    private void StopWallRun()
    {
        pm.wallrunning = false;
    }
}
