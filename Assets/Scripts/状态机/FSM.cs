using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态类型
/// </summary>
public enum StateType
{
    /// <summary>
    /// 待机
    /// </summary>
    Idle, 
    /// <summary>
    /// 巡逻
    /// </summary>
    Patrol, 
    /// <summary>
    /// 追逐
    /// </summary>
    Chase, 
    /// <summary>
    /// 反应
    /// </summary>
    React, 
    /// <summary>
    /// 攻击
    /// </summary>
    Attack
}

[Serializable]// 可序列化
public class Parameter
{
    /// <summary>
    /// 生命值
    /// </summary>
    public int health;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// 追杀速度
    /// </summary>
    public float chaseSpeed;
    /// <summary>
    /// 发呆时间
    /// </summary>
    public float idleTime;
    /// <summary>
    /// 巡逻位置
    /// </summary>
    public Transform[] patrolPoints;
    /// <summary>
    /// 追杀位置
    /// </summary>
    public Transform[] chasePoints;
    public Animator animator;
}

/// <summary>
/// 状态机管理类
/// </summary>
public class FSM : MonoBehaviour
{
    public Parameter parameter;
    private IState currentState;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TransitionState(StateType stateType)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[stateType];   
        currentState.OnEnter();
    }
}
