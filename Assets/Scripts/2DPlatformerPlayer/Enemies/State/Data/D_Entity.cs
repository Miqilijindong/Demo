using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体数据脚本类
/// </summary>
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    /// <summary>
    /// 墙壁检测距离
    /// </summary>
    public float wallCheckDistance = 0.2f;
    /// <summary>
    /// 地面检测距离
    /// </summary>
    public float ledgeCheckDistance = 0.4f;

    /// <summary>
    /// 玩家最大检测距离---进入警觉，后续可能会加入时间单位内变成战斗模式
    /// </summary>
    public float maxAgroDistance = 5f;
    /// <summary>
    /// 玩家最小检测距离---直接进入战斗模式
    /// </summary>
    public float minAgroDistance = 3f;

    /// <summary>
    /// 
    /// </summary>
    public float closeRangeActionDistance = 1f;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
