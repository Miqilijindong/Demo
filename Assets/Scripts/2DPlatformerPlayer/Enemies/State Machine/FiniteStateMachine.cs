using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬��
/// ���裺1�������µ�״̬��(��Ҫ�̳�State.cs)���磺IdleState.cs
/// 2������״̬�����ࡣ�磺D_IdleState.cs
/// 3������״̬�������Ʒ�ʽ���磺E1_IdleState.cs
/// 4���ڵ�����������״̬�ࡣ�磺Enemy1.idleState
/// 5�����ö�Ӧ�Ķ�������
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
