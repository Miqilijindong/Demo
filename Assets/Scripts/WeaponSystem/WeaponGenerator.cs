using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// �������������
    /// ��������Զ�������������Ķ�Ӧ�����ɾ�����
    /// </summary>
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField]
        private Weapon weapon;
        [SerializeField]
        private WeaponDataSO data;

        /// <summary>
        /// ���������Ѵ��ڵ��������
        /// </summary>
        private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
        /// <summary>
        /// ��������Ҫ��ӵ��������������
        /// </summary>
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        /// <summary>
        /// �������������---ͨ����ָ�̳�WeaponComponent��������
        /// </summary>
        private List<Type> componentDependencies = new List<Type>();

        private Animator anim;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            GenerateWeapon(data);
        }

        /// <summary>
        /// ���԰�ť
        /// [ContextMenu]����Inspector������У�����һ���Ҽ����԰�ť��������Ի���˵�������������˵���ť�����Ӱ�
        /// </summary>
        [ContextMenu("Text  Generate")]
        private void TestGenerationi()
        {
            GenerateWeapon(data);
        }

        /// <summary>
        /// ��������������������
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

            // �ж�Ҫ����������
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

            // ɾ���������
            var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach (var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }

            anim.runtimeAnimatorController = data.AnimatorController;
        }
    }
}


