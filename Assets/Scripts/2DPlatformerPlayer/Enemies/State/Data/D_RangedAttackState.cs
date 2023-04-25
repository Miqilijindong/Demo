using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
public class D_RangedAttackState : ScriptableObject
{
    public GameObject projectile;
    public float projectileDamage = 10f;
    public float projectileSpeed = 12f;
    /// <summary>
    /// 抛射物的行动距离
    /// </summary>
    public float projectileTravelDistance = 8f ;

}
