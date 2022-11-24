using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 时光倒流的物体，如果要会时光倒流的话，一定要继承这个类
/// </summary>
public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity;

    public virtual void TimeUpdate()
    {

    }
}
