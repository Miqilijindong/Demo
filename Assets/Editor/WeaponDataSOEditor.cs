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
/// 武器数据脚本编辑器
/// </summary>
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    /// <summary>
    /// 所有继承了ComponentData的类
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
        //GUILayout.Button("This is a button!");// 放在这里会置顶在Inspector
        base.OnInspectorGUI();

        // 当按钮被点击时，会变成true，所以可以通过if判断来添加类似AddListener()
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
    /// 每当脚本重新加载后，自动执行一遍
    /// </summary>
    [DidReloadScripts]
    private static void OnRecompile()
    {
        // 获取当前运行的程序集
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        // 提取所有类的信息
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());

        // 提纯，获取所有ComponentData的子类并且不包含任何泛型参数
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
        );

        dataCompTypes = filteredTypes.ToList();
    }
}
