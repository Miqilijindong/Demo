using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������״̬��
/// </summary>
public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    /// <summary>
    /// �Ƿ�����ת��
    /// </summary>
    protected bool turnImmediatyly;
    /// <summary>
    /// �ж�����Ƿ���С������
    /// </summary>
    protected bool isPlayerInMinAgroRange;
    /// <summary>
    /// �Ƿ�ȫ��ִ�����
    /// </summary>
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityX(0f);

        if (turnImmediatyly)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            turnImmediatyly = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            core.Movement.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();
    }

    /// <summary>
    /// ��������ת��
    /// </summary>
    /// <param name="flip"></param>
    public void SetTurnImmediately(bool flip)
    {
        turnImmediatyly = flip;
    }
}
