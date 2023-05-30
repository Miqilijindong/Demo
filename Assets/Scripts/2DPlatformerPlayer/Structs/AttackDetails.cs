using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 position;
    public float damageAmount;
    public float stunDamageAmount;
}

[Serializable]
public struct WeaponAttackDetails
{
    /// <summary>
    /// ��������
    /// </summary>
    [Tooltip("��������")]
    public string attackName;
    /// <summary>
    /// ����ʱ�ƶ��ٶ�
    /// </summary>
    [Tooltip("����ʱ�ƶ��ٶ�")]
    public float movementSpeed;
    /// <summary>
    /// �����˺�
    /// </summary>
    [Tooltip("�����˺�")]
    public float damageAmount;

    public float knockbackStrenght;
    public Vector2 knockbackAngle;
}
