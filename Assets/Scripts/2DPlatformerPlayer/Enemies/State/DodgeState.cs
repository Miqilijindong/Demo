using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 闪避状态
/// </summary>
public class DodgeState : State
{
    protected D_DodgeState stateData;

    protected bool performCloseRangeAction;
    /// <summary>
    /// 检测玩家是否在最大距离之内
    /// </summary>
    protected bool isPlayerInMaxAgroRange;
    protected bool isGround;
    /// <summary>
    /// 是否闪避结束
    /// </summary>
    protected bool isDodgeOver;

    public DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isGround = entity.CheckGround();
        
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;

        entity.SetVelocity(stateData.dodgeSpeed, stateData.dodgeAngle, -entity.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.dodgeTime && isGround)
        {
            isDodgeOver = true;
        }
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();
    }
}
