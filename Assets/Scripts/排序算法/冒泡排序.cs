using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 时间复杂度是O(N^2)，最优复杂度是O(N)
/// 空间复杂度是O(1)
/// 稳定性，元素两两交换时，相同元素的前后顺序并没有改变，所以冒泡排序是稳定排序算法
/// </summary>
public class 冒泡排序 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 4, 5, 3, 1, 2 };

        for(int i = 0; i < array.Length - 1; i++)
        {
            bool isSort = true;
            for(int j = 0; j < array.Length - 1 - i; j++)
            {
                if(array[j + 1] < array[j])
                {
                    isSort = false;
                    int temp = array[j + 1];
                    array[j + 1] = array[j];
                    array[j] = temp;
                }
            }

            if(isSort)
            {
                break;
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
