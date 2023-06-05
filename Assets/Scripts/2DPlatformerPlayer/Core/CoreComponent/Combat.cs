using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗核心组件类
/// </summary>
public class Combat : CoreComponent, IDamageable, IKnocakbackable
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses
    {
        //get => collisionSenses ??= core.GetComponent<CollisionSenses>();// 当collisionSenses为空时，则执行core.GetComponent<CollisionSenses>();并赋值给collisionSenses，这是一种比较简单的方法
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }
    private Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;

    [SerializeField]
    private float maxKnockbackTime = 0.2f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        Stats?.DecreaseHealth(amount);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement?.currentVelocity.y <= 0.01f && CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
