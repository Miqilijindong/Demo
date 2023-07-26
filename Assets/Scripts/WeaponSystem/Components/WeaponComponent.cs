using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        protected AnimationEventHandler AnimationEventHandler => weapon.EventHandler;

        protected bool isAttackActive;

        public virtual void Init() { }

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
        }

        protected virtual void Start()
        {
            weapon.OnEnter += HandleEnter;
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestroy()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    /// <summary>
    /// 抽象类
    /// 通过集成ComponentData和AttackData两个类的数据，来完成一些特殊组件操作
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        public override void Init()
        {
            base.Init();

            data = weapon.Data.GetData<T1>();
        }
    }
}
