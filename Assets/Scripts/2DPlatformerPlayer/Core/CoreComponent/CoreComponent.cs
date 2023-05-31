using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����������
/// ���к������������Ҫ�̳������
/// </summary>
public class CoreComponent : MonoBehaviour, ILogicUpdate
{
    protected Core core;

    protected virtual void Awake()
    {
        core = transform.parent.GetComponent<Core>();

        if (core == null)
        {
            Debug.LogError("There is not Core on the parent");
        }
        core.AddComponent(this);
    }

    public virtual void LogicUpdate() { }
}
