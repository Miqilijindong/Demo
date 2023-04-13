using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState playerDetectedState;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;


    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState playerDetectedState) : base(entity, stateMachine, animBoolName)
    {
        this.playerDetectedState = playerDetectedState;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
    }
}
