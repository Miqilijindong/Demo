using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ComponentData
{

}

[Serializable]
public class ComponentData<T> : ComponentData where T : AttackData
{
    //[Tooltip($" {typeof(T)}")]
    [field: SerializeField]
    public T[] AttackData { get; private set; }
}