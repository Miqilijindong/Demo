using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;

public class BezierPathController : MonoBehaviour
{
    public static BezierPathController _instance;
    public ObjectPool<GameObject> objectPool;
    public GameObject startObject;
    public GameObject targetBallPrefab;//目标球预制体，后续要改成数组，因为要放多个。
    /// <summary>
    /// 虚线点的数量
    /// </summary>
    [Header("虚线点的数量")]
    public int segmentPerCurve;
    //public List<GameObject> ControllerPointList = new List<GameObject>();//存放控制点的列表，控制点类型为GO
    public List<GameObject> TargetBallPointList = new List<GameObject>();//目标球生成位置的列表。

    public bool isShowDrawing = true;

    private void Awake()
    {
        objectPool = new ObjectPool<GameObject>(InstanceObject, segmentPerCurve / 2);
    }

    List<Vector3> pointPos = new List<Vector3>();
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            pointPos.Clear();

            Vector3 a = Input.mousePosition;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(a);
            pointPos.Add(startObject.transform.position);
            mousePos.z = pointPos[0].z;
            pointPos.Add(mousePos);
            var pointsOnCurve = GetDrawPoint(pointPos, segmentPerCurve);

            for (int i = 0; i < TargetBallPointList.Count; i++)
            {
                TargetBallPointList[i].transform.localPosition = pointsOnCurve[i];
            }

            int count = TargetBallPointList.Count;
            for (int k = count; k < segmentPerCurve; k++)
            {
                GameObject go = objectPool.GetObject();
                go.SetActive(true);
                go.transform.localPosition = pointsOnCurve[k];

                TargetBallPointList.Add(go);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //for (int i = 0; i < TargetBallPointList.Count; i++)// 这里如果用这个for的话 ，会导致下标错误
            for (int i = TargetBallPointList.Count - 1; i >= 0; i--)
            {
                TargetBallPointList[i].SetActive(false);
                objectPool.AddObject(TargetBallPointList[i]);
                TargetBallPointList.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 实例化球的预制体，并添加脚本
    /// </summary>
    /// <returns></returns>
    private GameObject InstanceObject()
    {
        GameObject ball = Instantiate(targetBallPrefab, transform);
        ball.SetActive(false);
        return ball;
    }

    //获取曲线绘制点并调用公式进行计算
    //传递的两个参数分别为控制点的列表和每个曲线分成多少段
    //多少段用来控制参数t的改变
    public List<Vector3> GetDrawPoint(List<Vector3> controlPoint, int segmentPerCurve)
    {
        List<Vector3> pointOnCurve = new List<Vector3>();//曲线上的点的列表
        //从列表中取出用于计算的4个点。
        //注意条件语句和累加值，一是不要越界，二是要让上一个组合中的最后一个点成为下一个组合的第一个点，以保证曲线的连续。
        for (int i = 0; i < controlPoint.Count - 1; i += 1)
        {
            //var类型是根据传递的参数类型来决定的。
            var p0 = controlPoint[i];
            var p1 = controlPoint[i + 1];
            //var p2 = controlPoint[i + 2];
            //var p3 = controlPoint[i + 3];

            for (int j = 0; j < segmentPerCurve; j++)
            {
                //float t = j / (float)segmentPerCurve;//控制t的取值，范围为[0,1]
                //pointOnCurve.Add(BezierCubicFormula(t, p0, p1, p2, p3));
                pointOnCurve.Add(Bezier(j, p0, p1));
            }
        }
        return pointOnCurve;
    }

    /// <summary>
    /// 贝塞尔三次曲线计算公式
    /// </summary>
    /// <param name="t"></param>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <returns></returns>
    public Vector3 BezierCubicFormula(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return p0 * Mathf.Pow((1 - t), 3) + 3 * p1 * t * Mathf.Pow((1 - t), 2) + 3 * p2 *
            Mathf.Pow(t, 2) * (1 - t) + p3 * Mathf.Pow(t, 3);
    }

    /// <summary>
    /// 计算角度并算出对应下标的位置
    /// </summary>
    /// <param name="t">下标</param>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <returns></returns>
    public Vector3 Bezier(float t, Vector3 p0, Vector3 p1)
    {
        return t * (p1 - p0).normalized / 2.5f;
    }
}
