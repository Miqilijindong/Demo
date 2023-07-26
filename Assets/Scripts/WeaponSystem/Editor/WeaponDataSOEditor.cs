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
    /// �������ݽű��༭��
    /// [CustomEditor]�Զ���༭��
    /// ���������Ҫ���Component��ť�Ļ����½�һ��AttackData���������һ��ComponentData�������࣬�Լ�һ��WeaponComponent��������
    /// AttackData���������Ǹ�ÿһ�������ṩ��Ӧ�Ĳ���
    /// ComponentData�������������ṩ��Ӧ�����Ϣ
    /// WeaponComponent����������Ƕ���Ϊ��ҵ������������Movement�ǵ��������Movement������й���ʱ�ƶ�
    /// </summary>
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : Editor
    {
        /// <summary>
        /// �������м̳�ComponentData�����
        /// </summary>
        public static List<Type> dataCompTypes;

        private WeaponDataSO dataSO;

        /// <summary>
        /// �۵��˵�AddComponent
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

            // �۵��˵�
            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");
            if (showAddComponentButtons)
            {
                foreach (var dataCompType in dataCompTypes)
                {
                    // ������������������һ����ť
                    if (GUILayout.Button(dataCompType.Name))
                    {
                        ComponentData comp = Activator.CreateInstance(dataCompType) as ComponentData;

                        if (comp == null)
                            return;

                        comp.InitializeAttackData(dataSO.NumberOfAttacks);

                        dataSO.AddData(comp);

                        // ��������ΪWeaponSoEditor�����Componentsʱ���ǲ��ᱣ�����ݵ�asset�����Ҫͨ������Ϊ���������ɱ���(��������Ҫÿ�ε�Ctrl+s)
                        EditorUtility.SetDirty(dataSO);
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ����ComponentData��������
        /// [DidReloadScripts]ÿ�����¼��ؽű�ʱִ�У�����ʹ��static
        /// </summary>
        [DidReloadScripts]
        private static void OnRecompile()
        {
            // ��ȡ�Ѽ��ص�Ӧ�ó������ִ���������еĳ���
            System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // �����������ֻ����.NET Framework����ʹ�á�.NET Standard�в���
            //System.Web.Compilation.BuildManager.GetReferencedAssemblies();

            // Ȼ������г���������ѯ��һ����ö�ٻ����飬��Ѱ�ҹ���ComponentData��������
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
