using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGround;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected int inputX;
    protected int inputY;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses
    {
        //get => collisionSenses ??= core.GetComponent<CollisionSenses>();// 当collisionSenses为空时，则执行core.GetComponent<CollisionSenses>();并赋值给collisionSenses，这是一种比较简单的方法
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private Movement movement;
    private CollisionSenses collisionSenses;


    public PlayerTouchingWallState(PlatformerPlayer.Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        isGround = CollisionSenses.Ground;
        isTouchingWall = CollisionSenses.WallFront;
        isTouchingLedge = CollisionSenses.LedgeHorizontal;

        if (isTouchingWall && !isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPosition(player.transform.position);
        }
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
        else if (!isTouchingWall || (inputX != Movement.facingDirection && !grabInput))
        {
            stateMachine.ChangeState(player.inAirState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
