using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���״̬����
/// ����״̬��Ҫ�̳������
/// </summary>
public class PlayerState
{
    protected Core core;

    protected PlatformerPlayer.Player player;
    /// <summary>
    /// ״̬��
    /// </summary>
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    /// <summary>
    /// �����Ƿ񲥷����
    /// </summary>
    protected bool isAnimatinoFinshed;
    /// <summary>
    /// �����������Ϊ��״̬�ĸ��෢����ChangeState��Ȼ�������ַ�����changeStateʱ����ֹ����ChangeState
    /// �磺������PlayerIdleState.LogicUpdateʱ���������PlayerGroundState.LogiciUpdate������ChangeState����������Ҳ�����ˣ��ͻᵼ��״̬��Ѹ���л�������ֶ�����
    /// </summary>
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core = player.core;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(animBoolName, true);
        Debug.Log("Enter:" + animBoolName);
        startTime = Time.time;

        isAnimatinoFinshed = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        isExitingState = true;
        Debug.Log("Exit:" + animBoolName);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimatinoFinshed = true;
}
