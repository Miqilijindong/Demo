using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlatformerPlayer.Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimatinoFinshed;

    protected float startTime;

    private string animBoolName;

    public PlayerState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        player.anim.SetBool(animBoolName, true);
        Debug.Log("Enter:" + animBoolName);
        startTime = Time.time;

        isAnimatinoFinshed = false;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
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
