using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 希尔排序是把记录按下标的一定增量分组，对每组使用插入排序算法，随着增量逐渐减少，每组包含的元素越来越多，当增量减至1时，所有元素被分为一组，算法终止。
/// 希尔排序算法利用了分组粗调的方式减少了插入排序算法的工作量，使得算法的平均时间复杂度低于O(N^2)，极端情况下时间复杂度任然为O(N^2)，甚至比插入排序算法那更慢
/// 空间复杂度是O(1)
/// 稳定性的话，因为会进行分组，中间过程中有可能改变相同元素的前后位置，因此是一种不稳定的排序算法
/// </summary>
public class 希尔排序 : MonoBehaviour
{
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };

        int length = array.Length;

        while(length > 1)
        {
            //使用希尔增量的方式，每次折半
            length /= 2;
            for (int i = 0; i < length; i++)
            {
                for (int j = i + length; j < array.Length; j = j + length)
                {
                    int temp = array[j];
                    int z;
                    for (z = j - length; z >= 0 && array[z] > temp; z = z - length)
                    {
                        array[z + length] = array[z];
                    }
                    array[z + length] = temp;

                    StringBuilder stringBuilder = new StringBuilder();
                    for (int x = 0; x < array.Length; x++)
                    {
                        stringBuilder.Append(array[x]);
                    }
                    Debug.Log(stringBuilder.ToString());
                }
            }
        }
    }
}
