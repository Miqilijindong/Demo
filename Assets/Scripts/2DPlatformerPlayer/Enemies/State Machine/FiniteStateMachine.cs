using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限状态机
/// 步骤：1、创建新的状态类(需要继承State.cs)。如：IdleState.cs
/// 2、创建状态数据类。如：D_IdleState.cs
/// 3、创建状态类具体控制方式。如：E1_IdleState.cs
/// 4、在敌人类里声明状态类。如：Enemy1.idleState
/// 5、设置对应的动画控制
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
