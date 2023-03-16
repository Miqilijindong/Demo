using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    /// <summary>
    /// 初始速度
    /// </summary>
    public float initialVelocity;
    /// <summary>
    /// 加速度
    /// </summary>
    public float acceleration;
    float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = initialVelocity;
    }

    private void FixedUpdate()
    {
        if (Time.fixedTime < Timer.predictedTime)
        {
            currentVelocity += acceleration * Time.fixedDeltaTime;
            transform.Translate(Vector3.right * currentVelocity * Time.fixedDeltaTime);
        }
    }
}
