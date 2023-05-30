using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected Core core;

    public float startTime { get; protected set; }

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        core = entity.core;
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
    /// ¼ì²â
    /// </summary>
    public virtual void DoChecks()
    {

    }
}
