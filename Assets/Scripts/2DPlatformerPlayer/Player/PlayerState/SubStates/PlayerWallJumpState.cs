using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        player.jumpState.ResetAmountOfJumpsLeft();
        Movement?.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        Movement?.CheckIfShouldFlip(wallJumpDirection);
        player.jumpState.DecreaseAmoutOfJumpLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", Movement.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(Movement.currentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    /// <summary>
    /// ���ǽ��Ծ�ķ���
    /// </summary>
    /// <param name="isTouchingWall"></param>
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Movement.facingDirection;
        }
        else
        {
            wallJumpDirection = Movement.facingDirection;
        }
    }
}
