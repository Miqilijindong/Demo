using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ս��������
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
    private float inputTime, attack1Radius, attack1Damage;
    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];

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
            // �ȴ���һ�����룬�����Ϳ�����������2
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

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
    /// �˺�����
    /// </summary>
    /// <param name="attackDetails">0.���˺���ֵ��1.�����ķ���</param>
    private void Damage(float[] attackDetails)
    {
        if (PC.GetDashStatus())
        {
            return;
        }

        int direction;

        PS.DecreaseHealth(attackDetails[0]);

        if (attackDetails[1] < transform.position.x)
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