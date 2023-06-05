using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private CollisionSenses collisionSenses;
    private Movement movement;

    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;
    private Vector2 workspace;

    /// <summary>
    /// 是否悬挂
    /// </summary>
    private bool isHanging;
    /// <summary>
    /// 是否爬上墙角
    /// </summary>
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int inputX;
    private int inputY;

    public PlayerLedgeClimbState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = DetermineCornerPosition();

        startPos.Set(cornerPos.x - (Movement.facingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (Movement.facingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimatinoFinshed)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
        else
        {
            inputX = player.inputHandler.NormInputX;
            inputY = player.inputHandler.NormInputY;
            jumpInput = player.inputHandler.JumpInput;

            Movement?.SetVelocityZero();
            player.transform.position = startPos;

            if (inputX == Movement.facingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.anim.SetBool("climbLedge", true);
            }
            else if (inputY == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.inAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                player.wallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.wallJumpState);
            }
        }

    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    public void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * Movement.facingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, CollisionSenses.WhatIsGround);

        player.anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }

    private Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(CollisionSenses.WallCheck.position, Vector2.right * Movement.facingDirection, CollisionSenses.WallCheckDistance, CollisionSenses.WhatIsGround);
        float xDist = xHit.distance;

        workspace.Set((xDist + 0.015f) * Movement.facingDirection, 0f);
        //workSpace.Set(xDist * faceingDirection, 0f);// 这里不知道为什么有问题，会导致墙角抓墙位置有问题
        RaycastHit2D yHit = Physics2D.Raycast(CollisionSenses.LedgeCheckHorizontal.position + (Vector3)(workspace), Vector2.down, CollisionSenses.LedgeCheckHorizontal.position.y - CollisionSenses.WallCheck.position.y + 0.015f, CollisionSenses.WhatIsGround);
        //RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workSpace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y, playerData.whatIsGround);// 这里不知道为什么有问题，会导致墙角抓墙位置有问题
        float yDist = yHit.distance;

        workspace.Set(CollisionSenses.WallCheck.position.x + (xDist * Movement.facingDirection), CollisionSenses.LedgeCheckHorizontal.position.y - yDist);

        return workspace;
    }
}
