using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ע�������淶�����Ʋ���ȥ�ؼ�����ͬ����Ȼ�ᵼ�����ô���
/// ֮ǰ������д��SerializeReference������[SerializeReference]���ô���
/// TODO : field: SerializeField��SerializeField��������ģ���inspector����ʾ�Ļ�������Ҫ����field�ģ���Ȼ���޷���ȷ����ʾ�����ǲ�֪��Ϊʲô
/// </summary>
[CreateAssetMenu(fileName = "SerializeReferenceTest", menuName = "Data/����/SerializeReferenceTest")]
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