using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 武器类
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        public WeaponDataSO Data { get; private set; }

        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnUseInput;

        /// <summary>
        /// 攻击次数重置冷却时间
        /// </summary>
        [Tooltip("攻击次数重置冷却时间")]
        [SerializeField]
        private float attackCounterRestCooldown;

        private Animator anim;
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        public AnimationEventHandler EventHandler
        {
            get
            {
                if (!initDone)
                {
                    GetDependencies();
                }
                return eventHandler;
            }
            set => eventHandler = value;
        }
        public Core Core { get; private set; }

        /// <summary>
        /// 判断是完成过
        /// </summary>
        private bool initDone;
        private AnimationEventHandler eventHandler;

        public void SetData(WeaponDataSO data)
        {
            Data = data;
        }

        /// <summary>
        /// 获取依赖数据
        /// </summary>
        private void GetDependencies()
        {
            if (initDone)
                return;

            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            anim = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();

            initDone = true;
        }
    }
}
