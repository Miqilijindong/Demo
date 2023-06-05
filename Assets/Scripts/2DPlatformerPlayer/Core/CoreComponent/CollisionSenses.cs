using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{

    #region Check Transforms
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    public Transform WallCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name);
        private set => wallCheck = value;
    }
    public Transform LedgeCheckHorizontal
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.parent.name);
        private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.parent.name);
        private set => ledgeCheckVertical = value;
    }
    public Transform CeilingCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, core.transform.parent.name);
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

    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

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
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// ���ˮƽ���������
    /// </summary>
    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.facingDirection, wallCheckDistance, whatIsGround);
    }

    /// <summary>
    /// ��ⴹֱ���������
    /// </summary>
    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.facingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion
}
