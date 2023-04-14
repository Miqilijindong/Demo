using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);

    }

    /// <summary>
    /// Update()
    /// </summary>
    public virtual void LogicUpdate()
    {

    }

    /// <summary>
    /// FixedUpdate()
    /// </summary>
    public virtual void PhysicsUPdate()
    {
        DoChecks();
    }

    /// <summary>
    /// ���
    /// </summary>
    public virtual void DoChecks()
    {

    }
}
