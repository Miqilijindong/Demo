using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ�����ݽű���
/// </summary>
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    /// <summary>
    /// ǽ�ڼ�����
    /// </summary>
    public float wallCheckDistance = 0.2f;
    /// <summary>
    /// ���������
    /// </summary>
    public float ledgeCheckDistance = 0.4f;

    /// <summary>
    /// �����������---���뾯�����������ܻ����ʱ�䵥λ�ڱ��ս��ģʽ
    /// </summary>
    public float maxAgroDistance = 5f;
    /// <summary>
    /// �����С������---ֱ�ӽ���ս��ģʽ
    /// </summary>
    public float minAgroDistance = 3f;

    /// <summary>
    /// 
    /// </summary>
    public float closeRangeActionDistance = 1f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
