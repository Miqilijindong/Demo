using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// 想用这个类的方法时，首先要注意关键字不能重命名。
/// 其次如果直接用inspector里的+添加实例的话，是不行的，他会添加object导致无法识别
/// 必须要加入对应的AddData()函数才可以
/// </summary>
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    /// <summary>
    /// 攻击次数
    /// </summary>
    [field: SerializeField]
    [Tooltip("攻击次数")]
    public int NumberOfAttacks { get; private set; }

    /// <summary>
    /// TODO: 这个特性要看看
    /// 不知道为什么Demo用就有问题，用教程的就没问题
    /// MovedFromAttribute如果文件移动了，导致已有的序列化数据错误，可以用这个特性来保证没问题
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
