using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int inputX;

    private bool jumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;

    public PlayerGroundedState(PlatformerPlayer.Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        this.player = player;
        this.stateMachine = playerStateMachine;

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetAmountOfJumpsLeft();
        player.dashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inputX = player.inputHandler.NormInputX;
        jumpInput = player.inputHandler.JumpInput;
        grabInput = player.inputHandler.GrabInput;
        dashInput = player.inputHandler.DashInput;

        if (jumpInput && player.jumpState.CanJump())
        {
            //player.inputHandler.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
        else if (dashInput && player.dashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
