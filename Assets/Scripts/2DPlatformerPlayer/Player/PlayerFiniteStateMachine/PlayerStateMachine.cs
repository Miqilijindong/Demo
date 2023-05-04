using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���״̬��
/// </summary>
public class PlayerStateMachine
{
    /// <summary>
    /// ��ǰ״̬
    /// </summary>
    public PlayerState CurrentState { get; private set; }

    /// <summary>
    /// ��ʼ��״̬
    /// </summary>
    /// <param name="startingState"></param>
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    /// <summary>
    /// �ı�״̬
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
