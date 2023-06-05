using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public Animator anim { get; private set; }
    //public GameObject aliveGo { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public int lastDamageDirection { get; private set; }
    public Core core { get; private set; }

    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerDetectedCheck;
    [SerializeField]
    private Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    public virtual void Awake()
    {
        core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;


        //aliveGo = transform.Find("Alive").gameObject;
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Movement.rb.velocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUPdate();
    }

    /*/// <summary>
    /// 检测墙壁
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    /// <summary>
    /// 检测地面
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }*/

    /// <summary>
    /// 检测玩家最小距离
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerDetectedCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 检测玩家最大距离
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerDetectedCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    /// <summary>
    /// 检测接近距离里是否存在玩家 
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerDetectedCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.rb.velocity.x, velocity);
        Movement.rb.velocity = velocityWorkspace;
    }

    /// <summary>
    /// 重置眩晕抗性
    /// </summary>
    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void OnDrawGizmos()
    {
        if (core != null)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Movement.facingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerDetectedCheck.position + (Vector3.right * Movement.facingDirection * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerDetectedCheck.position + (Vector3.right * Movement.facingDirection * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerDetectedCheck.position + (Vector3.right * Movement.facingDirection * entityData.maxAgroDistance), 0.2f);
        }
    }
}
