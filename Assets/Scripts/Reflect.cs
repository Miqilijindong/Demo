using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Reflect :MonoBehaviour
    {
        private void Start()
        {

            Type type = typeof(Test);
            Debug.Log(type.FullName);
            Debug.Log(type.Name);
            Debug.Log(type.Namespace);
            Debug.Log(type.Assembly);
            System.Reflection.FieldInfo[] fieldInfos = type.GetFields();
            foreach (var item in fieldInfos)
            {
                Debug.Log(item.Name);
            }
        }
    }
}
