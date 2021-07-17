using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class DP
    {
        public static int levenshteinDistance(string strWord1, string strWord2)
        {
            int word1Len = strWord1.Length;
            int word2Len = strWord2.Length;
            var table = new int[word1Len + 1, word2Len + 1];

            //base Cases
            table[0, 0] = 0;

            for (int row = 1; row < word1Len + 1; row++)
            {
                table[row, 0] = row;
            }

            for (int col = 1; col < word2Len + 1; col++)
            {
                table[0, col] = col;
            }

            //recursive case:
            for (int row = 1; row < word1Len + 1; row++)
            {
                for (int col = 1; col < word2Len + 1; col++)
                {
                    if (strWord1[row - 1] == strWord2[col - 1])
                        table[row, col] = table[row - 1, col - 1];
                    else
                        table[row, col] = Math.Min(table[row, col - 1] + 1, Math.Min(table[row - 1, col] + 1, table[row - 1, col - 1] + 1));
                }
            }
            return table[word1Len, word2Len];
        }
        public static int MinNumberOfCoinsForChange(int n, int[] denoms)
        {
            // Write your code here.
            if (n == 0 || denoms == null || denoms.Length == 0)
                return 0;

            int[] table = new int[n + 1];
            table[0] = 0;

            for (int i = 1; i < n + 1; i++)
            {
                int minVal = int.MaxValue;
                foreach (var denom in denoms)
                {
                    if (i - denom >= 0 && table[i - denom] >= 0)
                    {
                        minVal = Math.Min(minVal, table[i - denom]);
                    }
                }
                if (minVal == int.MaxValue)
                    table[i] = -1;
                else
                    table[i] = minVal + 1;

            }
            return table[n] == 0 ? -1 : table[n];
        }
        public static List<List<int>> MaxSumIncreasingSubsequence(int[] array)
        {
            int n = array.Length;
            ValuePath[] table = new ValuePath[n];
            table[0] = new ValuePath(array[0], 0);

            for (int i = 1; i < n; i++)
            {
                int maxValue = int.MinValue;
                int currVal = array[i];
                var valPath = new ValuePath(currVal, i);
                var path = new List<int>();
                for (int j = i; j > 0; j--)
                {
                    var prevArrVal = array[i - j];
                    var tableValuePath = table[i - j];
                    var subSeqSum = currVal + tableValuePath.Value;
                    if (currVal >  prevArrVal && subSeqSum > currVal && subSeqSum > maxValue)
                    {
                        path.Clear();
                        maxValue = currVal + tableValuePath.Value;
                        path.AddRange(tableValuePath.Path);
                    }
                }
                if(maxValue != int.MinValue)
                {
                    valPath.Value = maxValue;
                    valPath.Path.AddRange(path);
                }
                table[i] = valPath;
            }

            var maxSubSeq = table.OrderByDescending(e => e.Value).FirstOrDefault();
            var indices = maxSubSeq.Path.OrderBy(e => e).ToList();
            maxSubSeq.Path = indices.Select(e => array[e]).ToList();
            return new List<List<int>>(){
                new List<int>(){
                    maxSubSeq.Value, // Sum of the items.
			    },
                maxSubSeq.Path, // Item sequence.
		    };
        }

        public static List<List<int>> MaxSumIncreasingSubsequences(int[] array)
        {
            int n = array.Length;
            int?[] sequences = new int?[n];
            int[] table = new int[n];
            table[0] = array[0];
            int maxIdx = 0;
            int overAllMax = int.MinValue;

            for (int i = 1; i < n; i++)
            {
                int maxValue = int.MinValue;
                int currVal = array[i];
                int prevIdx = -1;
                int currPrevMaxIdx = -1;
                for (int j = i; j > 0; j--)
                {
                    prevIdx = i - j;
                    var prevArrVal = array[prevIdx];
                    var tableValuePath = table[prevIdx];
                    var subSeqSum = currVal + tableValuePath;
                    if (currVal > prevArrVal && subSeqSum > currVal && subSeqSum > maxValue)
                    {
                        maxValue = currVal + tableValuePath;
                        currPrevMaxIdx = prevIdx;
                    }
                }
                if (maxValue == int.MinValue)
                {
                    table[i] = currVal;
                }
                else
                {
                    table[i] = maxValue;
                    if (maxValue > overAllMax)
                    {
                        overAllMax = maxValue;
                        maxIdx = i;
                    }
                    sequences[i] = currPrevMaxIdx;
                }
            }

            var valIdx = sequences[maxIdx];
            var indices = new List<int>
            {
                maxIdx
            };
            while (valIdx != null)
            {
                indices.Add(valIdx.Value);
                valIdx = sequences[valIdx.Value];
            }
            indices = indices.OrderBy(e => e).ToList();
            var maxSubSeq = indices.Select(e => array[e]).ToList();
            return new List<List<int>>(){
                new List<int>(){
                     overAllMax, // Sum of the items.
		 	    },
                maxSubSeq, // Item sequence.
		    };
        }
        public static List<char> LongestCommonSubsequence(string str1, string str2)
        {
            int len1 = str1.Length;
            int len2 = str2.Length;
            string[,] table = new string[len1 + 1, len2 + 1];
            table[0, 0] = string.Empty;

            for (int row = 1; row < len1 + 1; row++)
            {
                table[row, 0] = string.Empty;
            }
            for (int col = 1; col < len2 + 1; col++)
            {
                table[0, col] = string.Empty;
            }
            for (int row = 1; row < len1 + 1; row++)
            {
                for (int col = 1; col < len2 + 1; col++)
                {
                    if (str1[row - 1] == str2[col - 1])
                    {
                        table[row, col] = table[row - 1, col - 1] + str1[row - 1];
                        //table[row, col] = new string(table[row - 1, col - 1].Append(str1[row - 1]).ToArray());
                    }
                    else
                    {
                        table[row, col] = table[row, col] = table[row - 1, col].Length >= table[row, col - 1].Length ? table[row - 1, col] : table[row, col - 1];
                    }

                }
            }
            return table[len1, len2].ToCharArray().ToList();
        }
        public static int WaterArea(int[] heights)
        {
            // Write your code here.
            if (heights == null || heights.Length == 0)
                return 0;
            int n = heights.Length;
            int[] leftMax = new int[n];
            int[] rightMax = new int[n];
            int[] area = new int[n];

            int currLeftMax = int.MinValue;
            int currRightMax = int.MinValue;

            for (int i = 1; i < n; i++)
            {
                currLeftMax = Math.Max(currLeftMax, heights[i - 1]);
                leftMax[i] = currLeftMax;
            }

            for (int i = n - 2; i > 0; i--)
            {
                currRightMax = Math.Max(currRightMax, heights[i + 1]);
                rightMax[i] = currRightMax;
            }

            for (int i = 0; i < n; i++)
            {
                int currMin = Math.Min(leftMax[i], rightMax[i]);
                if (heights[i] < currMin)
                    area[i] = currMin - heights[i];
            }

            int surfaceArea = 0;

            foreach (int a in area)
            {
                surfaceArea += a;
            }
            return surfaceArea;
        }
        public static int NumDecodingsTabulation(string s)
        {
            if (s == null || s.Length == 0 || s[0] == '0')
                return 0;

            int n = s.Length;
            int[] table = new int[n + 1];

            table[0] = 1;
            table[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                int oneDigit = Convert.ToInt32(s.Substring(i - 1, 1));
                int twoDigit = Convert.ToInt32(s.Substring(i - 2, 2));

                if (oneDigit >= 1)
                    table[i] += table[i - 1];
                if (twoDigit >= 10 && twoDigit <= 26)
                    table[i] += table[i - 2];
            }
            return table[n];
        }
        public static int NumDecodingsMemo(string s)
        {
            int n = s.Length;
            int[] memo = new int[n + 1];
            Array.Fill(memo, -1);
            memo[0] = 1;

            return helper(s, n, memo);
        }

        public static int helper(string s, int n, int[] memo)
        {
            if (n == 0)
                return 1;

            int idx = s.Length - n;
            if (s[idx] == '0')
                return 0;

            if (memo[n] != -1)
                return memo[n];

            int result = helper(s, n - 1, memo);

            if (n >= 2 && Convert.ToInt32(s.Substring(0, n)) <= 26)
                result += helper(s, n - 2, memo);

            memo[n] = result;
            return result;
        }
        public static int MinCost(int[][] costs)
        {
            int red = 0;
            int blue = 1;
            int green = 2;

            for (int i = 1; i < costs.Length; i++)
            {
                costs[i][red] = costs[i][red] + Math.Min(costs[i - 1][green], costs[i - 1][blue]);
                costs[i][green] = costs[i][green] + Math.Min(costs[i - 1][red], costs[i - 1][blue]);
                costs[i][blue] = costs[i][blue] + Math.Min(costs[i - 1][red], costs[i - 1][green]);
            }
        
            return costs[costs.Length - 1].Min();
        }
        public static bool IsInterleave(string s1, string s2, string s3)
        {
            if (s1.Length + s2.Length != s3.Length)
                return false;

            int len1 = s1.Length;
            int len2 = s2.Length;
            int len3 = s3.Length;
            bool isPossible = false;
            helper(s1, s2, s3, s3.Length, new List<char>(), 0, 0, ref isPossible);

            return isPossible;
        }

        public static void helper(string s1, string s2, string s3, int length, List<char> slate, int idx1, int idx2, ref bool isPossible)
        {
            if (idx1 == s1.Length || idx2 == s2.Length)
                return;

            if (slate.Count() == length)
            {
                var str = new String(slate.ToArray());
                if (str.Equals(s3))
                    isPossible = true;
                return;
            }

            if (!isPossible)
            {
                slate.Add(s1[idx1]);
                helper(s1, s2, s3, length, slate, idx1 + 1, idx2, ref isPossible);
                slate.RemoveAt(slate.Count() - 1);

                slate.Add(s2[idx2]);
                helper(s1, s2, s3, length, slate, idx1, idx2 + 1, ref isPossible);
                slate.RemoveAt(slate.Count() - 1);
            }
        }
        public static bool isInterleave(string s1, string s2, string s3)
        {
            if (s3.Length != s1.Length + s2.Length)
                return false;

            bool[,] dp = new bool[s1.Length + 1, s2.Length + 1];
            dp[0, 0] = true;
            for (int row = 1; row <= s1.Length; row++)
            {
                dp[row, 0] = dp[row - 1, 0] && s1[row - 1] == s3[row + 0 - 1];
            }
            for (int col = 1; col <= s2.Length; col++)
            {
                dp[0, col] = dp[0, col - 1] && s2[col - 1] == s3[0 + col - 1];
            }

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    dp[i, j] = (dp[i - 1, j] && s1[i - 1] == s3[i + j - 1]) || (dp[i, j - 1] && s2[j - 1] == s3[i + j - 1]);
                }
            }
            return dp[s1.Length, s2.Length];
        }
        public static int MaxProfitWithKTransactions(int[] prices, int k)
        {
            if (prices.Length == 0)
                return 0;

            int[,] table = new int[k + 1, prices.Length];
            for (int row = 1; row < k + 1; row++)
            {
                for (int col = 1; col < prices.Length; col++)
                {
                    table[row, col] = Math.Max(table[row, col - 1], prices[col] + findPreviousMaxProfit(prices, table, row, col));
                }
            }
            return table[k, prices.Length - 1];
        }

        private static int findPreviousMaxProfit(int[] prices, int[,] profit, int t, int d)
        {
            int maxProfit = int.MinValue;
            for (int i = 0; i < d; i++)
            {
                maxProfit = Math.Max(maxProfit, -prices[i] + profit[t - 1, i]);
            }
            return maxProfit;
        }

        public static bool WordBreak(string s, IList<string> wordDict)
        {
            HashSet<string> wordSet = ConvertToDict(wordDict);
            int n = s.Length;
            bool[] table = new bool[n + 1];
            table[0] = true;
            
            for (int i = 1; i < n + 1; i++)
            {
                var stInd = n - i;
                for (int j = 1; j <= i; j++)
                {
                    var prefix = s.Substring(stInd, j);
                    if (wordSet.Contains(prefix) && table[i - j])
                    {
                        table[i] = true;
                        break;
                    }
                }

            }

            return table[n];
        }

        private static HashSet<string> ConvertToDict(IList<string> wordDict)
        {
            HashSet<string> set = new HashSet<string>();

            foreach (string word in wordDict)
            {
                if (!set.Contains(word))
                    set.Add(word);
            }

            return set;
        }
        public static int LeastBricks(IList<IList<int>> wall)
        {
            if (wall.Count() == 0)
                return 0;

            int n = wall.Count();
            Dictionary<int, int> cuts = new Dictionary<int, int>();

            foreach (var row in wall)
            {
                int count = 0;
                for (int i = 0; i < row.Count() - 1; i++)
                {
                    count += row[i];
                    if (!cuts.ContainsKey(count))
                        cuts.Add(count, 1);
                    else
                        cuts[count]++;
                }
            }
            if (cuts.Count == 0)
                return n;

            int maxCut = cuts.Select(e => e.Value).Max();
            return n - maxCut;
        }

    }

    public class ValuePath
    {
        public int Value { get; set; }
        public List<int> Path { get; set; }

        public ValuePath(int value, int index)
        {
            this.Value = value;
            this.Path = new List<int>() { index };
        }
    }
}
