using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPlayer
{
    /// <summary>
    /// ������Ϊ�Ѿ�����һ��IdleState�ˣ�����ֻ����������������ռ䣬��Ȼ�ᱨ��
    /// </summary>
    public class IdleState : State
    {
        protected D_IdleState stateData;

        protected bool flipAfterIdle;
        protected bool isIdleTimeOver;
        /// <summary>
        /// ��������С����
        /// </summary>
        protected bool isPlayerInMinAgroRange;

        protected float idleTime;

        private CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
        private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

        private CollisionSenses collisionSenses;
        private Movement movement;

        public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
        {
            this.stateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            isIdleTimeOver = false;
            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (flipAfterIdle)
            {
                Movement.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Movement?.SetVelocityX(0f);

            if (Time.time >= startTime + idleTime)
            {
                isIdleTimeOver = true;
            }
        }

        public override void PhysicsUPdate()
        {
            base.PhysicsUPdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        }

        /// <summary>
        /// ����Idle��ת��
        /// </summary>
        /// <param name="flip"></param>
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
