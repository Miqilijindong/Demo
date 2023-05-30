using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ½üÕ½¹¥»÷×´Ì¬Àà
/// </summary>
public class MeleeAttackState : AttackState
{
    protected D_MeleeAttackState stateData;

    //protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //attackDetails.damageAmount = stateData.attackDamage;
        //attackDetails.position = entity.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();
    }

    public override void TriggerAttact()
    {
        base.TriggerAttact();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (var collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            IKnocakbackable knocakbackable = collider.GetComponent<IKnocakbackable>();

            if (damageable != null)
            {
                damageable.Damage(stateData.attackDamage);
            }

            if (knocakbackable != null)
            {
                knocakbackable.Knockback(stateData.knockbackAngle, stateData.knockbackStrength, core.Movement.facingDirection);
            }
        }
    }
}
