using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态基类
/// 所有状态都要继承这个类
/// </summary>
public class PlayerState
{
    protected Core core;

    protected PlatformerPlayer.Player player;
    /// <summary>
    /// 状态机
    /// </summary>
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    /// <summary>
    /// 动画是否播放完毕
    /// </summary>
    protected bool isAnimatinoFinshed;
    /// <summary>
    /// 加入这个是因为当状态的父类发生了ChangeState，然后子类又发生了changeState时，禁止子类ChangeState
    /// 如：当子类PlayerIdleState.LogicUpdate时，如果父类PlayerGroundState.LogiciUpdate调用了ChangeState，并且子类也调用了，就会导致状态的迅速切换，会出现二段跳
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
