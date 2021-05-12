﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace P05.ConnectedAreasInMtrx
{
    public class Area
    {
        public int Size { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var matrix = ReadMatrix(rows, cols);
            var visited = new bool[rows, cols];

            var areas = new List<Area>();

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == '*')
                    {
                        continue;
                    }

                    if (visited[r, c])
                    {
                        continue;
                    }


                    var areaSize = GetAreaSize(matrix, r, c, visited);

                    var area = new Area { Row = r, Col = c, Size = areaSize };
                    areas.Add(area);
                }
            }

           var sorted = areas
                .OrderByDescending(a => a.Size)
                .ThenBy(a => a.Row)
                .ThenBy(a => a.Col)
                .ToList();

            Console.WriteLine($"Total areas found: {sorted.Count}");

            for (int i = 0; i < sorted.Count; i++)
            {
                var area = sorted[i];
                Console.WriteLine($"Area #{i + 1} at ({area.Row}, {area.Col}), size: {area.Size}");
            } 
        }

        private static int GetAreaSize(char[,] matrix, int row, int col, bool[,] visited)
        {
            if (IsOutside(matrix, row, col))
            {
                return 0;
            }

            if (visited[row, col])
            {
                return 0;
            }

            if (matrix[row, col] == '*')
            {
                return 0;
            }

            visited[row, col] = true;

            var areaSize = 1 + GetAreaSize(matrix, row - 1, col, visited) +
                GetAreaSize(matrix, row + 1, col, visited) + GetAreaSize(matrix, row, col - 1, visited) + GetAreaSize(matrix, row, col + 1, visited);

            return areaSize;
        }

        private static bool IsOutside(char[,] matrix, int row, int col)
        {
            return row < 0 ||
                col < 0 ||
                row >= matrix.GetLength(0) ||
                col >= matrix.GetLength(1);
        }

        private static char[,] ReadMatrix(int rows, int cols)
        {
            var matrix = new char[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                var colElements = Console.ReadLine();

                for (int c = 0; c < colElements.Length; c++)
                {
                    matrix[r, c] = colElements[c];
                }
            }

            return matrix;
        }
    }
}
