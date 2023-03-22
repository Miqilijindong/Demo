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
    public float playerMoveSpeed;
    public float jumpForce;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMultiplier = 0.95f ;
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

    public LayerMask whatIsGround;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    /// <summary>
    /// 还剩跳跃次数
    /// </summary>
    [SerializeField]
    private int amountOfJumpsLeft;
    private int facingDirection = 1 ;
    private bool isRun;
    private bool isGround;
    private bool isTouchWall;
    private bool isFaceRight = true;


    private bool canJump;
    private bool isWallSliding;

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
        InputController();
        CheckMovementDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        UpdateAnimations();
        CheckSurroundings();
    }

    private void InputController()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    float test;
    Vector3 localScale;
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
        else if (!isGround && !isWallSliding && horizontal != 0)
        {
            Vector2 forceToAdd = new Vector2(horizontal * movementForceInAir, 0);
            rb.AddForce(forceToAdd);

            // 这里以为float不精确，导致有时候会true，直接转向，所以要强转int
            if (((int)Mathf.Abs(rb.velocity.x)) > playerMoveSpeed)
            {
                rb.velocity = new Vector3(playerMoveSpeed * horizontal, rb.velocity.y);
            }
        }
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

        if (test != 0 && test == rb.velocity.x * -1)
        {
            Debug.Log("123");
        }
    }

    private void CheckIfCanJump()
    {
        // 这里要判断是否在地上，同时也要判断Y速率是否为负数，如果是向上的话，就不能补充跳跃次数
        if (isGround && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft > 0)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
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
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            facingDirection *= -1;
            isFaceRight = !isFaceRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Jump()
    {
        if (canJump/* && rb.velocity.y <= 0*/)
        {
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
        }
        else if (isWallSliding && horizontal == 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        else if ((isWallSliding || isTouchWall) && horizontal != 0 && canJump)
        {
            isWallSliding = false;
            amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * horizontal, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);

        }
    }

    private void CheckIfWallSliding()
    {
        // 贴着墙，不在地面上，向下掉落时
        if (isTouchWall && !isGround && rb.velocity.y <= 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("IsRun", horizontal != 0);
        animator.SetBool("IsGround", isGround);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("IsWallSliding", isWallSliding);
    }

    private void CheckSurroundings()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
    }

    Vector2 wallCheckPointPos;
    private void OnDrawGizmos()
    {
        wallCheckPointPos = wallCheck.position;
        wallCheckPointPos.x += wallCheckDistance * (isFaceRight ? 1 : -1);


        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(transform.position, wallCheckPointPos);
    }
}
