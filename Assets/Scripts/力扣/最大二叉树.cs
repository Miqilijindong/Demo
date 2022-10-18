
using UnityEngine;

public class TreeNode {
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class 最大二叉树 : MonoBehaviour
{
    private void Start()
    {
        int[] arr = { 3, 2, 1, 6, 0, 5 };
        TreeNode treeNode = ConstructMaximumBinaryTree(arr);
    }

    public TreeNode ConstructMaximumBinaryTree(int[] nums)
    {
        return build(nums, 0, nums.Length - 1);
    }

    public TreeNode build(int[] nums, int left, int right)
    {
        if (left > right) return null;
        int index = left;
        for (int i = left+1; i <= right; i++)
        {
            if (nums[index] < nums[i]) index = i;
        }
        TreeNode tree = new TreeNode(nums[index]);
        tree.left = build(nums, left, index - 1);
        tree.right = build(nums, index + 1, right);

        return tree;
    }
}