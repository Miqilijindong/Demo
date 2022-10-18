using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 快速排序的概念
///     快速排序是从冒泡排序演变过来的，实际上是在冒泡排序基础上的递归分治法。快速排序在每一轮挑选一个基准元素，并让其他比它大的元素移动到数列一边，
///     比它小的元素移动到数列的另一边，从而把数列拆解成了两个部分。
/// 平均时间复杂度是O(nlogn)，极端情况下，时间复杂度是O(N^2)
/// </summary>
public class 快速排序 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };

        sort(array, 0, array.Length - 1);


    }

    void sort(int[] array, int startIndex, int endIndex)
    {
        if (startIndex >= endIndex)
        {
            return;
        }
        int pivotIndex = partition(array, startIndex, endIndex);

        sort(array, startIndex, pivotIndex - 1);
        sort(array, pivotIndex + 1, endIndex);

        StringBuilder stringBuilder = new StringBuilder();
        for (int x = 0; x < array.Length; x++)
        {
            stringBuilder.Append(array[x]);
        }
        Debug.Log(stringBuilder.ToString());

    }

    int partition(int[] array, int startIndex, int endIndex)
    {
        int pivot = array[startIndex];
        int left = startIndex;
        int right = endIndex;
        int index = startIndex;

        while (right > left)
        {
            while (right > left)
            {
                if (array[right] < pivot)
                {
                    array[left] = array[right];
                    index = right;
                    left++;
                    break;
                }
                right--;
            }

            while (right > left)
            {
                if (array[left] > pivot)
                {
                    array[right] = array[left];
                    index = left;
                    right--;
                    break;
                }
                left++;
            }
        }

        array[index] = pivot;
        return index;
    }
}
