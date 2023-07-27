using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 武器生成组件类
    /// 启动后会自动生成武器所需的对应组件和删除组件
    /// </summary>
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;
        [SerializeField]
        private WeaponDataSO data;

        /// <summary>
        /// 保存所有已存在的武器组件
        /// </summary>
        private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
        /// <summary>
        /// 保存所有要添加到武器的武器组件
        /// </summary>
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        /// <summary>
        /// 武器组件依赖类---通常是指继承WeaponComponent的派生类
        /// </summary>
        private List<Type> componentDependencies = new List<Type>();

        private Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            GenerateWeapon(data);
        }

        /// <summary>
        /// 测试按钮
        /// [ContextMenu]是在Inspector的组件中，新增一个右键调试按钮，方便测试或者说可以用来新增菜单按钮这样子吧
        /// </summary>
        [ContextMenu("Text  Generate")]
        private void TestGenerationi()
        {
            GenerateWeapon(data);
        }

        /// <summary>
        /// 根据武器数据生成武器
        /// </summary>
        /// <param name="data"></param>
        private void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            componentAlreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentDependencies.Clear();

            componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            // 判断要添加武器组件
            foreach (var dependency in componentDependencies)
            {
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

                if (weaponComponent == null)
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                weaponComponent.Init();

                componentsAddedToWeapon.Add(weaponComponent);
            }

            // 删除多余组件
            var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }

            anim.runtimeAnimatorController = data.AnimatorController;
        }
    }
}


