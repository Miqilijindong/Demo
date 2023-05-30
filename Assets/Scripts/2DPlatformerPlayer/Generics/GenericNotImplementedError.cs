using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͨ���࣬������ȡCoreComponent��ʵ������ҪҲ��Ϊ�˼򻯴���
/// </summary>
/// <typeparam name="T"></typeparam>
public static class GenericNotImplementedError<T> /*where T : CoreComponent*/
{
    public static T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value; ;
        }

        Debug.LogError(typeof(T) + " not implemented on " + name);
        return default;
    }
}
