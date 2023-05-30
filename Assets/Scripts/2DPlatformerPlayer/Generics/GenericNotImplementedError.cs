using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用类，用来获取CoreComponent的实例，主要也是为了简化代码
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
