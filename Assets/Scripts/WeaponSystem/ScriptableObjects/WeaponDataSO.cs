using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon System/Basic Weapon Data", order = 0)]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int NumberOfAttacks { get; private set; }
        /// <summary>
        /// 当前data文件中所需要的component组件数据
        /// 后期的话，更像是这个武器所对应可用操作。每一个组件对应一个操作，比如有Movement的话，就可以边攻击边移动。可以理解成武器的状态机
        /// 这里用的是serializereference，一定要记住
        /// </summary>
        [field: SerializeReference]
        public List<ComponentData> ComponentData { get; private set; }

        public T GetData<T>()
        {
            // List.OfType<T>();数组中不同的实体，可以通过OfType<T>()取出T类型的对象
            return ComponentData.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// 判断是否填过组件，如果没有则添加，反之则啥也不做
        /// </summary>
        /// <param name="comp">所需组件</param>
        public void AddData(ComponentData comp)
        {
            if (ComponentData.FirstOrDefault(t => t.GetType() == comp.GetType()) != null)
                return;

            ComponentData.Add(comp);
        }
    }
}
