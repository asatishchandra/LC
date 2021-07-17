using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    //public static class Islands
    //{
    //    public static int count_islands(List<List<int>> matrix)
    //    {
    //        var rowCount = matrix.Count();
    //        var colCount = matrix[0].Count();
    //        int count = 0;
    //        for (int i = 0; i < rowCount; i++)
    //        {
    //            var q = new Queue<RowCol>();
    //            for (int j = 0; j < colCount; j++)
    //            {
    //                if (matrix[i][j] != -1 && matrix[i][j] == 1)
    //                {
    //                    q.Enqueue(new RowCol(i, j));
    //                    matrix[i][j] = -1;
    //                    bfs(matrix, q, rowCount, colCount);
    //                }
    //            }
    //            count++;
    //        }
    //        return count;
    //    }

    //    public static void bfs(List<List<int>> matrix, Queue<RowCol> q, int rowCount, int colCount)
    //    {

    //        while (q.Count() > 0)
    //        {
    //            var rc = q.Dequeue();
    //            matrix[rc.row][rc.col] = -1;
    //            getNeighbors(matrix, q, rc, rowCount, colCount);
    //        }

    //    }

    //    public static void getNeighbors(List<List<int>> matrix, Queue<RowCol> q, RowCol rc, int rowCount, int colCount)
    //    {
    //        //up
    //        if (rc.row - 1 > 0 && matrix[rc.row - 1][rc.col] == 1)
    //            q.Enqueue(new RowCol(rc.row - 1, rc.col));

    //        //down
    //        if (rc.row < rowCount && matrix[rc.row + 1][rc.col] == 1)
    //            q.Enqueue(new RowCol(rc.row + 1, rc.col));

    //        //right
    //        if (rc.col - 1 > 0 && matrix[rc.row][rc.col - 1] == 1)
    //            q.Enqueue(new RowCol(rc.row, rc.col - 1));

    //        //left
    //        if (rc.col < colCount && matrix[rc.row][rc.col + 1] == 1)
    //            q.Enqueue(new RowCol(rc.row, rc.col + 1));
    //    }
    //}

    //public class RowCol
    //{
    //    public int row { get; set; }
    //    public int col { get; set; }

    //    public RowCol(int r, int c)
    //    {
    //        this.row = r;
    //        this.col = c;
    //    }
    //}
}
