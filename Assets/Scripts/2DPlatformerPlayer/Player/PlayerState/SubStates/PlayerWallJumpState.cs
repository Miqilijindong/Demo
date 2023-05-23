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
        core.movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        core.movement.CheckIfShouldFlip(wallJumpDirection);
        player.jumpState.DecreaseAmoutOfJumpLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    /// <summary>
    /// ¼ì²âÇ½ÌøÔ¾µÄ·½Ïò
    /// </summary>
    /// <param name="isTouchingWall"></param>
    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.movement.faceingDirection;
        }
        else
        {
            wallJumpDirection = core.movement.faceingDirection;
        }
    }
}
