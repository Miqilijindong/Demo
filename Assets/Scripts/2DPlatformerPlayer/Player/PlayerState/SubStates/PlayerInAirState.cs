using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int inputX;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool jumpInput;
    private bool grapInput;
    private bool jumpInputStop;
    /// <summary>
    /// ����ʱ��---���뿪����ʱ������һ����ʱ�����������Ծ
    /// </summary>
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        /**
         * �����ǽ���󣬽���wallJumpState�󣬵���oldIsTouchingWall��oldIsTouchWallBackû�иģ����»���true��
         * ��isAbilityDone = trueʱ������InAirState�󣬾ͻ���oldIsTouchingWall��oldIsTouchingWallBack���ᱣ��ԭ����
         * DoCheck()ʱ���ֻ����½���wallJumpCoyoteTime(),���¿��Զ�����
         */
        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        inputX = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        jumpInputStop = player.inputHandler.JumpInputStop;
        grapInput = player.inputHandler.GrabInput;

        CheckJumpMultiplier();

        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            //player.inputHandler.UseJumpInput();

            StopWallJumpCoyoteTime(); 
            // ��������ΪDoCheck()�Ǹ���PhysicsUpdate()�ģ����Ե��º����жϴ�����������Ҫ���»�ȡһ��
            isTouchingWall = player.CheckIfTouchingWall();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {
            //player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (isTouchingWall && grapInput)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (isTouchingWall && inputX == player.faceingDirection && player.currentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else
        {
            player.CheckIfShouldFlip(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);

            player.anim.SetFloat("yVelocity", player.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DecreaseAmoutOfJumpLeft();
        }
    }

    /// <summary>
    /// ��ʼ��������ʱ��
    /// </summary>
    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
            player.jumpState.DecreaseAmoutOfJumpLeft();
        }
    }

    /// <summary>
    /// ��ʼ���㿿ǽ��Ծ������ʱ��
    /// </summary>
    public void StartWallJumpCoyoteTime()
    {
         wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    /// <summary>
    /// ֹͣ���㿿ǽ��Ծ������ʱ��
    /// </summary>
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

}
