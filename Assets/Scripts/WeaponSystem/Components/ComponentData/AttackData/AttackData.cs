using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 攻击数据的基类，主要设置对应的攻击名称
    /// </summary>
    public class AttackData
    {
        [SerializeField, HideInInspector]
        private string name;

        /// <summary>
        /// 根据攻击次数设置对应攻击数据名称
        /// </summary>
        /// <param name="i"></param>
        public void SetAttackName(int i) => name = $"Attack {i}";
    }
}
