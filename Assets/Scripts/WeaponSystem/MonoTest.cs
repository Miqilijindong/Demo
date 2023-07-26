using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    public class MonoTest : MonoBehaviour
    {
        #region 测试自定义编辑器用

        public enum EnumValue
        {
            EnumValue1,
            EnumValue2,
            EnumValue3,
        }

        public int intValue;
        public bool boolValue;
        public EnumValue enumValue;

        #endregion
    }
}
