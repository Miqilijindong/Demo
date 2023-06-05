using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool perfromLongRangeAction;
    /// <summary>
    /// 判断近距离是否存在玩家
    /// </summary>
    protected bool perfromCloseRangeAction;
    protected bool isDetectingLedge;

    private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private CollisionSenses collisionSenses;
    private Movement movement;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;

        perfromCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        perfromLongRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(0f);

        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            perfromLongRangeAction = true;
        }
    }

    public override void PhysicsUPdate()
    {
        base.PhysicsUPdate();
    }
}
