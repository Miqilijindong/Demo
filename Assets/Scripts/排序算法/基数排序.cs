using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class 基数排序 : MonoBehaviour
{
    private void Start()
    {
        int[] arr = { 6, 56, 89, 12, 394, 21, 11, 156, 657 };
        radixSort(arr);
    }

    private void radixSort(int[] arr)
    {
        // 得到数组最大数
        int maxNum = arr[0];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] > maxNum)
            {
                maxNum = arr[i];
            }
        }

        // 得到最大数是几位数
        int maxLength = (maxNum + "").Length;

        // 定义一个二维数组，表示10个桶，每个桶就是一个一维数组
        int[][] bucket = new int[10][];
        // 每个桶存入了几个数字
        int[] everyBucketNum = new int[10];

        // n* = 10的原因是
        // 123取出个位数字是123%10，即123/1 %10
        // 123取出十位数字是123/10%10；
        // 123取出百位数字是123/100%10
        // 以此类推
        for (int i = 0, n = 1; i < maxLength; i++, n *= 10)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                // 取出每个元素的对应位的值
                int digit = arr[j] / n % 10;
                // 这个应该是只能这么写，因为实例化的时候，不知道为什么不能给第二个[]赋值，所有只能在遍历第一个[]时赋值
                if (bucket[digit] == null) 
                    bucket[digit] = new int[arr.Length];

                // 放入到对应的堆
                bucket[digit][everyBucketNum[digit]] = arr[j];
                everyBucketNum[digit]++;
            }

            // 按照这个桶的顺序(以为数组的下标依次取出数据，放入原来数组)
            int index = 0;

            // 遍历每一个桶，并将桶中是数据，放入到原数组
            for (int k = 0; k < everyBucketNum.Length; k++)
            {
                if(everyBucketNum[k] != 0)
                {
                    for(int j = 0; j < everyBucketNum[k]; j++)
                    {
                        arr[index++] = bucket[k][j];
                    }
                }
                // 放回原数组后，需要将每个everyBucketNum[k] = 0
                everyBucketNum[k] = 0;
            }


            StringBuilder stringBuilder = new StringBuilder();
            for (int x = 0; x < arr.Length; x++)
            {
                stringBuilder.Append(arr[x]);
                if (x != arr.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }
            Debug.Log(stringBuilder.ToString());
        }


    }
}