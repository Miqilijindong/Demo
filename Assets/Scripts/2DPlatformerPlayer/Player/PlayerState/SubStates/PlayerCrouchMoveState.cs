using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlatformerPlayer.Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            core.movement.SetVelocityX(playerData.crouchMovementVelocity * core.movement.faceingDirection);
            core.movement.CheckIfShouldFlip(inputX);

            if (inputX == 0)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else if (inputY != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.moveState);
            }
        }
    }
}
