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
    /// ¹¥»÷Ãû³Æ
    /// </summary>
    [Tooltip("¹¥»÷Ãû³Æ")]
    public string attackName;
    /// <summary>
    /// ¹¥»÷Ê±ÒÆ¶¯ËÙ¶È
    /// </summary>
    [Tooltip("¹¥»÷Ê±ÒÆ¶¯ËÙ¶È")]
    public float movementSpeed;
    /// <summary>
    /// ¹¥»÷ÉËº¦
    /// </summary>
    [Tooltip("¹¥»÷ÉËº¦")]
    public float damageAmount;

    public float knockbackStrenght;
    public Vector2 knockbackAngle;
}
