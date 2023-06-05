using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ҿ�ֹ״̬��
/// </summary>
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlatformerPlayer.Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityX(0);
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
            if (inputX != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if (inputY == -1)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
