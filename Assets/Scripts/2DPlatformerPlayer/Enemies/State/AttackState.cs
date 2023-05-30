using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬��
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
    /// ������ɺ����
    /// ���ڸýű���ͨ��Enemy1.cs��ʼ��E_AttackState.cs���ɵģ�����Aliveʵ�����޷�ֱ�ӵ��øú���
    /// ֻ����ʵ����ؽű���ͨ���ű����øú���
    /// </summary>
    public virtual void FinishAttack()
    {
        isAnimationFinised = true;
    }
}
