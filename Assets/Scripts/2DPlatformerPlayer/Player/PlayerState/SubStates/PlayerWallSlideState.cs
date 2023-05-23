using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¿¿Ç½ÏÂ»¬
/// </summary>
public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (!isExitingState)
        {
            core.movement.SetVelocityY(-playerData.wallSliderVelocity);

            if (grabInput && inputY == 0)
            {
                stateMachine.ChangeState(player.wallGrabState);
            }
        }
    }
}
