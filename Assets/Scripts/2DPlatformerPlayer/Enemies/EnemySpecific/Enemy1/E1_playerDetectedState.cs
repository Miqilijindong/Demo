using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_playerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_playerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState playerDetectedState, Enemy1 enemy) : base(entity, stateMachine, animBoolName, playerDetectedState)
    {
        this.enemy = enemy;
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

        if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();
    }
}
