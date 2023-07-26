using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// [CustomEditor]第一个参数描述需要自定义编辑器的类， 第二参数表示是否对其子类起效
    /// </summary>
    [CustomEditor(typeof(MonoTest), true)]
    public class MonoTestEditor : Editor
    {
        private MonoTest m_Target;

        private SerializedProperty m_IntValue;
        private SerializedProperty m_BoolValue;
        private SerializedProperty m_EnumValue;

        private void OnEnable()
        {
            m_Target = target as MonoTest;

            m_IntValue = serializedObject.FindProperty("intValue");
            m_BoolValue = serializedObject.FindProperty("boolValue");
            m_EnumValue = serializedObject.FindProperty("enumValue");
        }

        public override void OnInspectorGUI()
        {
            // 如果不改原本的显示模样的话，就需要调用父类的方法
            //base.OnInspectorGUI();

            #region 使用基本绘制

            EditorGUILayout.PropertyField(m_IntValue, new GUIContent("这是一个整型值的提示"));
            EditorGUILayout.PropertyField(m_BoolValue);
            EditorGUILayout.PropertyField(m_EnumValue);

            #endregion

            #region 使用特殊形式

            GUIContent content = new GUIContent { text = "整型值", tooltip = "这是一个整形的提示" };
            EditorGUILayout.IntSlider(m_IntValue, 0, 100, content);// 使用一个滑动条来代替基本的绘制

            bool boolValue = m_BoolValue.boolValue;
            m_BoolValue.boolValue = EditorGUILayout.ToggleLeft("布尔值", m_BoolValue.boolValue);
            if (m_BoolValue.boolValue != boolValue)
            {
                Debug.LogError("value changed!" + m_BoolValue.boolValue);// 在值变化后进行一些操作
            }

            m_EnumValue.intValue = (int)(MonoTest.EnumValue)EditorGUILayout.EnumPopup("enumValue", (MonoTest.EnumValue)m_EnumValue.intValue);// 使用自定义的枚举值

            if (serializedObject.ApplyModifiedProperties())
            {
                Debug.LogError("value changed!" + m_BoolValue.boolValue);// 在值变化后进行一些操作
            }

            // 禁止某些字段的在运行时修改
            if (Application.isPlaying)
            {
                GUI.enabled = false;
                m_BoolValue.boolValue = EditorGUILayout.Toggle("布尔值", m_BoolValue.boolValue);
                GUI.enabled = true;
            }
            else
            {
                m_BoolValue.boolValue = EditorGUILayout.Toggle("布尔值", m_BoolValue.boolValue);
            }

            #endregion
        }
    }
}
