using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ≈¿«Ω
/// </summary>
public class PlayerWallClimbState : PlayerTeachingWallState
{
    public PlayerWallClimbState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.SetVelocityY(playerData.wallClimbVelocity);

            if (inputY != 1)
            {
                stateMachine.ChangeState(player.wallGrabState);
            }
        }
    }
}
