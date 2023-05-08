using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeachingWallState : PlayerState
{
    protected bool isGround;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected int inputX;
    protected int inputY;


    public PlayerTeachingWallState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        isGround = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inputX = player.inputHandler.NormInputX;
        inputY = player.inputHandler.NormInputY;
        grabInput = player.inputHandler.GrabInput;
        jumpInput = player.inputHandler.JumpInput;

        if (jumpInput)
        {
            //player.inputHandler.UseJumpInput();
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (isGround && !grabInput)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (!isTouchingWall || (inputX != player.faceingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.inAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
