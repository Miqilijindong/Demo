using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ´ý»ú×´Ì¬
/// </summary>
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}

/// <summary>
/// ´ý»ú×´Ì¬
/// </summary>
public class PatrolState : IState
{
    private FSM manager;
    private Parameter parameter;

    public PatrolState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}

/// <summary>
/// ´ý»ú×´Ì¬
/// </summary>
public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}


/// <summary>
/// ´ý»ú×´Ì¬
/// </summary>
public class ReactState : IState
{
    private FSM manager;
    private Parameter parameter;

    public ReactState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
