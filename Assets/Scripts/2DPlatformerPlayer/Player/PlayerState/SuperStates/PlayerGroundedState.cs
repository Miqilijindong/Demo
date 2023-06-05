using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家地面
/// </summary>
public class PlayerGroundedState : PlayerState
{
    protected int inputX;
    protected int inputY;

    /// <summary>
    /// 判断是否触碰到天花板
    /// </summary>
    protected bool isTouchingCeiling;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private CollisionSenses CollisionSenses
    {
        //get => collisionSenses ??= core.GetComponent<CollisionSenses>();// 当collisionSenses为空时，则执行core.GetComponent<CollisionSenses>();并赋值给collisionSenses，这是一种比较简单的方法
        get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);
    }

    private Movement movement;
    private CollisionSenses collisionSenses;

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

        if (CollisionSenses)
        {
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            isTouchingCeiling = CollisionSenses.Ceiling;
        }
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
        inputY = player.inputHandler.NormInputY;
        jumpInput = player.inputHandler.JumpInput;
        grabInput = player.inputHandler.GrabInput;
        dashInput = player.inputHandler.DashInput;

        if (player.inputHandler.AttackInputs[(int)CombatInputs.primary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.AttackInputs[(int)CombatInputs.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (jumpInput && player.jumpState.CanJump() && !isTouchingCeiling)
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
        else if (dashInput && player.dashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
