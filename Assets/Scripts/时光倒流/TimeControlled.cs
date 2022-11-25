using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʱ�⵹�������壬���Ҫ��ʱ�⵹���Ļ���һ��Ҫ�̳������
/// </summary>
public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity;
    /// <summary>
    /// ��ǰ����
    /// </summary>
    public AnimationClip currentAnimation;
    /// <summary>
    /// ����ʱ��
    /// </summary>
    public float animationTime;

    public virtual void TimeUpdate()
    {
        if (currentAnimation != null)
        {
            animationTime += Time.deltaTime;
            // ������ʱ�䳬����ǰ�����ĳ���
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
