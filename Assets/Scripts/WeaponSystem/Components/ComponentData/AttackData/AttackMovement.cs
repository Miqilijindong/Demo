using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    [Serializable]
    public class AttackMovement : AttackData
    {
        [field: SerializeField]
        public Vector2 Direction { get; private set; }
        [field: SerializeField]
        public float Velocity { get; private set; }
    }
}
