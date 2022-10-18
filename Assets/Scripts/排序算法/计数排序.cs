using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 计数排序是一种非基于比较的排序算法，其核心在于将输入的数据转化为键存储在额外开辟的数组空间中已达到排序的效果
/// 时间复杂度遍历了三次原始数组，一次技术素组，所以算法时间复杂度是O(N+M)
/// 空间复杂度则是过程中新建了一个计数数组和一个输出数组，所以是O(N+M)
/// 稳定性优化后的排序算法在排序过程中，相同的元素前后不会发生改变，是一种稳定的算法。
/// 局限性---当最大值和最小值相差特别大时，不适合使用计数排序算法。当排序序列的值不是整数的时候，不适合计数排序算法
/// </summary>
public class 计数排序 : MonoBehaviour
{
    int[] sort(int[] arr)
    {
        // 得到数列的最大值，和最小值，
        int max = arr[0];
        foreach (int item in arr)
        {
            if (item > max)
            {
                max = item;
            }
        }

        // 初始化数组
        int[] countArr = new int[arr.Length];

        // 遍历原始数组，填充计数数组
        foreach (int i in arr)
        {
            countArr[i]++;
        }

        // 遍历计数数组，得到排序后结果
        int index = 0;
        int[] sortArr = new int[arr.Length];
        for (int i = 0; i <= max; i++)
        {
            for (int j = 0; j < countArr[i]; j++)
            {
                sortArr[index++] = i;
            }
        }

        return sortArr;
    }

    private void Start()
    {
        int[] arr = { 12,1, 7, 4, 9, 10, 5, 2, 4, 7, 3, 4 };
        int[] ints = sortNew(arr);

        StringBuilder stringBuilder = new StringBuilder();
        for (int x = 0; x < ints.Length; x++)
        {
            stringBuilder.Append(ints[x]);
            if(x!=ints.Length - 1)
            {
                stringBuilder.Append(",");
            }
        }
        Debug.Log(stringBuilder.ToString());
    }

    /// <summary>
    /// 计数排序优化
    /// </summary>
    /// <param name="arr"></param>
    int[] sortNew(int[] arr)
    {
        // 得到数列的最大值、最小值，并且计算计数数组长度
        int max = arr[0], min = arr[0];
        foreach (int item in arr)
        {
            if (item > max)
            {
                max = item;
            }
            if (item < min)
            {
                min = item;
            }
        }
        int length = max - min + 1;

        //初始化数组
        int[] countArr = new int[length];

        // 遍历原始数组，填充计数数组
        foreach (int item in arr)
        {
            countArr[item - min]++;
        }

        //计数数组形变后，后面的元素等于全面的元素纸盒
        int sum = 0;
        for (int i = 0; i < length; i++)
        {
            sum += countArr[i];
            countArr[i] = sum;
        }

        //倒序遍历计数数组，从计数数组中找到正确位置。
        int[] sortArr = new int[length];
        for(int i = arr.Length - 1; i >= 0; i--)
        {
            int v = arr[i] - min;
            int v1 = countArr[v] - 1;
            sortArr[v1] = arr[i];
            countArr[v]--;
            //Debug.LogFormat(arr[i], countArr, sortArr);
        }

        return sortArr;

    }
}
