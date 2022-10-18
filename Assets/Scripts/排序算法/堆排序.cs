using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 堆排序是利用二叉堆的概念来排序的选择排序算法,分为两种:升序排序-利用最大堆进行排序、降序排序:利用最小对进行排序
/// 时间复杂度等同于堆的高度O(logn)，构建二叉堆执行下沉调整次数是n/2，循环删除进行下沉调整次数是n-1，时间复杂度约为O(nlogn)
/// 空间复杂度则是O(1)，因为堆排序算法排序过程中只需要一个临时变量两两交换
/// 稳定性则是因为相同的元素前后顺序有可能发生变化，所以堆排序是一种不稳定的排序算法
/// </summary>
public class 堆排序 : MonoBehaviour
{
    void downAdjust(int[] arr, int parentIndex, int length)
    {
        // 缓存父节点的值，用于最后进行赋值，而不需要每一步进行交换
        int temp = arr[parentIndex];
        // 孩子节点下标，暂时定位左孩子节点下标
        int childIndex = 2 * parentIndex + 1;

        while (childIndex < length)
        {
            // 当存在右孩子节点，且右孩子节点的值小于左孩子节点的值，childIndex记录为右孩子节点的下标
            // childIndex实际记录的是最小的孩子节点的下标
            if (childIndex + 1 < length && arr[childIndex + 1] > arr[childIndex])
            {
                childIndex++;
            }

            // 如果父节点的值比孩子节点的值都小，则调整结束
            if (temp >= arr[childIndex])
            {
                break;
            }

            // 将最小的孩子节点的值赋值给父节点
            arr[parentIndex] = arr[childIndex];
            parentIndex = childIndex;
            childIndex = 2 * parentIndex + 1;
        }
        arr[parentIndex] = temp;
    }

    // 堆排序
    void sort(int[] arr)
    {
        StringBuilder stringBuilder;
        // 把无序数组构建成二叉树
        for (int i = arr.Length / 2 - 1; i >= 0; i--)
        {
            downAdjust(arr, i, arr.Length);

            stringBuilder = new StringBuilder();
            for (int x = 0; x < arr.Length; x++)
            {
                stringBuilder.Append(arr[x]);
            }
            Debug.Log(stringBuilder.ToString());
        }

        Debug.Log("----------------------------");

        for (int i = arr.Length - 1; i > 0; i--)
        {
            // 最后一个元素与第一个元素交换
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;
            downAdjust(arr, 0, i);


            stringBuilder = new StringBuilder();
            for (int x = 0; x < arr.Length; x++)
            {
                stringBuilder.Append(arr[x]);
            }
            Debug.Log(stringBuilder.ToString());
        }
    }

    private void Start()
    {
        int[] array = { 2, 5, 4, 3, 1, 6 };

        sort(array);
    }
}