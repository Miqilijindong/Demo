using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意命名规范，名称不能去关键字相同，不然会导致引用错误
/// 之前这个类就写成SerializeReference，导致[SerializeReference]引用错误
/// TODO : field: SerializeField和SerializeField是有区别的，在inspector里显示的话，必须要带上field的，不然就无法正确的显示，但是不知道为什么
/// </summary>
[CreateAssetMenu(fileName = "SerializeReferenceTest", menuName = "Data/特性/SerializeReferenceTest")]
public class SerializeReferenceTest : ScriptableObject
{
    [field: SerializeReference]
    public List<FsmStateAction> actions { get; private set; }
}

[Serializable]
public class FsmStateAction
{
}

[Serializable]
public class FsmStateAction<T> : FsmStateAction where T : StateData
{
    [field: SerializeField]
    public T[] stateData { get; private set; }
}

public class StateData
{

}

[Serializable]
public class WaitActionData : StateData
{
    [field: SerializeField]
    public Sprite[] values { get; private set; }
}

[Serializable]
public class PlayerActionData : StateData
{
    [field: SerializeField]
    public Vector2[] values { get; private set; }
}