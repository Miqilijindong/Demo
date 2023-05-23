using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    // Input 
    private int inputX;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grapInput;
    private bool dashInput;

    // Check
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isJumping;
    private bool isTouchingLedge;

    /// <summary>
    /// 土狼时间---在离开地面时，还有一定的时间能让玩家跳跃
    /// </summary>
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.collisionSenses.Ground;
        isTouchingWall = core.collisionSenses.WallFront;
        isTouchingWallBack = core.collisionSenses.WallBack;
        isTouchingLedge = core.collisionSenses.Ledge;

        if (isTouchingWall && !isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        /**
         * 当玩家墙跳后，进入wallJumpState后，但是oldIsTouchingWall和oldIsTouchWallBack没有改，导致还是true。
         * 当isAbilityDone = true时，进入InAirState后，就还是oldIsTouchingWall和oldIsTouchingWallBack还会保持原样。
         * DoCheck()时，又会重新进入wallJumpCoyoteTime(),导致可以二段跳
         */
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        inputX = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;
        grapInput = player.inputHandler.GrabInput;
        dashInput = player.inputHandler.DashInput;

        CheckJumpMultiplier();

        if (player.inputHandler.AttackInputs[(int)CombatInputs.primary])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.AttackInputs[(int)CombatInputs.secondary])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (isGrounded && core.movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            //player.inputHandler.UseJumpInput();

            StopWallJumpCoyoteTime();
            // 这里是因为DoCheck()是根据PhysicsUpdate()的，所以导致后续判断错误，所以这里要重新获取一次
            isTouchingWall = core.collisionSenses.WallFront;
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {
            //player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isTouchingWall && grapInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (isTouchingWall && inputX == core.movement.faceingDirection && core.movement.currentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else if (dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else
        {
            core.movement.CheckIfShouldFlip(inputX);
            core.movement.SetVelocityX(playerData.movementVelocity * inputX);

            player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                core.movement.SetVelocityY(core.movement.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (core.movement.currentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DecreaseAmoutOfJumpLeft();
        }
    }

    /// <summary>
    /// 开始计算土狼时间
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
            player.jumpState.DecreaseAmoutOfJumpLeft();
        }
    }

    /// <summary>
    /// 开始计算靠墙跳跃的土狼时间
    /// </summary>
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    /// <summary>
    /// 停止计算靠墙跳跃的土狼时间
    /// </summary>
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

}
