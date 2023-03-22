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

    public LayerMask whatIsGround;

    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;

    /// <summary>
    /// ��ʣ��Ծ����
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

        // Vector3.normalized�����ص��ǵ�ǰ�����ǲ��ı�Ĳ��ҷ���һ���µĹ淶����������Vector3.Normalize���ص��Ǹı䵱ǰ������Ҳ���ǵ�ǰ����������1
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
            // ���ﲻ����ֱ��ʹ��horizontal����Ϊ������������˷�����صȱ��ļ��ܣ�ת��ͻ��Scale���1
            localScale.x = MathF.Sign(horizontal);
            rb.transform.localScale = localScale;
        }*/

        // ֻ���ڵ��ϲſ��Ըı��ƶ�
        if (isGround)
        {
            rb.velocity = new Vector2(horizontal * playerMoveSpeed, rb.velocity.y);
        }
        else if (!isGround && !isWallSliding && horizontal != 0)
        {
            Vector2 forceToAdd = new Vector2(horizontal * movementForceInAir, 0);
            rb.AddForce(forceToAdd);

            // ������Ϊfloat����ȷ��������ʱ���true��ֱ��ת������Ҫǿתint
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
        // ����Ҫ�ж��Ƿ��ڵ��ϣ�ͬʱҲҪ�ж�Y�����Ƿ�Ϊ��������������ϵĻ����Ͳ��ܲ�����Ծ����
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
        // ����ǽ�����ڵ����ϣ����µ���ʱ
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
