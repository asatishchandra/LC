using System;
using System.Collections.Generic;
using System.Text;

namespace LC
{
    public static class Strings
    {
        public static int LengthOfLongestSubstring(string s)
        {
            if (s == null || s.Length == 0)
                return 0;

            HashSet<char> set = new HashSet<char>();
            int [] indices = new int[2];
            int start = 0;
            int subStrStart = 0;
            int maxLen = int.MinValue;
            while (start <= s.Length - 1)
            {
                if (!set.Contains(s[start]))
                    set.Add(s[start]);
                else
                {
                    var diff = start - subStrStart;
                    if (diff > maxLen)
                    {
                        indices[0] = subStrStart;
                        indices[1] = start;
                    }
                    set.Clear();
                    set.Add(s[start]);
                    subStrStart = start;
                }
                start++;
            }
            return indices[1] - indices[0];
        }
        public static string rotationalCipher(String input, int rotationFactor)
        {
            // Write your code here
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(Convert.ToChar(((c - '0' + rotationFactor) % 10) + '0'));
                }
                else if (char.IsUpper(c))
                {
                    sb.Append(Convert.ToChar(((c - 'A' + rotationFactor) % 26) + 'A'));
                }
                else if (char.IsLower(c))
                {
                    sb.Append(Convert.ToChar(((c - 'a' + rotationFactor) % 26) + 'a'));
                }
                else
                {
                    sb.Append(c);
                }
            }
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        public static int numberOfWays(int[] arr, int k)
        {
            // Write your code here
            int count = 0;
            var dict = new Dictionary<int, int>();
            foreach (var ele in arr)
            {
                if (!dict.ContainsKey(ele))
                    dict.Add(ele, 1);
                else
                    dict[ele]++;
            }

            //var set = new HashSet<int>();

            foreach (var ele in dict.Keys)
            {
                var diff = k - ele;
                if (dict.ContainsKey(diff) && dict[ele] > 0 && dict[diff] > 0)
                {
                    count += dict[diff];
                    dict[ele]--;
                    dict[diff]--;
                }
            }
            Console.WriteLine(count);
            return count;
        }
        public static string decodeString(int numberOfRows, string encodedString)
        {
            int numberOfCols = GetNumberOfCols(encodedString); 
            char[,] stringGrid = ConstructGrid(numberOfRows, numberOfCols, encodedString);

            char[] result = new char[encodedString.Length];
            int idx = 0;
            int j = 0;
            while (true)
            {
                for(int i = 0; i < numberOfRows && i+j < numberOfCols; i++)
                {
                    result[idx] = stringGrid[i, i+j];
                    Console.Write("{0}\t", stringGrid[i, i+j]);
                    idx++;
                }
                Console.WriteLine();
                j++;
                if (j > numberOfCols)
                    break;
            }
            Console.WriteLine(new string(result));
            return new string(result);
        }

        private static int GetNumberOfCols(string encodedString)
        {
            string[] encodedSplit = encodedString.Split('_');
            if (encodedSplit.Length == 1)
                return encodedSplit[0].Length;
            else
            {
                int spaceCount = 0;
                for(int i = 1; i < encodedSplit.Length; i++)
                {
                    if (encodedSplit[i] != string.Empty)
                        break;

                    spaceCount++;
                }
                return encodedSplit[0].Length + spaceCount;
            }
        }
        private static char[,] ConstructGrid(int numberOfRows, int numberOfCols, string encodedString)
        {
            char[,] stringGrid = new char[numberOfRows, numberOfCols];
            int encodedIdx = 0;

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfCols; col++)
                {
                    stringGrid[row, col] = encodedString[encodedIdx].Equals('_') ? ' ' : encodedString[encodedIdx];
                    encodedIdx++;
                }
            }
            return stringGrid;
        }
    }
}
