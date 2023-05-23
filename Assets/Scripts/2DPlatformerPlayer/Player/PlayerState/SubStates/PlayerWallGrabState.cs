using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抓墙状态
/// </summary>
public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = player.transform.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!isExitingState)
        {
            HoldPosition();

            if (inputY > 0)
            {
                stateMachine.ChangeState(player.wallClimbState);
            }
            else if (inputY < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void HoldPosition()
    {
        player.transform.position = holdPosition;

        core.movement.SetVelocityX(0);
        core.movement.SetVelocityY(0);
    }
}
