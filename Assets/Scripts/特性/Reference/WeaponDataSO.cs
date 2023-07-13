using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// ���������ķ���ʱ������Ҫע��ؼ��ֲ�����������
/// ������ֱ����inspector���+���ʵ���Ļ����ǲ��еģ��������object�����޷�ʶ��
/// ����Ҫ�����Ӧ��AddData()�����ſ���
/// </summary>
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    /// <summary>
    /// ��������
    /// </summary>
    [field: SerializeField]
    [Tooltip("��������")]
    public int NumberOfAttacks { get; private set; }

    /// <summary>
    /// TODO: �������Ҫ����
    /// ��֪��ΪʲôDemo�þ������⣬�ý̵̳ľ�û����
    /// MovedFromAttribute����ļ��ƶ��ˣ��������е����л����ݴ��󣬿����������������֤û����
    /// </summary>
    [field: SerializeReference]
    public List<ComponentData> ComponentDatas { get; private set; }

    public T GetData<T>()
    {
        return ComponentDatas.OfType<T>().FirstOrDefault();
    }

    public void AddData(ComponentData data)
    {
        if (ComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) == null)
        {
            ComponentDatas.Add(data);
        }
    }

    //[ContextMenu("Add Sprite Data")]
    //private void AddSpriteData() => ComponentDatas.Add(new WeaponSpriteData());

    //[ContextMenu("Add Movement Data")]
    //private void AddMovementData() => ComponentDatas.Add(new MovementData());
}
