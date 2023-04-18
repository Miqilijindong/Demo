using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You are given a list of strings of the same length words and a string target.
/// Your task is to form target using the given words under the following rules:
/// target should be formed from left to right.
/// To form the ith character (0-indexed) of target, you can choose the kth character of the jth string in words if target[i] = words[j][k].
/// Once you use the kth character of the jth string of words, you can no longer use the xth character of any string in words where x <= k. In other words, all characters to the left of or at index k become unusuable for every string.
/// Repeat the process until you form the string target.
/// Notice that you can use multiple characters from the same string in words provided the conditions above are met.
/// Return the number of ways to form target from words. Since the answer may be too large, return it modulo 109 + 7.
/// </summary>
public class NumberOfWaysToFormATargetStringGivenADictionary : MonoBehaviour
{
    private void Start()
    {
        string[] vs = new string[] { "acca", "bbbb", "caca" };
        string target = "aba";

        int v = numWays(vs, target);

        print(v);
    }

    public int numWays(string[] words, string target)
    {
        int n = words[0].Length;
        int m = target.Length;
        int mod = 1000000007;
        int[] dp = new int[m + 1];
        dp[0] = 1;

        //int[][] count = new int[n][];
        int[,] count = new int[n, 26];
        foreach (string word in words)
        {
            for (int i = 0; i < n; i++)
            {
                //count[i][word[i] - 'a']++;
                count[i, word[i] - 'a']++;
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = m - 1; j >= 0; j--)
            {
                //dp[j + 1] += (int)((long)dp[j] * count[i][target[j] - 'a'] % mod);
                dp[j + 1] += (int)((long)dp[j] * count[i, target[j] - 'a'] % mod);
                dp[j + 1] %= mod;
            }
        }

        return dp[m];
    }
}
