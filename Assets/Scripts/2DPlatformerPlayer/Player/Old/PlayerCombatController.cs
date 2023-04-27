using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家战斗控制类
/// </summary>
public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;

    [SerializeField]
    private bool combatEnable;
    private bool gotInput, isAttacking, isFirstAttack;
    [SerializeField]
    private float stunDamageAmount = 1f;

    [SerializeField]
    private float inputTime, attack1Radius, attack1Damage;
    private float lastInputTime = Mathf.NegativeInfinity;

    private AttackDetails attackDetails = new AttackDetails();

    private Animator anim;

    private PlayerController PC;
    private PlayerStats PS;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnable);
        PC = GetComponent<PlayerController>();
        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnable)
            {
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time >= lastInputTime + inputTime)
        {
            // 等待下一个输入，这样就可以派生攻击2
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack1()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    /// <summary>
    /// 伤害计算
    /// </summary>
    /// <param name="attackDetails">0.是伤害数值，1.攻击的方向</param>
    private void Damage(AttackDetails attackDetails)
    {
        if (PC.GetDashStatus())
        {
            return;
        }

        int direction;

        PS.DecreaseHealth(attackDetails.damageAmount);

        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        PC.Knockback(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
