using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ�����ݽű���
/// </summary>
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30;
    public float damageHopSpeed = 3f;
    /// <summary>
    /// ǽ�ڼ�����
    /// </summary>
    public float wallCheckDistance = 0.2f;
    /// <summary>
    /// ���������
    /// </summary>
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    /// <summary>
    /// �����������---���뾯�����������ܻ����ʱ�䵥λ�ڱ��ս��ģʽ
    /// </summary>
    public float maxAgroDistance = 5f;
    /// <summary>
    /// �����С������---ֱ�ӽ���ս��ģʽ
    /// </summary>
    public float minAgroDistance = 3f;

    public float stunResistance = 3f;
    public float stunRecoveryTime = 2f;

    /// <summary>
    /// 
    /// </summary>
    public float closeRangeActionDistance = 1f;

    /// <summary>
    /// �ܻ���Ч
    /// </summary>
    public GameObject hitParticle;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
