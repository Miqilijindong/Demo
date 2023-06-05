using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool canDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirtection;
    private Vector2 dashDirtectionInput;
    private Vector2 lastAfterImgePos;

    public PlayerDashState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        canDash = false;
        player.inputHandler.UseDashInput();

        isHolding = true;
        dashDirtection = Vector2.right * Movement.facingDirection;

        // �����õļ���Ч������ֱ�ӽ����������ʱ�䶼������Ч����Ȼ���ˣ����ǿ��ܻ�Ӱ�쵽������ҵ���߼������Խ����Ժ�ĵ�
        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.dashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (Movement?.currentVelocity.y > 0)
        {
            Movement?.SetVelocityY(Movement.currentVelocity.y * playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            player.anim.SetFloat("yVelocity", Movement.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(Movement.currentVelocity.x));

            if (isHolding)
            {
                dashDirtectionInput = player.inputHandler.DashDirectionInput;
                dashInputStop = player.inputHandler.DashInputStop;

                if (dashDirtectionInput != Vector2.zero)
                {
                    dashDirtection = dashDirtectionInput;
                    dashDirtection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirtection);
                // ��45����ΪͼƬ��ʼ�����������45��
                player.dashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                    Movement?.CheckIfShouldFlip(Mathf.RoundToInt(dashDirtection.x));
                    player.rb.drag = playerData.drag;
                    Movement?.SetVelocity(playerData.dashVelocity, dashDirtection);

                    player.dashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Movement?.SetVelocity(playerData.dashVelocity, dashDirtection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.rb.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAfterImgePos) >= playerData.distBetweenAfterImager)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();

        lastAfterImgePos = player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return canDash & Time.time > lastDashTime + playerData.dashCoodown;
    }

    public void ResetCanDash() => canDash = true;
}
