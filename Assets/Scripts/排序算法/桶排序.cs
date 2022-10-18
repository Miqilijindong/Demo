using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 桶排序是基数排序算法的升级版，将数据分到有限数量的桶子里，然后每个桶再分别排序
/// 时间复杂度，O(N+M+N(logN-logM))
/// 空间赋值度O(N+M)
/// 稳定性则是稳定的排序算法
/// </summary>
public class 桶排序 : MonoBehaviour
{
    private void Start()
    {

        int[] arr = { 11, 38, 8, 34, 27, 19, 26, 13 };
        bucketsort(arr);
    }

    public static void bucketsort(int[] arr)
    {
        ArrayList[] bucket = new ArrayList[5];// 声明五个桶
        for (int i = 0; i < bucket.Length; i++)
        {
            bucket[i] = new ArrayList();// 确定桶的格式为ArrayList
        }
        for (int i = 0; i < arr.Length; i++)
        {
            int index = arr[i] / 10;// 确定元素存放的桶号
            bucket[index].Add(arr[i]);// 将元素存入对应的桶中
        }
        for (int i = 0; i < bucket.Length; i++)
        {// 遍历每一个桶
            bucket[i].Sort(null);// 对每一个桶排序
            for (int j = 0; j < bucket[i].Count; j++)
            {// 遍历桶中的元素并输出
                Debug.Log(bucket[i][j]);
            }
        }
    }
}
