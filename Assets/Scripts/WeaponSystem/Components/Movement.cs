using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 武器系统中的移动
    /// 更多是负责攻击时顺带移动
    /// </summary>
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        /// <summary>
        /// 这里用的是全局Movement类，是CoreComponent的派生类
        /// 主要是为了调用移动函数。
        /// global是取全局命名空间的
        /// </summary>
        private global::Movement coreMovement;

        private float velocity;
        private Vector2 direction;

        protected Core Core => weapon.Core;

        protected override void Start()
        {
            base.Start();

            coreMovement = Core.GetCoreComponent<global::Movement>();

            AnimationEventHandler.OnStartMovement += HandleStartMovement;
            AnimationEventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            velocity = 0f;
            direction = Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (!isAttackActive)
                return;

            SetVelocityX();
        }

        private void HandleStopMovement()
        {
            velocity = 0f;
            direction = Vector2.zero;

            SetVelocity();
        }

        private void HandleStartMovement()
        {
            velocity = currentAttackData.Velocity;
        }

        private void SetVelocity()
        {
            coreMovement.SetVelocity(velocity, direction, coreMovement.facingDirection);
        }

        private void SetVelocityX()
        {
            coreMovement.SetVelocityX((direction * velocity).x * coreMovement.facingDirection);
        }
    }
}
