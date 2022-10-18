using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 归并排序的概念
///     是建立在归并操作上的一种有效的排序算法，归并排序对序列的元素进行逐层折半分组，然后从最小分组开始比较排序，合并称一个大的分组，逐层进行，最终所有的元素都是有序地
/// 时间复杂度是O(nlogn)
/// 空间复杂度是O(n)
/// 稳定性因为相同元素的前后顺序没有改变，所以归并排序是一种稳定排序算法
/// </summary>
public class 归并排序 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 4, 5, 3, 2, 1 };

        sort(array, 0, array.Length - 1);
    }

    void sort(int[] arr, int startIndex, int endIndex)
    {
        if (startIndex >= endIndex)
        {
            return;
        }

        // 折半递归
        int midIndex = (startIndex + endIndex) / 2;
        sort(arr, startIndex, midIndex);
        sort(arr, midIndex + 1, endIndex);

        // 将两个有序地小数组，合并成一个大数组
        merge(arr, startIndex, midIndex, endIndex);


    }

    void merge(int[] arr, int startIndex, int midIndex, int endIndex)
    {
        int[] tempArr = new int[endIndex - startIndex + 1];
        int p1 = startIndex, p2 = midIndex + 1, p = 0;

        // 比较两个有序小数组的元素，一次放入大数组中
        while (p1 <= midIndex && p2 <= endIndex)
        {
            if (arr[p1] <= arr[p2])
            {
                tempArr[p++] = arr[p1++];
            }
            else
            {
                tempArr[p++] = arr[p2++];
            }
        }

        while (p1 <= midIndex)
        {
            tempArr[p++] = arr[p1++];
        }
        while (p2 <= endIndex)
        {
            tempArr[p++] = arr[p2++];
        }

        for (int i = 0; i < tempArr.Length; i++)
        {
            arr[i + startIndex] = tempArr[i];
        }
    }
}
