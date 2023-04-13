using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬��
/// </summary>
public class FiniteStateMachine
{
    public State currentState { get; private set; }

    /// <summary>
    /// ��ʼ��
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
