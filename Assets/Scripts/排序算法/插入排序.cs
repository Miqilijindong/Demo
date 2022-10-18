using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 插入排序是一种简单直观的排序算法，它的工作原理是通过构建有序序列，对于未排序数据，在已排序中从后向前扫描，找到相应位置并插入
/// 时间复杂度是O(N^2)
/// 空间复杂度是O(1)
/// 是稳定性排序，无序数列插入到有序数区里，不会改变相同元素的前后顺序，是一种稳定排序算法
/// </summary>
public class 插入排序 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };
        int insertValue, j;
        for (int i = 1; i < array.Length; i++)
        {
            insertValue = array[i];
            j = i - 1;
            for (; j >= 0 && insertValue < array[j]; j--)
            {
                array[j + 1] = array[j];
            }
            array[j + 1] = insertValue;


            StringBuilder stringBuilder = new StringBuilder();
            for (int x = 0; x < array.Length; x++)
            {
                stringBuilder.Append(array[x]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }
}
