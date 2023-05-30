using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击状态类
/// </summary>
public class AttackState : State
{
    protected Transform attackPosition;

    protected bool isAnimationFinised;
    protected bool isPlayerInMinAgroRange;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinised = false;
        core.Movement.SetVelocityX(0f);
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
    }

    public virtual void TriggerAttact()
    {

    }

    /// <summary>
    /// 动画完成后调用
    /// 由于该脚本是通过Enemy1.cs初始化E_AttackState.cs生成的，所以Alive实体是无法直接调用该函数
    /// 只能在实体挂载脚本后，通过脚本调用该函数
    /// </summary>
    public virtual void FinishAttack()
    {
        isAnimationFinised = true;
    }
}
