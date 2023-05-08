using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlatformerPlayer.Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (inputX != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if (isAnimatinoFinshed)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }
}
