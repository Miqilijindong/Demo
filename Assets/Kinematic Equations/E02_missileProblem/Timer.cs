using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// 预测时间
    /// </summary>
    public static float predictedTime;
    public Motor objectA;
    public Motor objectB;

    /// <summary>
    /// 0.02是默认时间间隔，后面编辑器修改成了0.001f
    /// </summary>
    public float timeStep = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        // 修改fixedupdate的时间间隔
        Time.fixedDeltaTime = timeStep;
        // 两点之间的距离
        float h = objectA.transform.position.x - objectB.transform.position.x;

        float a = objectB.acceleration - objectA.acceleration;
        float b = 2 * (objectB.initialVelocity - objectA.initialVelocity);
        float c = -2 * h;

        predictedTime = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        print(predictedTime);
    }
}
