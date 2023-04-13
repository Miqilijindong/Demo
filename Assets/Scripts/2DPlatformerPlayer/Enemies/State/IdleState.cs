using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPlayer
{
    /// <summary>
    /// 这里因为已经存在一个IdleState了，所以只能让这个加上命名空间，不然会报错
    /// </summary>
    public class IdleState : State
    {
        protected D_IdleState stateData;

        protected bool flipAfterIdle;
        protected bool isIdleTimeOver;
        protected bool isPlayerInMinAgroRange;

        protected float idleTime;

        public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            entity.SetVelocity(0f);
            isIdleTimeOver = false;
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (flipAfterIdle)
            {
                entity.Flip(); 
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= startTime + idleTime)
            {
                isIdleTimeOver = true;
            }
        }

        public override void PhysicsUPdate()
        {
            base.PhysicsUPdate();
            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange(); 
        }

        public void SetFlipAfterIdle(bool flip)
        {
            flipAfterIdle = flip;
        }

        private void SetRandomIdleTime()
        {
            idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime); 
        }
    }
}
