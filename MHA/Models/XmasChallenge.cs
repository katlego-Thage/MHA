using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class XmasChallenge : IServiceChallenge
    {
        public void Run()
        {
            Console.WriteLine("Counting All XMAS Occurrences...\n");

            string[] grid = File.ReadAllLines("xmas.txt");  //Note: Load Grid Data From A Text File Stored On ".bin" Folder 

            int count = CountXMASOccurrences(grid);

            try
            {                
                Console.WriteLine($"\nTotal Occurrences Of 'XMAS': {count}");

                Logger.Log("XMAS Search Completed Successfully.");
            }
            catch
            {
                Logger.Log($"Corrupted Grids File: {grid}");

                Console.WriteLine("Corrupted Grids File...");
            }
        }
        private int CountXMASOccurrences(string[] grid)
        {
            Console.WriteLine("---- Find XMAS -------");

            int rows = grid.Length;
            int cols = grid[0].Length;
            int count = 0;

            int[][] directions = new int[][]
            {
            new int[] { 0, 1 },
            new int[] { 1, 0 },
            new int[] { 1, 1 },
            new int[] { -1, 1 },
            new int[] { 0, -1 },
            new int[] { -1, 0 },
            new int[] { -1, -1 },
            new int[] { 1, -1 },
            };

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    foreach (var dir in directions)
                    {
                        if (IsXMAS(grid, r, c, dir[0], dir[1]))
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
        private bool IsXMAS(string[] grid, int r, int c, int dr, int dc)
        {
            string target = "XMAS";

            int rows = grid.Length;

            int cols = grid[0].Length;

            for (int i = 0; i < target.Length; i++)
            {
                int nr = r + i * dr;

                int nc = c + i * dc;

                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || grid[nr][nc] != target[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
