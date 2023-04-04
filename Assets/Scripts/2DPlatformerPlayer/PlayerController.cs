using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;
    public float playerMoveSpeed;
    public float jumpForce;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f;
    public float variableJumpHeightMultiplier = 0.5f;

    /// <summary>
    /// 可跳跃次数
    /// </summary>
    public int amountOfJumps = 1;

    public float horizontal;
    /// <summary>
    /// 地面检测半径
    /// </summary>
    public float groundCheckRadius;
    /// <summary>
    /// 靠墙检测直径
    /// </summary>
    public float wallCheckDistance;
    public float wallHopForce;
    public float wallJumpForce;
    public float jumpTimerSet = 0.15f;
    public float turnTimerSet = 0.1f;
    public float wallJumpTimerSet = 0.5f;
    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;

    public LayerMask whatIsGround;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    /// <summary>
    /// 还剩跳跃次数
    /// </summary>
    [SerializeField]
    private int amountOfJumpsLeft;
    /// <summary>
    /// 当前面对的方向
    /// 1 = 右   -1 = 左
    /// </summary>
    private int facingDirection = 1;
    /// <summary>
    /// 判断最后一个墙跳的方向
    /// </summary>
    private int lastWallJumpDirection;

    private bool isFaceRight = true;
    private bool isRun;
    private bool isGround;
    private bool isTouchWall;
    private bool canNormalJump;
    private bool canWallJump;
    private bool isWallSliding;
    /// <summary>
    /// 尝试跳跃，当在空中时，按下跳跃键
    /// 类似于预输入的样子
    /// </summary>
    private bool isAttemptingToJump;
    /// <summary>
    /// 跳跃后计算按键时间，判断是否大跳
    /// </summary>
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJump;
    private bool isTouchingLedge;
    /// <summary>
    /// 判断是否能爬墙
    /// </summary>
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool isDashing;

    private float jumpTimer;
    private float turnTimer;
    private float wallJumpTimer;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;

        // Vector3.normalized的作特点是当前向量是不改变的并且返回一个新的规范化的向量；Vector3.Normalize的特点是改变当前向量，也就是当前向量长度是1
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyNewMovement();
        UpdateAnimations();
        CheckSurroundings();
    }

    private void CheckInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if ((isGround || (amountOfJumpsLeft > 0 && isTouchWall)))
            {
                NormalJump();
            }
            else
            {// 判断是否预输入，并加入时间判断
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && isTouchWall)
        {
            if (!isGround && horizontal != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0 && !canClimbLedge)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        //if (Input.GetButtonUp("Jump"))
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                AttemptToDash();
            }
        }
    }

    /// <summary>
    /// 准备冲刺
    /// </summary>
    private void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;

                // 这里从此方向后期可以改改，改成可操控的比较合适
                rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
                dashTimeLeft -= Time.deltaTime;

                if (MathF.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }
    }

    Vector3 localScale;
    /// <summary>
    /// 旧版的移动方法
    /// </summary>
    private void ApplyMovement()
    {
        /*if (horizontal != 0)
        {
            localScale = rb.transform.localScale;
            // 这里不建议直接使用horizontal，因为后期如果加上了法相天地等变大的技能，转向就会吧Scale变回1
            localScale.x = MathF.Sign(horizontal);
            rb.transform.localScale = localScale;
        }*/

        // 只有在地上才可以改变移动
        if (isGround)
        {
            rb.velocity = new Vector2(horizontal * playerMoveSpeed, rb.velocity.y);
        }
        // 空中移动
        else if (!isGround && !isWallSliding && horizontal != 0)
        {
            Vector2 forceToAdd = new Vector2(horizontal * movementForceInAir, 0);
            rb.AddForce(forceToAdd);

            // 这里空中控制速度，因为float不精确，导致有时候会true，直接转向，所以要强转int
            if (((int)Mathf.Abs(rb.velocity.x)) > playerMoveSpeed)
            {
                rb.velocity = new Vector3(playerMoveSpeed * horizontal, rb.velocity.y);
            }
        }
        // 空中有空气阻力
        else if (!isGround && !isWallSliding && horizontal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    /// <summary>
    /// 优化后的移动方法
    /// </summary>
    private void ApplyNewMovement()
    {
        /*if (horizontal != 0)
        {
            localScale = rb.transform.localScale;
            // 这里不建议直接使用horizontal，因为后期如果加上了法相天地等变大的技能，转向就会吧Scale变回1
            localScale.x = MathF.Sign(horizontal);
            rb.transform.localScale = localScale;
        }*/
        if (!isGround && !isWallSliding && horizontal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        // 只有在地上才可以改变移动
        else if (canMove)
        {
            rb.velocity = new Vector2(horizontal * playerMoveSpeed, rb.velocity.y);
        }


        if (isWallSliding)
        {
            if (rb.velocity.y < -wallSlidingSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }
        }
    }

    private void CheckIfCanJump()
    {
        // 这里要判断是否在地上，同时也要判断Y速率是否为负数，如果是向上的话，就不能补充跳跃次数
        if (isGround && rb.velocity.y <= 0.01f)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (isTouchWall)
        {
            canWallJump = true;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    private void CheckMovementDirection()
    {
        if (isFaceRight && horizontal < 0)
        {
            Flip();
        }
        else if (!isFaceRight && horizontal > 0)
        {
            Flip();
        }

        /*if (MathF.Abs(rb.velocity.x) >= 0.01f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }*/
    }

    private void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFaceRight = !isFaceRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Jump()
    {
        if (canNormalJump && !isWallSliding/* && rb.velocity.y <= 0*/)
        {
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        else if (isWallSliding && horizontal == 0 && canNormalJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isTouchWall) && horizontal != 0 && canNormalJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * horizontal, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    private void CheckJump()
    {
        if (jumpTimer > 0)
        {// 当预输入跳跃时间内判断符合跳跃条件，就跳跃
            if (!isGround && isTouchWall && horizontal != 0 && horizontal != facingDirection)
            {
                WallJump();
            }
            else if (isGround)
            {
                NormalJump();
            }
        }
        // 当预输入跳跃后，会持续减时间，直到下次跳跃，会很消耗性能，不是很建议这种方法
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        // 墙跳的时间
        if (wallJumpTimer > 0)
        {
            // 当之前墙跳过且向着墙的方向移动的话，向上的力会被取消，意思是单墙不能向上跳
            if (hasWallJump && horizontal == -lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                hasWallJump = false;
            }
            else if (wallJumpTimer <= 0f)
            {
                hasWallJump = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (canNormalJump)
        {
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        // 这里要判断不能是爬墙状态，不然会导致canFlip = true，就可以在爬墙的时候转向了
        if (canWallJump && !canClimbLedge)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * horizontal, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingToJump = false;
            checkJumpMultiplier = true;

            turnTimer = 0;
            canMove = true;
            canFlip = true;

            hasWallJump = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;
        }
    }

    private void CheckIfWallSliding()
    {
        // 贴着墙，不在地面上，向下掉落时
        if (isTouchWall && horizontal == facingDirection && rb.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if (ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFaceRight)
            {
                // Mathf.Floor()返回向下取整
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                // Mathf.Ceil()返回向上取整
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }

            canMove = false;
            canFlip = false;

            animator.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("IsRun", horizontal != 0);
        animator.SetBool("IsGround", isGround);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("IsWallSliding", isWallSliding);
    }

    /// <summary>
    /// 这个是由动画函数调用的
    /// 当动画播放结束后调用
    /// </summary>
    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        animator.SetBool("canClimbLedge", canClimbLedge);
    }

    private void CheckSurroundings()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (isTouchWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnAbleFlip()
    {
        canFlip = true;
    }

    Vector2 wallCheckPointPos;
    private void OnDrawGizmos()
    {
        wallCheckPointPos = wallCheck.position;
        wallCheckPointPos.x += wallCheckDistance * (isFaceRight ? 1 : -1);


        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(transform.position, wallCheckPointPos);
    }

    public int GetFacingDirection()
    {
        return facingDirection;
    }
}
