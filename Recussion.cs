using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class Recussion
    {
        public static IList<string> result = new List<string>();
        public static IList<string> LetterCombinations(string digits)
        {
            if (digits.Length == 0)
                return result;

            var digitLetterMap = new Dictionary<char, string>();

            digitLetterMap.Add('2', "abc");
            digitLetterMap.Add('3', "def");
            digitLetterMap.Add('4', "ghi");
            digitLetterMap.Add('5', "jkl");
            digitLetterMap.Add('6', "mno");
            digitLetterMap.Add('7', "pqrs");
            digitLetterMap.Add('8', "tuv");
            digitLetterMap.Add('9', "wxyz");

            helper(digits, digitLetterMap, new char[digits.Length], 0);
            return result;
        }

        public static void helper(string digits, Dictionary<char, string> digitLetterMap, char[] arr, int i)
        {
            if (i == digits.Length)
            {
                result.Add(new string(arr));
                return;
            }
            var letters = digitLetterMap[digits[i]];
            foreach (var c in letters)
            {
                arr[i] = c;
                helper(digits, digitLetterMap, arr, i+1);
            }
        }

        public static List<List<int>> get_permutations(List<int> arr)
        {
            var result = new List<List<int>>();
            if (arr.Count == 0)
                return result;

            Phelper(result, arr, 0);
            return result;

        }

        private static void Phelper(List<List<int>> result, List<int> arr, int i)
        {
            if (i == arr.Count)
            {
                var x = new List<int>();
                x .AddRange(arr);
                result.Add(x);
                return;
            }

            for (int k = i; k < arr.Count; k++)
            {
                Swap(arr, i, k);
                Phelper(result, arr, i + 1);
                Swap(arr, i, k);
            }
        }

        public static List<List<int>> Get_Permutations_No_Dups(List<int> arr)
        {
            var result = new List<List<int>>();

            if (arr.Count == 0)
                return result;

            Helper(result, arr, 0);

            return result;
        }

        private static void Helper(List<List<int>> result, List<int> arr, int i)
        {
            if (i == arr.Count)
            {
                var x = new List<int>();
                x.AddRange(arr);
                result.Add(x);
                return;
            }

            HashSet<int> set = new HashSet<int>();
            for (int k = i; k < arr.Count; k++)
            {
                if (!set.Contains(arr[k]))
                {
                    set.Add(arr[k]);
                    Swap(arr, i, k);
                    Helper(result, arr, i + 1);
                    Swap(arr, i, k);
                }
            }
        }

        public static List<string> getWordsFromPhoneNumber(string phoneNumber)
        {
            var result = new List<string>();

            var dict = new Dictionary<char, string>();
            dict.Add('2', "abc");
            dict.Add('3', "def");
            dict.Add('4', "ghi");
            dict.Add('5', "jkl");
            dict.Add('6', "mno");
            dict.Add('7', "pqrs");
            dict.Add('8', "tuv");
            dict.Add('9', "wxyz");

            StringBuilder inputStringPruned = new StringBuilder();
            for (int i = 0; i < phoneNumber.Length; i++)
            {
                if (phoneNumber[i] != '0' && phoneNumber[i] != '1')
                {
                    inputStringPruned.Append(phoneNumber[i]);
                }
            }
            if (inputStringPruned.Length == 0)
            {
                result.Add("-1");
                return result;
            }
            Helper(inputStringPruned.ToString(), result, dict, new char[phoneNumber.Length], 0);

            return result;
        }

        private static void Helper(string phoneNumber, List<string> result, Dictionary<char, string> dict, char[] slate, int i)
        {
            if (i == phoneNumber.Length)
            {
                result.Add(new string(slate));
                return;
            }
            
            var numLetters = dict[phoneNumber[i]];
            foreach (var c in numLetters)
            {
                slate[i] = c;
                Helper(phoneNumber, result, dict, slate, i + 1);
            }

        }

        public static string[] generate_all_expressions(string s, long target)
        {
            var res = new List<string>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Helper(s, target, 0, 0, 0, sb, res);
            return res.ToArray();
        }

        static void Helper(string s, long target, long cur, long prev, int i, System.Text.StringBuilder slate, List<string> res)
        {
            if (i == s.Length)
            {
                if (cur == target)
                {
                    res.Add(slate.ToString());
                }
                return;
            }

            for (int j = i; j < s.Length; j++)
            {
                var temp = s.Substring(i, j - i + 1);
                if (i == 0)
                {
                    slate.Append(temp);
                    Helper(s, target, long.Parse(temp), long.Parse(temp), j + 1, slate, res);
                    slate.Length = slate.Length - temp.Length;
                }
                else
                {
                    slate.Append("+");
                    slate.Append(temp);
                    Helper(s, target, cur + long.Parse(temp), long.Parse(temp), j + 1, slate, res);
                    slate.Length = slate.Length - temp.Length - 1;
                    slate.Append("*");
                    slate.Append(temp);
                    Helper(s, target, cur - prev + prev * long.Parse(temp), prev * long.Parse(temp), j + 1, slate, res);
                    slate.Length = slate.Length - temp.Length - 1;

                }
            }
        }
        public static List<List<int>> generate_all_combinations(List<int> arr, int target)
        {
            var result = new List<List<int>>();

            arr = arr.Distinct().OrderBy(e => e).ToList();
            Helper(result, arr, target, new List<int>(), target, 0);

            return result;
        }

        private static void Helper(List<List<int>> result, List<int> arr, int target, List<int> slate, int sum, int idx1)
        {
            if (sum == 0)
            {

                // Adding deep copy of list to ans

                result.Add(new List<int>(slate));
                return;
            }

            for (int i = idx1; i < arr.Count; i++)
            {

                // checking that sum does not become negative

                if ((sum - arr[i]) >= 0)
                {

                    // adding element which can contribute to
                    // sum

                    slate.Add(arr[i]);

                    Helper(result, arr, target, slate, sum - arr[i], i);

                    // removing element from list (backtracking)
                    slate.RemoveAt(slate.Count - 1);
                }
            }
        }

        public static int[][] spiral(int n)
        {
            var result = new List<int[]>();

            int i = 0;
            int j = 0;
            
            for(int k = 0; k < n; k++)
            {
                result[k] = new int[n];
            }
            int num = 0;

            while(!(i > 0 && result[i - 1][j] != 0
                  && i < result.Count() && result[i + 1][j] != 0
                  && j > 0 && result[i][j - 1] != 0
                  && j < result[i].Count() && result[i][j + 1] > 0))
            {
                if (i == 0 && j == 0)
                {
                    j++;
                }
                else if (j == n - 1)
                    i++;
                else if (i == n - 1 && j == n - 1)
                {
                    j--;
                }
                else if (i == n - 1 && j == 0)
                    i--;

                result[i][j] = num++;
                j++;
                    

            }

            return result.ToArray();
        }
        public static List<string> get_distinct_subsets(string str)
        {
            var result = new List<string>();
            var input = str.ToCharArray();
            Array.Sort(input);

            helper(new string(input), 0, result, new List<char>());

            return result;
        }

        private static void helper(string input, int i, List<string> result, List<char> slate)
        {
            if (i == input.Length)
            {
                var res = slate.FirstOrDefault();
                result.Add(new string(slate.ToArray()));
                return;
            }

            int count = getRepeatedCharsCount(input, i);
            //exclude
            helper(input, i + count, result, slate);
            //include
            for (int j = 1; j <= count; j++)
            {
                slate.Add(input[i]);
                helper(input, i + count, result, slate);
            }

            for (int k = count; k >= 1; k--)
            {
                slate.RemoveAt(slate.Count() - k);
            }
        }

        private static int getRepeatedCharsCount(string input, int index)
        {
            int count = 1;

            for (int i = index; i < input.Length - 1; i++)
            {
                if (input[i] != input[i + 1])
                    break;
                else
                    count++;
            }
            return count;
        }
        public static List<List<int>> generate_all_combinations_unique(List<int> arr, int target)
        {
            var result = new List<List<int>>();

            arr = arr.OrderBy(e => e).ToList();
            Helper_generate_all_combinations(result, arr, target, new List<int>(), 0, 0);

            return result;
        }

        private static void Helper_generate_all_combinations(List<List<int>> result, List<int> arr, int target, List<int> slate, int sum, int idx1)
        {
            if (sum > target)
                return;
            if (sum == target)
            {
                var x = new List<int>();
                x.AddRange(slate);
                result.Add(x);
                return;
            }
            if (idx1 == arr.Count)
                return;

            int count = getRepeatedCharsCount(arr, idx1);
            //exclude
            Helper_generate_all_combinations(result, arr, target, slate, sum, idx1 + count);
            //include
            for (int j = 1; j <= count; j++)
            {
                slate.Add(arr[idx1]);
                sum += arr[idx1];
                Helper_generate_all_combinations(result, arr, target, slate, sum, idx1 + count);
            }

            for (int k = count; k >= 1; k--)
            {
                slate.RemoveAt(slate.Count() - k);
            }
        }

        private static int getRepeatedCharsCount(List<int> input, int index)
        {
            int count = 1;

            for (int i = index; i < input.Count() - 1; i++)
            {
                if (input[i] != input[i + 1])
                    break;
                else
                    count++;
            }
            return count;
        }
        public static bool check_if_sum_possible(long[] arr, long k)
        {
            bool found = false;

            helper(arr, k, ref found, 0, 0);
            return found;
        }

        private static void helper(long[] arr, long k, ref bool found, long sum, int i)
        {
            //(sum > k){
            //    return;
            //}
            if (sum == k && i != 0)
            {
                found = true;
                return;
            }

            if (i == arr.Length)
                return;

            if (!found)
            {
                helper(arr, k, ref found, sum, i + 1);
                sum += arr[i];
                helper(arr, k, ref found, sum, i + 1);
            }
        }
        private static void Swap(List<int> arr, int i, int k)
        {
            int temp = arr[i];
            arr[i] = arr[k];
            arr[k] = temp;
        }
    }
}
