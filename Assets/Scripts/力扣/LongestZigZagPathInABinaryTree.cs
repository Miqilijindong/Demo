using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongestZigZagPathInABinaryTree : MonoBehaviour
{
    public int maxLenght;

    // Start is called before the first frame update
    void Start()
    {
        // 但是加了问号不知道有什么后果
        int? i = null;// 基础数据类型要想为空可以在声明时加？
        // 这里的排序是层次排序
        int?[] arr = { 6, 9, 7, 3, null, 2, 8, 5, 8, 9, 7, 3, 9, 9, 4, 2, 10, null, 5, 4, 3, 10, 10, 9, 4, 1, 2, null, null, 6, 5, null, null, null, null, 9, null, 9, 6, 5, null, 5, null, null, 7, 7, 4, null, 1, null, null, 3, 7, null, 9, null, null, null, null, null, null, null, null, 9, 9, null, null, null, 7, null, null, null, null, null, null, null, null, null, 6, 8, 7, null, null, null, 3, 10, null, null, null, null, null, 1, null, 1, 2 };

        TreeNode treeNode = CreateTree(arr);

        int v = LongestZigZag(treeNode);
        print(v);
    }

    /// <summary>
    /// 创建层次排序二叉树(这个不行)
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public TreeNode BuildTree(int?[] arr, int index)
    {
        if (arr[index] == null)
        {
            return null;
        }
        TreeNode treeNode = new TreeNode(arr[index]);

        if (arr[index * 2] != null)
        {
            treeNode.left = BuildTree(arr, index * 2);
        }
        if (arr[index * 2 + 1] != 0)
        {
            treeNode.right = BuildTree(arr, index * 2 + 1);
        }
        return treeNode;
    }

    /// <summary>
    /// 创建层次排序的二叉树
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static TreeNode CreateTree(int?[] arr)
    {
        Queue<TreeNode> q = new Queue<TreeNode>();

        TreeNode t = new TreeNode(arr[0]);
        q.Enqueue(t);

        int index = 0;
        while (q.Count != 0)
        {
            index++;
            TreeNode treeNode = q.Dequeue();

            if (arr.Length > index && arr[index] != null)
            {
                treeNode.left = new TreeNode(arr[index]);
                q.Enqueue(treeNode.left);
            }


            index++;
            if (arr.Length > index && arr[index] != null)
            {
                treeNode.right = new TreeNode(arr[index]);
                q.Enqueue(treeNode.right);
            }
        }

        return t;
    }

    public int LongestZigZag(TreeNode root)
    {
        CalculateLenght(root, 0, 0);
        CalculateLenght(root, 1, 0);
        return maxLenght;
    }

    /// <summary>
    /// 计算最大长度
    /// </summary>
    /// <param name="root"></param>
    /// <param name="dir">方向：0-左、1-右</param>
    /// <param name="currentLenght"></param>
    public void CalculateLenght(TreeNode root, int dir, int currentLenght)
    {
        if (root == null || root.val == null)
        {
            return;
        }
        maxLenght = Mathf.Max(maxLenght, currentLenght);

        // 当前子树指向右边，则当前子树的子树必须是向左边，maxLenght + 1
        if (dir == 1)
        {
            CalculateLenght(root.left, 0, currentLenght + 1);
            CalculateLenght(root.right, 1, 1);// 如果是同一个方向的话，重新计算
        }
        // 当前子树向左边，则当前子树的子树必须是向右边，maxLenght + 1
        else
        {
            CalculateLenght(root.left, 0, 1);
            CalculateLenght(root.right, 1, currentLenght + 1);
        }
    }
}
