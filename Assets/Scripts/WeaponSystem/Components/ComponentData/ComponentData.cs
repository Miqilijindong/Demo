using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 武器系统的组件类
    /// </summary>
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector]
        private string name;

        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }

        public void SetComponentName() => name = GetType().Name;

        /// <summary>
        /// 组件依赖
        /// </summary>
        protected abstract void SetComponentDependency();

        /// <summary>
        /// 设置攻击数据名称
        /// </summary>
        public virtual void SetAttackDataNames() { }

        /// <summary>
        /// 这个是根据攻击次数初始化攻击数据
        /// 作为虚函数，后续如果是继承了该类并重写了该方法，则每次按下"Set Number of Attacks"按钮后，就会初始化所有的AttackData
        /// </summary>
        /// <param name="numberOfAttacks"></param>
        public virtual void InitializeAttackData(int numberOfAttacks) { }
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        /// <summary>
        /// 重复数据
        /// 如果组件数据对于每次攻击都是相同的，则为True，避免了必须设置重复数据的问题
        /// </summary>
        [SerializeField]
        private bool repeatData;

        [SerializeField]
        private T[] attackData;

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for (int i = 0; i < attackData.Length; i++)
            {
                attackData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            var newLen = repeatData ? 1 : numberOfAttacks;

            var oldLen = attackData != null ? attackData.Length : 0;

            if (oldLen == newLen)
                return;

            // 重新分配了一块空间，然后将旧的内容拷过去---数组长度改成newLen的，可能会减少内容，也可能增加空间
            Array.Resize(ref attackData, newLen);

            if (oldLen < newLen)
            {
                for (int i = oldLen; i < attackData.Length; i++)
                {
                    // 使用与指定参数匹配程度最高的构造函数来创建指定类型的实例---可以理解开辟新空间，生成一个新的对象
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    attackData[i] = newObj;
                }
            }

            SetAttackDataNames();
        }
    }
}
