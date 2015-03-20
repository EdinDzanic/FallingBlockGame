using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FallingBlockGame
{
    public class Field
    {
        private int[][] grid;
        public int[][] Grid { get { return grid; } }

        public Field(int rows, int cols)
        {
            grid = new int[rows][];

            for (int rowIndex = 0; rowIndex < grid.Length; rowIndex++)
            {
                grid[rowIndex] = new int[cols];
            }
        }
    }
}
