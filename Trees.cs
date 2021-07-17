using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class Trees
    {
		public static int maxSum = int.MinValue;
		public static int MaxPathSum(BinaryTree tree)
		{
			// Write your code here
			if (tree == null)
				return -1;

			helper(tree);
			return maxSum;
		}

		private static int helper(BinaryTree tree)
		{
			if (tree.left != null && tree.right != null)
			{
				return tree.value;
			}

			int ls = 0;
			int rs = 0;

			if (tree.left != null)
			{
				ls += helper(tree.left);
			}
			if (tree.right != null)
			{
				rs += helper(tree.right);
			}
			maxSum = Math.Max(maxSum, ls + rs);

			return Math.Max(ls, rs);

		}
        public static IList<string> Result;
        public static IList<string> BinaryTreePaths(TreeNode root)
        {
            if (root == null)
                return new List<string>();

            Result = new List<string>();

            preOrder(root, new List<string>());

            return Result;
        }

        public static void preOrder(TreeNode root, List<string> slate)
        {
            if (root.left == null && root.right == null)
            {
                if (slate.Count() == 0)
                    slate.Add(root.val.ToString());
                else
                    slate.Add($"->{root.val}");

                Result.Add(string.Join("", slate));
                slate.RemoveAt(slate.Count() - 1);
            }

            if (root.left != null)
            {
                if (slate.Count() == 0)
                    slate.Add(root.val.ToString());
                else
                    slate.Add($"->{root.val}");

                preOrder(root.left, slate);
                slate.RemoveAt(slate.Count() - 1);
            }

            if (root.right != null)
            {
                if (slate.Count() == 0)
                    slate.Add(root.val.ToString());
                else
                    slate.Add($"->{root.val}");

                preOrder(root.right, slate);
                slate.RemoveAt(slate.Count() - 1);
            }
        }


    }
	public class BinaryTree
	{
		public int value;
		public BinaryTree left;
		public BinaryTree right;

		public BinaryTree(int value)
		{
			this.value = value;
		}
	}
}
