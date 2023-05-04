using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态机
/// </summary>
public class PlayerStateMachine
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public PlayerState CurrentState { get; private set; }

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <param name="startingState"></param>
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
