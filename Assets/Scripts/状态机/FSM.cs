using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬����
/// </summary>
public enum StateType
{
    /// <summary>
    /// ����
    /// </summary>
    Idle, 
    /// <summary>
    /// Ѳ��
    /// </summary>
    Patrol, 
    /// <summary>
    /// ׷��
    /// </summary>
    Chase, 
    /// <summary>
    /// ��Ӧ
    /// </summary>
    React, 
    /// <summary>
    /// ����
    /// </summary>
    Attack
}

[Serializable]// �����л�
public class Parameter
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public int health;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// ׷ɱ�ٶ�
    /// </summary>
    public float chaseSpeed;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public float idleTime;
    /// <summary>
    /// Ѳ��λ��
    /// </summary>
    public Transform[] patrolPoints;
    /// <summary>
    /// ׷ɱλ��
    /// </summary>
    public Transform[] chasePoints;
    public Animator animator;
}

/// <summary>
/// ״̬��������
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
