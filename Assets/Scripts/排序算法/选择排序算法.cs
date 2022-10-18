using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 选择排序是最简单直观的一种算法，基本思想就是从待排序的数据元素中选择最小的一个元素作为首元素，知道所有元素排完为止
/// 时间复杂度，共遍历n-1轮，所以时间复杂度是O(N^2)
/// 空间复杂度因为需要一个额外空间，所以是O(1)
/// 选择排序算法是不稳定排序算法，当出现相同元素，有可能会改变相同元素的顺序
/// 如: 254261->154262->124562->122456， 这里的第一个2变到了第二个2后面了
/// </summary>
public class 选择排序算法 : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };
        int minIndex = 0;
        for (int i = 0; i < array.Length - 1; i++)
        {
            minIndex = i;
            for(int j = i + 1; j < array.Length; j++)
            {
                minIndex = array[minIndex] > array[j] ? j : minIndex;
            }

            if(i != minIndex)
            {
                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int j = 0; j < array.Length; j++)
            {
                stringBuilder.Append(array[j]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }
}
