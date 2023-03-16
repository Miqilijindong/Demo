using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// https://www.youtube.com/watch?v=IvT8hjy6q4o
/// </summary>
public class BallLauncher : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;

    /// <summary>
    /// �߶�
    /// </summary>
    [Header("�߶ȣ���Ҫ����target.y - ball.y")]
    [Tooltip("ע��:�����ƶ���inspector��Ӧ��������ԣ�����ʾ��ʾ��")]
    public float h = 25;
    public float gravity = -18;

    // Start is called before the first frame update
    void Start()
    {
        // ����֮ǰ��������
        ball.useGravity = false;

        string a = null;
        print(a ?? "123");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

#if UNITY_EDITOR
        DrawPath();
#endif
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateLaunchData().initialVelocity;
        //print(CalculateLaunchVelocity());
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            // �����ֻ����game��ͼ�￴������
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        // Ŀ���뷢����֮��ľ��� 
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        // y�� = ���� * �߶� * -2
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        // Mathf.Sign(Val); ���ValΪ�㣬�򷵻�ֵΪ0�������ֵС���㣬��Ϊ-1�������ֵ�����㣬��Ϊ1��
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
