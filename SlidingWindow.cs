using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class SlidingWindow
    {
        public static int MaxSubArrayLen(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>() { { 0, -1 } };
            int sum = 0, max = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (dict.ContainsKey(sum - k))
                    max = Math.Max(max, i - dict[sum - k]);

                if (!dict.ContainsKey(sum))
                    dict[sum] = i;
            }

            return max;
        }

        public static List<int> find_top_k_frequent_elements(List<int> arr, int k)
        {
            // Write your code here.
            Dictionary<int, int> dict = new Dictionary<int, int>();

            foreach (int ele in arr)
            {
                if (!dict.ContainsKey(ele))
                    dict.Add(ele, 1);
                else
                    dict[ele]++;
            }
            var x = dict.OrderByDescending(e => e.Value).Select(e => e.Key).Take(k).ToList();
            return dict.OrderByDescending(e => e.Value).Select(e => e.Key).Take(k).ToList();
        }

        public static int subarraySum(int[] nums, int k)
        {
            int count = 0, sum = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (map.ContainsKey(sum - k))
                    count += map[sum - k];
                else
                    map.Add(sum, 1);
            }
            return count;
        }

        public static bool subarraySum_contains(int[] nums, int k)
        {
            int count = 0, sum = 0;
            HashSet<int> map = new HashSet<int>();
            //map.Add(0, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (map.Contains(sum - k))
                    return true;
                else
                    map.Add(sum);
            }
            return false;
        }

        public static int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n];

            int overAllMax = 0;
            for (int i = 1; i < n; i++)
            {
                int currMax = 0;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (nums[i] > nums[j])
                    {
                        currMax = Math.Max(currMax, dp[j] + 1);
                    }
                    overAllMax = Math.Max(overAllMax, currMax);
                    dp[i] = currMax;
                }
            }
            return overAllMax + 1;
        }
        public static int CountZeroSumSlices(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n];
            dp[0] = nums[0];
            int count = 0;
            bool found = false;
            for (int i = 1; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += nums[j];
                    if (sum == 0 || nums[j] == 0)
                    {
                        found = true;
                        count++;
                    }
                    dp[i] = sum;
                }
            }
            return found && count < 1000000000 ? count : -1;
        }
        public static bool IsAlienSorted(string[] words, string order)
        {
            Dictionary<char, int> mappings = setMappings(order);
            for (int i = 0; i < words.Length - 1; i++)
            {

                for (int j = 0; j < words[i].Length; j++)
                {
                    string curr = words[i];
                    string next = words[i + 1];
                    // If we do not find a mismatch letter between words[i] and words[i + 1],
                    // we need to examine the case when words are like ("apple", "app").
                    if (j >= next.Length) return false;

                    if (curr[j] != next[j])
                    {
                        char currentWordChar = curr[j];
                        char nextWordChar = next[j];
                        if (mappings[currentWordChar] > mappings[nextWordChar]) return false;
                        // if we find the first different letter and they are sorted,
                        // then there's no need to check remaining letters
                        else break;
                    }
                }
            }
            return true;
        }

        private static Dictionary<char, int> setMappings(string order)
        {
            Dictionary<char, int> mappings = new Dictionary<char, int>();
            for (int i = 0; i < order.Length; i++)
            {
                mappings.Add(order[i], i);
            }
            return mappings;
        }
        public static bool ValidPalindrome(string s)
        {
            int start = 0;
            int end = s.Length - 1;
            while (start < end)
            {
                if (s[start] != s[end])
                {
                    return ValidPalindromeHelper(s.Remove(start))
                        || ValidPalindromeHelper(s.Remove(end));
                }
                start++;
                end--;
            }

            return true;
        }

        public static bool ValidPalindromeHelper(string s)
        {
            int start = 0;
            int end = s.Length - 1;

            while (start < end)
            {
                if (s[start] != s[end])
                {
                    return false;
                }
                start++;
                end--;
            }

            return true;
        }
    }
}
