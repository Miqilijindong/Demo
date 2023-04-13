using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限状态机
/// </summary>
public class FiniteStateMachine
{
    public State currentState { get; private set; }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="startingState"></param>
    public void Initialize(State startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
