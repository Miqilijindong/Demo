using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 罗马数字转十进制
/// Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
/// I             1
/// V             5
/// X             10
/// L             50
/// C             100
/// D             500
/// M             1000
/// Roman numerals are usually written largest to smallest from left to right. However, the numeral for four is not IIII. Instead, the number four is written as IV. Because the one is before the five we subtract it making four. The same principle applies to the number nine, which is written as IX. There are six instances where subtraction is used:
/// I can be placed before V (5) and X (10) to make 4 and 9. 
/// X can be placed before L (50) and C (100) to make 40 and 90. 
/// C can be placed before D (500) and M (1000) to make 400 and 900.
/// </summary>
public class RomanToIntClass : MonoBehaviour
{
    public string s;
    /// <summary>
    /// Dictionary初始化赋值方式
    /// </summary>
    public Dictionary<string, int> RomanTrans = new Dictionary<string, int>() { { "I", 1 }, { "V", 5 }, { "X", 10 }, { "L", 50 }, { "C", 100 }, { "D", 500 }, { "M", 1000 } };
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < RomanTrans.Count; i++)
        {
            // 要引入Linq
            print(RomanTrans.ElementAt(i).Value);
        }
        print(RomanToInt(s));
    }

    public int RomanToInt(string s)
    {
        int num = 0;
        int all = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            switch (s[i])
            {
                case 'I':
                    num = 1;
                    break;
                case 'V':
                    num = 5;
                    break;
                case 'X':
                    num = 10;
                    break;
                case 'L':
                    num = 50;
                    break;
                case 'C':
                    num = 100;
                    break;
                case 'D':
                    num = 500;
                    break;
                case 'M':
                    num = 1000;
                    break;
            }
            // IV = 4    VI = 6
            // 这里的判断逻辑是根据 当前下标的值 * 4 比较之前的合计。
            if (4 * num < all)
            {
                all -= num;
            }
            else
            {
                all += num;
            }
        }
        return all;
    }
}
