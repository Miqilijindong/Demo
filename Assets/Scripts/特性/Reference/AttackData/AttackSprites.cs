using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackSprites : AttackData
{
    [field: SerializeField]
    public Sprite[] sprites { get; private set; }
}
