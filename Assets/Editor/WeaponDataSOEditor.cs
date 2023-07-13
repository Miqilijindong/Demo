using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������ݽű��༭��
/// </summary>
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    /// <summary>
    /// ���м̳���ComponentData����
    /// </summary>
    private static List<Type> dataCompTypes = new List<Type>()
    /*{
        typeof(MovementData),
        typeof(WeaponSpriteData)
    }*/;

    private WeaponDataSO dataSO;

    private void OnEnable()
    {
        dataSO = target as WeaponDataSO;
    }

    public override void OnInspectorGUI()
    {
        //GUILayout.Button("This is a button!");// ����������ö���Inspector
        base.OnInspectorGUI();

        // ����ť�����ʱ������true�����Կ���ͨ��if�ж����������AddListener()
        /*if (GUILayout.Button("This is a button!"))
        {
            Debug.Log("Pressed!");
        }*/

        foreach (var dataCompType in dataCompTypes)
        {
            if (GUILayout.Button(dataCompType.Name))
            {
                var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                if (comp == null)
                    continue;
                dataSO.AddData(comp);
            }
        }
    }

    /// <summary>
    /// ÿ���ű����¼��غ��Զ�ִ��һ��
    /// </summary>
    [DidReloadScripts]
    private static void OnRecompile()
    {
        // ��ȡ��ǰ���еĳ���
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        // ��ȡ���������Ϣ
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());

        // �ᴿ����ȡ����ComponentData�����ಢ�Ҳ������κη��Ͳ���
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
        );

        dataCompTypes = filteredTypes.ToList();
    }
}
