
using System;
using System.Collections.Generic;
using UnityEngine;

public class 划分数组为连续的集合 : MonoBehaviour
{
    private void Start()
    {
        int[] arr = { 1, 2, 3, 3, 4, 4, 5, 6 };
        IsPossibleDivide(arr, 4);
    }

    public bool IsPossibleDivide(int[] nums, int k)
    {
        if (nums.Length % k != 0) return false;

        Array.Sort(nums);

        Dictionary<int, int> dic = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (!dic.ContainsKey(nums[i]))
                dic.Add(nums[i], 0);
            dic[nums[i]]++;
        }
        foreach (var x in nums)
        {
            if (!dic.ContainsKey(x))
            {
                continue;
            }

            for (int j = 0; j < k; j++)
            {
                int num = x + j;
                if(dic.ContainsKey(num))
                {
                    return false;
                }
                dic[num]--;

                if (dic[num] == 0)
                {
                    dic.Remove(num);
                }
            }
        }

        return true;
    }
}