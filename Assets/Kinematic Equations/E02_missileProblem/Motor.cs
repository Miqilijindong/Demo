using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    /// <summary>
    /// ��ʼ�ٶ�
    /// </summary>
    public float initialVelocity;
    /// <summary>
    /// ���ٶ�
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
