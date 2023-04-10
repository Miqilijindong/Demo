using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˿��ƻ���
/// </summary>
public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, movementSpeed, maxHealth, knockbackDuration, lastTouchDamageTime, touchDamageCooldown, touchDamage, touchDamageWidth, touchDamageHeight;
    [SerializeField]
    private Transform groundCheck, wallCheck, touchDamageCheck;
    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField]
    private Vector2 knockbackSpeed;
    [SerializeField]
    private GameObject hitParticle, deathChunkparticle, deathBloodParticle;

    private float currentHealth, knockbackStartTime;

    private float[] attackDetails = new float[2];

    private int faceDirection, damageDiraction;
    private Vector2 movement, touchDamageBotLeft, touchDamageTopRight;

    private bool groundDetected, wallDetected;

    private GameObject alive;
    private Rigidbody2D aliveRb;
    private Animator aliveAnim;

    private void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth;
        faceDirection = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    #region WALKING STATE

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * faceDirection, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            // Flip
            Flip();
        }
        else
        {
            // Move
            movement.Set(movementSpeed * faceDirection, aliveRb.velocity.y);
            aliveRb.velocity = movement;
        }
    }

    private void ExitMovingState()
    {

    }

    #endregion

    #region KNOCKBACK STATE

    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDiraction, knockbackSpeed.y);
        aliveRb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("Knockback", false);
    }

    #endregion

    #region DEAD STATE

    private void EnterDeadState()
    {
        Instantiate(deathChunkparticle, alive.transform.position, deathChunkparticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    #endregion

    #region OTHER FUNCTION

    /// <summary>
    /// �˺����㣬��������[0]����
    /// �ܹ����ķ���������[1]���ж�
    /// </summary>
    /// <param name="attackDetails"></param>
    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            damageDiraction = -1;
        }
        else
        {
            damageDiraction = 1;
        }

        // Hit particle
        if (currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Flip()
    {
        faceDirection *= -1;
        alive.transform.Rotate(0f, 180f, 0f);
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * faceDirection, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight / 2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
    }

    #endregion
}
