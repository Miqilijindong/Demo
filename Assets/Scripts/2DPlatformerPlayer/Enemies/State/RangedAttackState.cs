using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : AttackState
{
    protected D_RangedAttackState stateData;

    protected GameObject projectile;
    protected PlatformerPlayer.Projectile projectileScript;

    public RangedAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangedAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<PlatformerPlayer.Projectile>();
        projectileScript.FireProjectil(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
    }
}
