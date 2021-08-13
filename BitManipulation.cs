using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public static class BitManipulation
    {
        public static  int GetSum(int a, int b)
        {
            while (b != 0)
            {
                int answer = a ^ b;
                int carry = (a & b) << 1;
                a = answer;
                b = carry;
            }

            return a;
        }

        public static int LargestIsland(int[][] grid)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            int m = grid.Length;
            int n = grid[0].Length;

            int groupId = 2; //gotcha, can't have 0 or 1
            var map = new Dictionary<int, int>();

            var max = 0;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        int currCount = 0;
                        dfs(grid, i, j, ref currCount, groupId);
                        map.Add(groupId, currCount);
                        //map.Add(groupId, helper(grid, i, j, groupId));                    
                        max = Math.Max(max, map[groupId]); //gtocha  
                        groupId++;
                    }
                }

            if (max == 0)
                return 1;

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    var set = new HashSet<int>();
                    if (grid[i][j] == 0)
                    {
                        for(int k = 0; k < 4; k++)
                        {

                            var row = i + dx[k];
                            var col = j + dy[k];

                            if(row >= 0 && row < grid.Length && col >= 0 && col < grid[0].Length && grid[row][col] > 1 && !set.Contains(grid[row][col])) //gothca
                                set.Add(grid[row][col]);
                        }
                    }
                    var curMax = 0;
                    foreach (var id in set)
                    {
                        curMax += map[id];
                    }
                    max = Math.Max(max, 1 + curMax);
                }

            return max;
        }

        private static void dfs(int[][] grid, int row, int col, ref int currCount, int groupId)
        {
            
            int rows = grid.Length;
            int cols = grid[0].Length;

            if (row >= 0 && row < rows && col >= 0 && col < cols && grid[row][col] == 1)
            {
                currCount++;
                grid[row][col] = groupId;
                dfs(grid, row + 1, col, ref currCount, groupId);
                dfs(grid, row - 1, col, ref currCount, groupId);
                dfs(grid, row, col + 1, ref currCount, groupId);
                dfs(grid, row, col - 1, ref currCount, groupId);
            }
        }

        private static void dfs(int[][] grid, int row, int col, HashSet<int> x)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;

            if (row >= 0 && row < rows && col >= 0 && col < cols && !x.Contains(grid[row][col]))
            {
                if (grid[row][col] > 1)
                    x.Add(grid[row][col]);

                dfs(grid, row + 1, col, x);
                dfs(grid, row - 1, col, x);
                dfs(grid, row, col + 1, x);
                dfs(grid, row, col - 1, x);

                
            }
            else
                return;
        }

        //private int helper(int[][] grid, int r, int c, int groupId)
        //{
        //    int area = 0;
        //    grid[r][c] = groupId;

        //    for (int i = 0; i < 4; i++)
        //    {
        //        var row = r + dx[i];
        //        var col = c + dy[i];

        //        if (row >= 0 && row < grid.Length && col >= 0 && col < grid[0].Length && grid[row][col] == 1)
        //            area += helper(grid, row, col, groupId);
        //    }
        //    return 1 + area;
        //}
    }
}
