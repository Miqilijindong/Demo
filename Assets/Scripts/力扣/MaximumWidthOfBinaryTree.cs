using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximumWidthOfBinaryTree : MonoBehaviour
{
    private void Start()
    {
        int?[] arr = { 1, 3, 2, 5, 3, null, 9 };
        TreeNode treeNode = LongestZigZagPathInABinaryTree.CreateTree(arr);

        int v = WidthOfBinaryTree(treeNode);
        print(v);
    }

    /// <summary>
    /// ������������
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int WidthOfBinaryTree(TreeNode root)
    {
        // �洢ÿһ���ж�Ӧ�Ŀ��
        Dictionary<int, int> set = new Dictionary<int, int>();
        return DFS(root, 1, 0, set);
    }

    /// <summary>
    /// ������������в���������
    /// </summary>
    /// <param name="node"></param>
    /// <param name="index"></param>
    /// <param name="level"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    private int DFS(TreeNode node, int index, int level, Dictionary<int, int> set)
    {
        if (node == null) return 0;

        if (!set.ContainsKey(level))
        {
            set[level] = index;
        }

        // ��ǰ��������
        int cur = index - set[level] + 1;

        int left = DFS(node.left, index * 2 - 1, level + 1, set);
        int right = DFS(node.right, index * 2, level + 1, set);

        return Mathf.Max(cur, Mathf.Max(left, right));
    }
}
