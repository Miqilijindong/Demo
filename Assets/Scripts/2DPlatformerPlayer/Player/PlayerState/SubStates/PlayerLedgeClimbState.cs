using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    /// <summary>
    ///  «∑Ò–¸π“
    /// </summary>
    private bool isHanging;
    /// <summary>
    ///  «∑Ò≈¿…œ«ΩΩ«
    /// </summary>
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int inputX;
    private int inputY;

    public PlayerLedgeClimbState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        player.anim.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = player.DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.faceingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (player.faceingDirection * playerData.stopOffset.x), cornerPos.y + playerData.stopOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimatinoFinshed)
        {
            if (isTouchingCeiling)
            {
                stateMachine.ChangeState(player.crouchIdleState);
            }
            else
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
        else
        {
            inputX = player.inputHandler.NormInputX;
            inputY = player.inputHandler.NormInputY;
            jumpInput = player.inputHandler.JumpInput;

            player.SetVelocityZero();
            player.transform.position = startPos;

            if (inputX == player.faceingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                player.anim.SetBool("climbLedge", true);
            }
            else if (inputY == -1 && isHanging && !isClimbing)
            {
                stateMachine.ChangeState(player.inAirState);
            }
            else if (jumpInput && !isClimbing)
            {
                player.wallJumpState.DetermineWallJumpDirection(true);
                stateMachine.ChangeState(player.wallJumpState);
            }
        }

    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    public void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * player.faceingDirection * 0.015f), Vector2.up, playerData.standColliderHeight, playerData.whatIsGround);

        player.anim.SetBool("isTouchingCeiling", isTouchingCeiling);
    }
}
