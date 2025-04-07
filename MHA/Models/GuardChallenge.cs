using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class GuardChallenge : IServiceChallenge
    {
        public void Run()
        {
            var map = File.ReadAllLines("guard.txt"); //Note: Load Guard Data From A Text File Stored On ".bin" Folder 
                                                      //Due To The Size Of Data That is On The File, It Take Longer To Execute. The Data In The File Is Reduced To See / Print Results
            try
            {
                int rows = map.Length;

                int cols = map.Max(line => line.Length); // Ensure handle uneven line lengths

                char[,] grid = new char[rows, cols];

                (int row, int col) position = (-1, -1);

                int direction = -1;

                for (int r = 0; r < rows; r++)
                {
                    var line = map[r];

                    for (int c = 0; c < cols; c++)
                    {
                        char ch = (c < line.Length) ? line[c] : '.'; // Fill Missing Spots With '.'

                        grid[r, c] = ch;

                        if ("^>v<".Contains(ch))
                        {
                            position = (r, c);

                            direction = ch switch
                            {
                                '^' => 0,
                                '>' => 1,
                                'v' => 2,
                                '<' => 3,
                                _ => throw new InvalidOperationException("Invalid direction")
                            };
                            grid[r, c] = '.';
                        }
                    }
                }

                // Movement deltas for Up, Right, Down, Left
                int[] dr = { -1, 0, 1, 0 };

                int[] dc = { 0, 1, 0, -1 };

                HashSet<(int, int)> visited = new HashSet<(int, int)>();

                visited.Add(position);

                while (position.row >= 0 && position.row < rows && position.col >= 0 && position.col < cols)
                {
                    int nextRow = position.row + dr[direction];

                    int nextCol = position.col + dc[direction];

                    if (nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= cols || grid[nextRow, nextCol] == '#')
                    {
                        // Turn right
                        direction = (direction + 1) % 4;
                    }
                    else
                    {
                        // Move forward
                        position = (nextRow, nextCol);

                        visited.Add(position);
                    }
                }

                Console.WriteLine($"Distinct Positions Visited: {visited.Count}");
            }
            catch 
            {
                Logger.Log($"Corrupted Guard File: {map}");

                Console.WriteLine("Corrupted Guard File...");
            }
            
        }
    }
}
