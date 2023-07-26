using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// 武器数据脚本编辑器
    /// [CustomEditor]自定义编辑器
    /// 后续如果需要添加Component按钮的话，新建一个AttackData的派生类和一个ComponentData的派生类，以及一个WeaponComponent的派生类
    /// AttackData的派生类是给每一个攻击提供对应的参数
    /// ComponentData的派生类则是提供对应组件信息
    /// WeaponComponent的派生类才是对行为做业务处理，比如武器Movement是调用人物的Movement组件进行攻击时移动
    /// </summary>
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        /// <summary>
        /// 保存所有继承ComponentData的组件
        /// </summary>
        public static List<Type> dataCompTypes;

        private WeaponDataSO dataSO;

        /// <summary>
        /// 折叠菜单AddComponent
        /// </summary>
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set Number of Attacks"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.InitializeAttackData(dataSO.NumberOfAttacks);
                }
            }

            // 折叠菜单
            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");
            if (showAddComponentButtons)
            {
                foreach (var dataCompType in dataCompTypes)
                {
                    // 这里根据组件名称生成一个按钮
                    if (GUILayout.Button(dataCompType.Name))
                    {
                        ComponentData comp = Activator.CreateInstance(dataCompType) as ComponentData;

                        if (comp == null)
                            return;

                        comp.InitializeAttackData(dataSO.NumberOfAttacks);

                        dataSO.AddData(comp);

                        // 这里是因为WeaponSoEditor在添加Components时，是不会保存数据到asset里，所以要通过设置为脏对象，来完成保存(但还是需要每次点Ctrl+s)
                        EditorUtility.SetDirty(dataSO);
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有ComponentData的派生类
        /// [DidReloadScripts]每当重新加载脚本时执行，必须使用static
        /// </summary>
        [DidReloadScripts]
        private static void OnRecompile()
        {
            // 获取已加载到应用程序域的执行上下文中的程序集
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // 但是这个反正只有在.NET Framework才能使用。.NET Standard中不行
            //System.Web.Compilation.BuildManager.GetReferencedAssemblies();

            // 然后把所有程序集里的类查询到一个可枚举化数组，并寻找关于ComponentData的派生类
            IEnumerable<Type> types = assemblies.SelectMany(assembly => assembly.GetTypes());
            IEnumerable<Type> filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData))
                && !type.ContainsGenericParameters
                && type.IsClass
            );

            dataCompTypes = filteredTypes.ToList();
        }

    }
}
