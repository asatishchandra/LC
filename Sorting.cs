using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class Sorting
    {
        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }
                Swap(i, minIndex, arr);
            }
        }
        //64, 25, 12, 22, 11
        public static void BubleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = arr.Length - 1; j > 0; j--)
                {
                    if (arr[j - 1] > arr[j])
                        Swap(j - 1, j, arr);
                }
            }
        }

        public static void InsertionSort(int[] arr)
        {
            //InsertionSortHelper(arr, arr.Length - 1);
            InsertionSortIterativeHelper(arr);
            Console.WriteLine(arr);
        }

        public static void MergeSort(int[] arr)
        {
            DivideHelper(arr, 0, arr.Length - 1);
            Console.WriteLine(arr);
        }

        public static void DivideHelper(int[] arr, int startIdx, int endIdx)
        {
            if (startIdx >= endIdx)
                return;
            int mid = (startIdx + endIdx) / 2;
            DivideHelper(arr, startIdx, mid);
            DivideHelper(arr, mid + 1, endIdx);

            MergeHelper(arr, startIdx, mid, endIdx);
        }

        //public static int MinimumWaitingTime(int[] queries)
        //{
        //    //Array.Sort(queries);
        //    queries = queries.OrderByDescending(e => e).ToArray();
        //    int waitingTime = 0;
        //    for (int i = 1; i < queries.Length; i++)
        //    {
        //        int duration = queries[i - 1];
        //        waitingTime = waitingTime + duration;
        //    }
        //    return waitingTime;
        //}

        public static int MinimumWaitingTime(int[] queries)
        {
            Array.Sort(queries);

            int waitingTime = 0;
            for (int i = 0; i < queries.Length; i++)
            {
                int duration = queries[i];
                int left = queries.Length - (i + 1);
                waitingTime += duration * left;
            }
            return waitingTime;
        }
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<List<int>>();
            if (nums == null || nums.Length == 0 || nums.Length == 1)
                return (IList<IList<int>>)result;
            Array.Sort(nums);
            int i = 0;
            int k = nums.Length - 1;

            while (i <= k && i + 1 < k)
            {
                for (int j = i + 1; j < k; j++)
                {
                    int sum = nums[i] + nums[k] + nums[j];
                    if (sum > 0)
                        k--;
                    else if (sum < 0)
                        i++;
                    else
                    {
                        i++;
                        k--;
                        result.Add(new List<int> { nums[i], nums[k], nums[j] });
                    }
                }
            }

            return result.ToArray();
        }
        private static void MergeHelper(int[] arr, int startIdx, int mid, int endIdx)
        {
            int n1 = mid - startIdx + 1;
            int n2 = endIdx - mid;

            int[] leftArr = new int[n1];
            int[] rightArr = new int[n2];

            int i, j;
            for (i = 0; i < n1; i++)
                leftArr[i] = arr[i + startIdx];

            for (j = 0; j < n2; j++)
                rightArr[j] = arr[j + mid + 1];
            
            i = 0;
            j = 0;

            int k = 0;
            
            
            while (i < n1 && j < n2)
            {
                if (leftArr[i] <= rightArr[j])
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }
        }
        public static int FindUnsortedSubarray(int[] nums)
        {
            int min = int.MaxValue, max = int.MinValue;
            bool flag = false;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < nums[i - 1])
                    flag = true;
                if (flag)
                    min = Math.Min(min, nums[i]);
            }
            flag = false;
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                if (nums[i] > nums[i + 1])
                    flag = true;
                if (flag)
                    max = Math.Max(max, nums[i]);
            }
            int l, r;
            for (l = 0; l < nums.Length; l++)
            {
                if (min < nums[l])
                    break;
            }
            for (r = nums.Length - 1; r >= 0; r--)
            {
                if (max > nums[r])
                    break;
            }
            return r - l < 0 ? 0 : r - l + 1;
        }
        public static IList<int> RightSideView(TreeNode root)
        {
            return new List<int>();
        }
        private static void InsertionSortHelper(int[] arr, int n)
        {
            if (n <= 0)
                return;
            
            InsertionSortHelper(arr, n - 1);
            int ele = arr[n];
            int j = n - 1;
            while(j >= 0 && arr[j] > ele)
            {
                arr[j+1] = arr[j];
                j--;
            }
            arr[j+1] = ele;
            return;
        }

        private static void InsertionSortIterativeHelper(int[] arr)
        {
            if (arr.Length <= 0)
                return;

            //InsertionSortHelper(arr, n - 1);
            for(int i = 1; i < arr.Length; i++)
            {
                int ele = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > ele)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = ele;
            }
            
            return;
        }


        private static void Swap(int index1, int index2, int[] arr)
        {
            int temp = arr[index1];
            arr[index1] = arr[index2];
            arr[index2] = temp;
        }
    }
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
