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
    /// ����Ծ����
    /// </summary>
    public int amountOfJumps = 1;

    public float horizontal;
    /// <summary>
    /// ������뾶
    /// </summary>
    public float groundCheckRadius;
    /// <summary>
    /// ��ǽ���ֱ��
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
    /// ��ʣ��Ծ����
    /// </summary>
    [SerializeField]
    private int amountOfJumpsLeft;
    /// <summary>
    /// ��ǰ��Եķ���
    /// 1 = ��   -1 = ��
    /// </summary>
    private int facingDirection = 1;
    /// <summary>
    /// �ж����һ��ǽ���ķ���
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
    /// ������Ծ�����ڿ���ʱ��������Ծ��
    /// ������Ԥ���������
    /// </summary>
    private bool isAttemptingToJump;
    /// <summary>
    /// ��Ծ����㰴��ʱ�䣬�ж��Ƿ����
    /// </summary>
    private bool checkJumpMultiplier;
    private bool canMove;
    private bool canFlip;
    private bool hasWallJump;
    private bool isTouchingLedge;
    /// <summary>
    /// �ж��Ƿ�����ǽ
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

        // Vector3.normalized�����ص��ǵ�ǰ�����ǲ��ı�Ĳ��ҷ���һ���µĹ淶����������Vector3.Normalize���ص��Ǹı䵱ǰ������Ҳ���ǵ�ǰ����������1
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
            {// �ж��Ƿ�Ԥ���룬������ʱ���ж�
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
    /// ׼�����
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

                // ����Ӵ˷�����ڿ��Ըĸģ��ĳɿɲٿصıȽϺ���
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
    /// �ɰ���ƶ�����
    /// </summary>
    private void ApplyMovement()
    {
        /*if (horizontal != 0)
        {
            localScale = rb.transform.localScale;
            // ���ﲻ����ֱ��ʹ��horizontal����Ϊ������������˷�����صȱ��ļ��ܣ�ת��ͻ��Scale���1
            localScale.x = MathF.Sign(horizontal);
            rb.transform.localScale = localScale;
        }*/

        // ֻ���ڵ��ϲſ��Ըı��ƶ�
        if (isGround)
        {
            rb.velocity = new Vector2(horizontal * playerMoveSpeed, rb.velocity.y);
        }
        // �����ƶ�
        else if (!isGround && !isWallSliding && horizontal != 0)
        {
            Vector2 forceToAdd = new Vector2(horizontal * movementForceInAir, 0);
            rb.AddForce(forceToAdd);

            // ������п����ٶȣ���Ϊfloat����ȷ��������ʱ���true��ֱ��ת������Ҫǿתint
            if (((int)Mathf.Abs(rb.velocity.x)) > playerMoveSpeed)
            {
                rb.velocity = new Vector3(playerMoveSpeed * horizontal, rb.velocity.y);
            }
        }
        // �����п�������
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
    /// �Ż�����ƶ�����
    /// </summary>
    private void ApplyNewMovement()
    {
        /*if (horizontal != 0)
        {
            localScale = rb.transform.localScale;
            // ���ﲻ����ֱ��ʹ��horizontal����Ϊ������������˷�����صȱ��ļ��ܣ�ת��ͻ��Scale���1
            localScale.x = MathF.Sign(horizontal);
            rb.transform.localScale = localScale;
        }*/
        if (!isGround && !isWallSliding && horizontal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        // ֻ���ڵ��ϲſ��Ըı��ƶ�
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
        // ����Ҫ�ж��Ƿ��ڵ��ϣ�ͬʱҲҪ�ж�Y�����Ƿ�Ϊ��������������ϵĻ����Ͳ��ܲ�����Ծ����
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
        {// ��Ԥ������Ծʱ�����жϷ�����Ծ����������Ծ
            if (!isGround && isTouchWall && horizontal != 0 && horizontal != facingDirection)
            {
                WallJump();
            }
            else if (isGround)
            {
                NormalJump();
            }
        }
        // ��Ԥ������Ծ�󣬻������ʱ�䣬ֱ���´���Ծ������������ܣ����Ǻܽ������ַ���
        if (isAttemptingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        // ǽ����ʱ��
        if (wallJumpTimer > 0)
        {
            // ��֮ǰǽ����������ǽ�ķ����ƶ��Ļ������ϵ����ᱻȡ������˼�ǵ�ǽ����������
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
        // ����Ҫ�жϲ�������ǽ״̬����Ȼ�ᵼ��canFlip = true���Ϳ�������ǽ��ʱ��ת����
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
        // ����ǽ�����ڵ����ϣ����µ���ʱ
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
                // Mathf.Floor()��������ȡ��
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                // Mathf.Ceil()��������ȡ��
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
    /// ������ɶ����������õ�
    /// ���������Ž��������
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
