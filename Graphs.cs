using System;
using System.Collections.Generic;
using System.Linq;

namespace LC
{
    public static class Graphs
    {
        public static bool[] visited;
        public static int[] parent;
        public static bool[] color;
        public static Dictionary<int, List<int>> adjMap;
        public static bool isBipartite;
        public static bool can_be_divided(int num_of_people, List<int> dislike1, List<int> dislike2)
        {

            isBipartite = true;
            visited = new bool[num_of_people];
            parent = new int[num_of_people];
            color = new bool[num_of_people];
            initAdjMap(dislike1, dislike2);

            for (int i = 0; i < num_of_people; i++)
            {
                if (!isBipartite)
                    return false;

                if (!visited[i] && adjMap.ContainsKey(i))
                {
                    color[i] = false;
                    dfs_isBipartite(i);
                }
            }

            return isBipartite;
        }

        public static void dfs_isBipartite(int node)
        {
            visited[node] = true;
            var edges = adjMap[node];

            if (!isBipartite)
                return;

            foreach (var edge in edges)
            {
                if (!visited[edge])
                {
                    visited[node] = true;
                    parent[edge] = node;
                    color[edge] = !color[node];
                    dfs_isBipartite(edge);
                }
                else
                {
                    if (parent[node] != edge && color[edge] == color[node])
                        isBipartite = false;
                }
            }
            return;
        }

        public static void initAdjMap(List<int> edge_start, List<int> edge_end)
        {
            adjMap = new Dictionary<int, List<int>>();

            for (int i = 0; i < edge_start.Count(); i++)
            {
                if (!adjMap.ContainsKey(edge_start[i]))
                    adjMap.Add(edge_start[i], new List<int> { edge_end[i] });
                else
                    adjMap[edge_start[i]].Add(edge_end[i]);

                if (!adjMap.ContainsKey(edge_end[i]))
                    adjMap.Add(edge_end[i], new List<int> { edge_start[i] });
                else
                    adjMap[edge_end[i]].Add(edge_start[i]);
            }
        }
        public static bool can_be_completed(int n, List<int> a, List<int> b)
        {
            bool isValid = true;
            bool[] visited = new bool[n];
            bool[] departed = new bool[n];
            Dictionary<int, List<int>> adjMap = initDirectedAdjMap(a, b);

            for (int i = 0; i < n; i++)
            {
                if (!visited[i])
                {
                    visited[i] = true;
                    dfs(adjMap, visited, departed, i, ref isValid);
                }
                if (!isValid)
                    return false;
            }
            return isValid;
        }

        public static void dfs(Dictionary<int, List<int>> adjMap, bool[] visited, bool[] departed, int node, ref bool isValid)
        {
            visited[node] = true;

            if (!adjMap.ContainsKey(node))
            {
                departed[node] = true;
                return;
            }
            var edges = adjMap[node];

            foreach (var edge in edges)
            {
                if (!visited[edge])
                {
                    dfs(adjMap, visited, departed, edge, ref isValid);
                }
                else
                {
                    if (!departed[edge])
                        isValid = false;
                }
            }
            departed[node] = true;
            return;
        }

        public static Dictionary<int, List<int>> initDirectedAdjMap(List<int> edge_start, List<int> edge_end)
        {
            var adjMap = new Dictionary<int, List<int>>();
            for (int i = 0; i < edge_start.Count(); i++)
            {
                if (!adjMap.ContainsKey(edge_start[i]))
                    adjMap.Add(edge_start[i], new List<int> { edge_end[i] });
                else
                    adjMap[edge_start[i]].Add(edge_end[i]);
            }
            return adjMap;
        }
        public static bool can_be_completed_kahns(int n, List<int> a, List<int> b)
        {
            int[] inDegree = new int[n];
            List<int> topSort = new List<int>();
            bool[] visited = new bool[n];
            bool[] departed = new bool[n];
            Queue<int> q = new Queue<int>();
            Dictionary<int, List<int>> adjMap = initDirectedAdjMap(a, b, inDegree);

            int index = 0;
            foreach (var degree in inDegree)
            {
                if (degree == 0)
                {
                    q.Enqueue(index);
                }
                index++;
            }
            if (q.Count() == 0)
                return false;

            while (q.Count() > 0)
            {
                var curr = q.Dequeue();
                topSort.Add(curr);
                if (adjMap.ContainsKey(curr))
                {
                    foreach (var edge in adjMap[curr])
                    {
                        inDegree[edge]--;
                        if (inDegree[edge] == 0)
                            q.Enqueue(edge);
                    }
                }
            }

            return topSort.Count() < n;

        }

        public static Dictionary<int, List<int>> initDirectedAdjMap(List<int> edge_start, List<int> edge_end, int[] inDegree)
        {
            var adjMap = new Dictionary<int, List<int>>();
            for (int i = 0; i < edge_start.Count(); i++)
            {
                if (!adjMap.ContainsKey(edge_start[i]))
                    adjMap.Add(edge_start[i], new List<int> { edge_end[i] });
                else
                    adjMap[edge_start[i]].Add(edge_end[i]);

                inDegree[edge_end[i]]++;
            }
            return adjMap;
        }
        public static int count = 0;
        public static List<int> RiverSizes(int[,] matrix)
        {
            // Write your code here.
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            if (rows == 1 && cols == 1)
            {
                return matrix[0, 0] == 1 ? new List<int> { 1 } : new List<int>();
            }

            List<int> result = new List<int>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] == 1)
                    {
                        count = 0;
                        dfs(matrix, rows, cols, row, col, result);
                        result.Add(count);
                    }
                        
                }
            }

            return result;
        }

        private static void dfs(int[,] matrix, int rows, int cols, int row, int col, List<int> result)
        {
            if (row < 0 || col < 0 || row >= rows || col >= cols || matrix[row, col] == 0)
                return;

            count++;
            matrix[row, col] = 0;
            dfs(matrix, rows, cols, row, col - 1, result);
            dfs(matrix, rows, cols, row - 1, col, result);
            dfs(matrix, rows, cols, row, col + 1, result);
            dfs(matrix, rows, cols, row + 1, col, result);
            
            return;
        }

        public static bool HasSingleCycle(int[] array)
        {
            // Write your code here.
            int visited = 0;
            int idx = 0;

            while (visited < array.Length)
            {
                if (visited > 0 && idx == 0)
                    return false;
                visited++;
                idx = nxtIdx(array, idx);
            }
            return idx == 0;
        }

        public static int nxtIdx(int[] array, int idx)
        {
            int jump = array[idx];
            int nextIdx = (idx + jump) % array.Length;

            return nextIdx >= 0 ? nextIdx : nextIdx + array.Length;

        }
        //public bool[] visited;
        //public int[] parent;
        public static bool CycleInUndirectedGraph(int[][] edges)
        {
            int vertices = edges.Length;

            visited = new bool[vertices];
            Array.Fill(visited, false);

            parent = new int[vertices];
            Array.Fill(parent, -1);

            

            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < edges[i].Length; j++)
                {
                    if (!visited[j])
                    {
                        if (HasCyclesUndirected(edges, j))
                            return true;
                    }
                }
            }
            return false;
        }

        public static bool HasCyclesUndirected(int[][] edges, int vertex)
        {
            visited[vertex] = true;

            int[] edgeList = edges[vertex];
            foreach (int neighbor in edgeList)
            {
                if (!visited[neighbor])
                {
                    parent[neighbor] = vertex;
                    if (HasCyclesUndirected(edges, neighbor))
                        return true;
                }
                else
                {
                    if (neighbor != parent[vertex])
                        return true;
                }
            }
            return false;
        }

        public static bool[,] visited2d;
        public static int[,] matrixCopy;
        public static int MinimumPassesOfMatrix(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            matrixCopy = new int[rows, cols];
            visited2d = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrixCopy[i, j] = matrix[i][j];
                    if (matrix[i][j] == 0)
                        visited2d[i, j] = true;
                }
            }
            
            int row = 0;
            int col = 0;
            bool stop = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (!visited2d[i, j] && matrix[i][j] > 0)
                    {
                        row = i;
                        col = j;
                        stop = true;
                        break;
                    }
                }
                if (stop)
                    break;
            }
            int count = bfs(matrix, rows, cols, row, col);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrixCopy[i, j] < 0)
                        return -1;
                }
            }
            return count;
        }

        public static int bfs(int[][] matrix, int rows, int cols, int row, int col)
        {
            int count = 0;
            Queue<RowCol> q1 = new Queue<RowCol>();
            Queue<RowCol> q2 = new Queue<RowCol>();
            visited2d[row, col] = true;
            q1.Enqueue(new RowCol(row, col));

            while (q1.Count > 0)
            {
                RowCol curr = q1.Dequeue();
                visited2d[curr.Row, curr.Col] = true;
                findValidNeighbors(matrix, q1, q2, rows, cols, curr.Row, curr.Col);
                if (q1.Count == 0 && q2.Count > 0)
                {
                    count++;
                    while (q2.Count > 0)
                    {
                        RowCol rc = q2.Dequeue();
                        q1.Enqueue(new RowCol(rc.Row, rc.Col));
                    }
                }
            }
            return count;
        }

        public static void findValidNeighbors(int[][] matrix, Queue<RowCol> q1, Queue<RowCol> q2, int rows, int cols, int row, int col)
        {
            if (col - 1 >= 0 && !visited2d[row, col - 1])
            {
                if (matrix[row][col - 1] < 0 && matrixCopy[row, col - 1] == matrix[row][col - 1])
                {
                    matrixCopy[row, col - 1] = matrix[row][col - 1] * -1;
                    q2.Enqueue(new RowCol(row, col - 1));
                }
                else if (matrix[row][col - 1] > 0)
                {
                    visited2d[row, col - 1] = true;
                    q1.Enqueue(new RowCol(row, col - 1));
                }
            }
            if (row - 1 >= 0 && !visited2d[row - 1, col])
            {
                if (matrix[row - 1][col] < 0 && matrixCopy[row - 1, col] == matrix[row - 1][col])
                {
                    matrixCopy[row - 1, col] = matrix[row - 1][col] * -1;
                    q2.Enqueue(new RowCol(row - 1, col));
                }
                else if (matrix[row - 1][col] > 0)
                {
                    visited2d[row - 1, col] = true;
                    q1.Enqueue(new RowCol(row - 1, col));
                }
            }
            if (col + 1 < cols && !visited2d[row, col + 1])
            {
                if (matrix[row][col + 1] < 0 && matrixCopy[row, col + 1] == matrix[row][col + 1])
                {
                    matrixCopy[row, col + 1] = matrix[row][col + 1] * -1;
                    q2.Enqueue(new RowCol(row, col + 1));
                }
                else if (matrix[row][col + 1] > 0)
                {
                    visited2d[row, col + 1] = true;
                    q1.Enqueue(new RowCol(row, col + 1));
                }
            }
            if (row + 1 < rows && !visited2d[row + 1, col])
            {
                if (matrix[row + 1][col] < 0 && matrixCopy[row + 1, col] == matrix[row + 1][col])
                {
                    matrixCopy[row + 1, col] = matrix[row + 1][col] * -1;
                    q2.Enqueue(new RowCol(row + 1, col));
                }
                else if (matrix[row + 1][col] > 0)
                {
                    visited2d[row + 1, col] = true;
                    q1.Enqueue(new RowCol(row + 1, col));
                }
            }
            return;
        }
        public static int NumIslands(char[][] grid)
        {
            if (grid == null)
                return -1;

            int rows = grid.Length;
            int cols = grid[0].Length;
            int count = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; cols++)
                {
                    if (grid[row][col] == '1')
                    {
                        count++;
                        dfs(grid, rows, cols, row, col);
                    }
                }
            }
            return count;
        }

        public static void dfs(char[][] grid, int rows, int cols, int row, int col)
        {
            if (col < 0 || row < 0 || col >= cols || row  >= rows || grid[row][col] == '0')
                return;

            grid[row][col] = '0';
            dfs(grid, rows, cols, row, col - 1);
            dfs(grid, rows, cols, row - 1, col);
            dfs(grid, rows, cols, row, col + 1);
            dfs(grid, rows, cols, row + 1, col);

            return;
        }
    }


    public static class Islands
    {
        public static int count_islands(List<List<int>> matrix)
        {
            var rowCount = matrix.Count();
            var colCount = matrix[0].Count();
            int count = 0;
            for (int i = 0; i < rowCount; i++)
            {
                var q = new Queue<RowCol>();
                for (int j = 0; j < colCount; j++)
                {
                    if (matrix[i][j] != -1 && matrix[i][j] == 1)
                    {
                        q.Enqueue(new RowCol(i, j));
                        matrix[i][j] = -1;
                        bfs(matrix, q, rowCount, colCount);
                        count++;
                    }
                }
               
            }
            return count;
        }

        public static void bfs(List<List<int>> matrix, Queue<RowCol> q, int rowCount, int colCount)
        {

            while (q.Count() > 0)
            {
                var rc = q.Dequeue();
                getNeighbors(matrix, q, rc, rowCount, colCount);
            }

        }

        public static void getNeighbors(List<List<int>> matrix, Queue<RowCol> q, RowCol rc, int rowCount, int colCount)
        {
            RowCol rowCol;

            //up
            if (rc.Row - 1 >= 0 && matrix[rc.Row - 1][rc.Col] == 1)
            {
                rowCol = new RowCol(rc.Row - 1, rc.Col);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
                
            //down
            if (rc.Row + 1 < rowCount && matrix[rc.Row + 1][rc.Col] == 1)
            {
                rowCol = new RowCol(rc.Row + 1, rc.Col);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            //left
            if (rc.Col - 1 >= 0 && matrix[rc.Row][rc.Col - 1] == 1)
            {
                rowCol = new RowCol(rc.Row, rc.Col - 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            //right
            if (rc.Col + 1 < colCount && matrix[rc.Row][rc.Col + 1] == 1)
            {
                rowCol = new RowCol(rc.Row, rc.Col + 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            // up left
            if (rc.Row - 1 >= 0 && rc.Col - 1 >= 0 && matrix[rc.Row - 1][rc.Col - 1] == 1)
            {
                rowCol = new RowCol(rc.Row - 1, rc.Col - 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            //up right   
            if (rc.Row - 1 >= 0 && rc.Col + 1 < colCount && matrix[rc.Row - 1][rc.Col + 1] == 1)
            {
                rowCol = new RowCol(rc.Row - 1, rc.Col + 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            //bottom left  
            if (rc.Row + 1 < rowCount && rc.Col - 1 >= 0 && matrix[rc.Row + 1][rc.Col - 1] == 1)
            {
                rowCol = new RowCol(rc.Row + 1, rc.Col - 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
            
            //bottom - right   
            if (rc.Row + 1 < rowCount && rc.Col + 1 < colCount && matrix[rc.Row + 1][rc.Col + 1] == 1)
            {
                rowCol = new RowCol(rc.Row + 1, rc.Col + 1);
                matrix[rowCol.Row][rowCol.Col] = -1;
                q.Enqueue(rowCol);
            }
        }

        
    }

    public class RowCol
    {
        public int Row;
        public int Col;
        public RowCol RC;


        public RowCol(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public RowCol(RowCol rowCol)
        {
            this.RC = rowCol;
        }
    }

}
