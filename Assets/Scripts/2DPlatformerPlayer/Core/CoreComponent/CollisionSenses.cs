using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{

    #region Check Transforms
    public Transform GroundCheck
    {
        get
        {
            if (groundCheck)
            {
                return groundCheck;
            }

            Debug.LogError("No Ground Check on " + core.transform.parent.name);
            return null;
        }
        private set => groundCheck = value;
    }
    public Transform WallCheck
    {
        get
        {
            if (wallCheck)
            {
                return wallCheck;
            }

            Debug.LogError("No Wall Check on " + core.transform.parent.name);
            return null;
        }
        private set => wallCheck = value;
    }
    public Transform LedgeCheckHorizontal
    {
        get
        {
            if (ledgeCheckHorizontal)
            {
                return ledgeCheckHorizontal;
            }

            Debug.LogError("No  Ledge Check Horizontal on " + core.transform.parent.name);
            return null;
        }
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical
    {
        get
        {
            if (ledgeCheckVertical)
            {
                return ledgeCheckVertical;
            }

            Debug.LogError("No  Ledge Check Vertical on " + core.transform.parent.name);
            return null;
        }
        private set => ledgeCheckVertical = value;
    }
    public Transform CeilingCheck
    {
        get
        {
            if (ceilingCheck)
            {
                return ceilingCheck;
            }

            Debug.LogError("No  ceiling Check on " + core.transform.parent.name);
            return null;
        }
        private set => ceilingCheck = value;
    }
    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheckHorizontal;
    [SerializeField]
    private Transform ledgeCheckVertical;
    [SerializeField]
    private Transform ceilingCheck;

    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private float wallCheckDistance;

    [SerializeField]
    private LayerMask whatIsGround;

    #endregion

    #region Check Functions

    /*public bool CheckForCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
    }*/

    public bool Ceiling
    {
        get => Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, whatIsGround);
    }

    /*public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }*/

    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /*public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * core.movement.faceingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.movement.faceingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -core.movement.faceingDirection, wallCheckDistance, whatIsGround);
    }*/

    public bool WallFront
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 检测水平方向的悬崖
    /// </summary>
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// 检测垂直方向的悬崖
    /// </summary>
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.facingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion
}
