using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private int inputX;
    private bool jumpInput;
    private bool jumpInputStop;
    /// <summary>
    /// 土狼时间---在离开地面时，还有一定的时间能让玩家跳跃
    /// </summary>
    private bool coyoteTime;
    private bool isJumping;

    public PlayerInAirState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();

        inputX = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;

        CheckJumpMultiplier();

        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
            player.inputHandler.UseJumpInput();
        }
        else
        {
            player.CheckIfShouldFlip(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);

            player.anim.SetFloat("yVelocity", player.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f)
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

}
