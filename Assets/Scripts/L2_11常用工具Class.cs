using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_11常用工具Class : MonoBehaviour
{
    
    void Start()
    {
        #region 数字工具类
        //int abs = Mathf.Abs(-1);
        //print("绝对值:" + abs);
        //print("最大值:" + Mathf.Max(1, 2, 3, 4, 5, 6, 7, 8, 11));
        //print("最小值:" + Mathf.Min(1, 2, 3, 4, 5, 6, 7, 8, 00));
        //print("四舍五入:" + Mathf.Round(5.5f));
        //print("向上取整:" + Mathf.Ceil(5.1f));
        //print("向下取整:" + Mathf.Floor(5.1f));
        //print("取随机数:" + Random.Range(0, 5));// 如果是int入参，则返回0~5的随机值，包含0，不包含5.
        //print("取随机数:" + Random.Range(0, 5f));// 如果是float入参，则返回0~5的随机值，包含0并且包含5.
        #endregion

        // 时间缩放，默认值为1，若设置<1，表示时间减慢，若设置>1,表示时间加快，0意味着游戏暂停
        Time.timeScale = 0;
    }

    private void Update()
    {
        print("游戏时间" + Time.time);
        print("上一帧到当前帧的游戏时间:" + Time.deltaTime);
        print("游戏开始的总时间，即使暂停也会增加，也就是现实时间" + Time.realtimeSinceStartup);

        transform.Translate(new Vector3(0, 0.1f, 0) * 1 * Time.deltaTime);
    }
}
