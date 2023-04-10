using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 假人模型控制类
/// </summary>
public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockBackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField]
    private bool applyKnockBack;
    [SerializeField]
    private GameObject hitParticle;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerController pc;
    private GameObject aliveGO, brokenTopGO, BrokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        BrokenBotGO = transform.Find("Broken Buttom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = BrokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        BrokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float[] details)
    {
        currentHealth -= details[0];
        //playerFacingDirection = pc.GetFacingDirection();

        if (details[1] < aliveGO.transform.position.x)
        {
            playerFacingDirection = 1;
        }
        else
        {
            playerFacingDirection = -1;
        }

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

        if (playerFacingDirection == 1)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockBack && currentHealth > 0.0f)
        {
            // knockBack
            Knockback();
        }
        if (currentHealth <= 0.0f)
        {
            // die 
            Die();
        }
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockBackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        BrokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        BrokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);

        // 增加轴转力
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
