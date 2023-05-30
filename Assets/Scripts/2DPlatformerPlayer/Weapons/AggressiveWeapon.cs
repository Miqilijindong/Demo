using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IKnocakbackable> detectedKnockbackables = new List<IKnocakbackable>();

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }


    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable item in detectedDamageables.ToList())
        {
            item.Damage(details.damageAmount);
        }

        foreach (IKnocakbackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrenght, core.Movement.facingDirection);
        }
    }

    public void AddToDectected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        IKnocakbackable knocakbackable = collision.GetComponent<IKnocakbackable>();

        if (damageable != null)
        {
            detectedDamageables.Add(damageable);
        }


        if (knocakbackable != null)
        {
            detectedKnockbackables.Add(knocakbackable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        IKnocakbackable knocakbackable = collision.GetComponent<IKnocakbackable>();

        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }

        if (knocakbackable != null)
        {
            detectedKnockbackables.Remove(knocakbackable);
        }
    }
}
