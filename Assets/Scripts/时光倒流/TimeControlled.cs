using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 时光倒流的物体，如果要会时光倒流的话，一定要继承这个类
/// </summary>
public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity;
    /// <summary>
    /// 当前动画
    /// </summary>
    public AnimationClip currentAnimation;
    /// <summary>
    /// 动画时间
    /// </summary>
    public float animationTime;

    public virtual void TimeUpdate()
    {
        if (currentAnimation != null)
        {
            animationTime += Time.deltaTime;
            // 当动画时间超过当前动画的长度
            if (animationTime > currentAnimation.length)
            {
                animationTime -= currentAnimation.length;
            }
        }
    }

    public void UpdateAnimaiton()
    {
        if (currentAnimation != null)
        {
            currentAnimation.SampleAnimation(gameObject, animationTime);
        }
    }
}
