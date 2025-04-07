using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HistorianHysteria
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Start Challenge");

            Console.WriteLine("Choose a challenge to solve:");
            Console.WriteLine("1 - Day 1: Challenge ");
            Console.WriteLine("2 - Day 2: Challenge ");
            Console.WriteLine("3 - Day 3: Challenge ");
            Console.WriteLine("4 - Day 4: Challenge ");
            Console.Write("Enter 1,2,3 or 4: ");

            string choice = Console.ReadLine() ?? string.Empty;


            try
            {
                if(choice == "1")
                {
                    StartHistorianHysteria();
                }
                else if(choice == "2")
                {
                    StartRedNosedReport();
                }
                else if(choice == "3")
                {
                    StartMull();
                }
                else if(choice == "4")
                {
                    Console.WriteLine("Counting All XMAS Occurrences...\n");

                    string[] grid = File.ReadAllLines("xmas.txt"); //Note: Load Grid Data From A Text File
                    int count = CountXMASOccurrences(grid);

                    Console.WriteLine($"\nTotal Occurrences Of 'XMAS': {count}");
                    Logger.Log("XMAS Search Completed Successfully.");
                }
                else
                {
                    throw new InvalidOperationException("Invalid Option Selected.");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurred. Please Check The Log For Details.");
                Logger.Log($"Error: {ex.Message}");
            }

            Logger.Log("End Challenge");
        }
        static void StartHistorianHysteria()
        {
            Console.WriteLine("---- Historian Hysteria Day -------");

            string input = Console.ReadLine() ?? string.Empty;

            List<int> leftList = GetListInput("Enter Left Numbers. Separated Numbers With ',': ");

            List<int> rightList = GetListInput("Enter Right Numbers. Separated Numbers With ',': ");

            try
            {
                if (leftList.Count != rightList.Count)
                {
                    Console.WriteLine("Number Entered Must Be Of The Same Lengh For Both Left And Right List");
                }
                else
                {
                    int totalDistance = CalculateTotalDistance(leftList, rightList);

                    Console.WriteLine($"Total Distance Calculated: {totalDistance}");

                    Logger.Log($"[Day 1] Total Distance Calculated: {totalDistance}");
                }
            }
            catch
            {
                Logger.Log($"[Day 1] Invalid Report Skipped: {input}");
                Console.WriteLine("Invalid Input. Skipping...");
            }
            
        }
        static void StartRedNosedReport()
        {
            Console.WriteLine("\n----- Red-Nosed Reports Day  ------");
            Console.WriteLine("Enter Each Report As Space-Separated Numbers (Press Enter On Empty Line To Finish):");

            List<List<int>> reports = new List<List<int>>();

            while (true)
            {
                string input = Console.ReadLine()?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                try
                {
                    List<int> report = input.Trim().Split(' ').Select(int.Parse).ToList();
                    reports.Add(report);
                }
                catch
                {
                    Logger.Log($"[Day 2] Invalid Report Skipped: {input}");
                    Console.WriteLine("Invalid Input. Skipping...");
                }
            }

            int safeCount = reports.Count(SafeReport);

            Console.WriteLine($"Number Of Safe Reports: {safeCount}");
            Logger.Log($"[Day 2] Safe Reports Count: {safeCount}");
        }
        static void StartMull()
        {
            Console.WriteLine("\n--- Mull It Over Day ---");
            Console.WriteLine("Enter the corrupted memory string:");

            string input = Console.ReadLine()?? string.Empty;

            int sum = 0;
            Regex pattern = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");

            foreach (Match match in pattern.Matches(input))
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                sum += x * y;
            }

            Console.WriteLine($"Total sum of valid mul results: {sum}");
            Logger.Log($"[Day 3] Sum of mul results: {sum}");
        }
        static List<int> GetListInput(string promptInput)
        {
            Console.Write(promptInput);

            string input = Console.ReadLine()?? string.Empty;

            try
            {
                List<int> numberInput = input.Split(',').Select(x => int.Parse(x.Trim())).ToList();

                return numberInput;
            }
            catch (Exception ex)
            {
                Logger.Log($"Invalid input: {input}. Error: {ex.Message}");
                throw new FormatException("Input must be a list of comma-separated integers.");
            }
        }
        static int CalculateTotalDistance(List<int> left, List<int> right)
        {
            left.Sort();
            right.Sort();

            int total = 0;

            for (int i = 0; i < left.Count; i++)
            {
                total += Math.Abs(left[i] - right[i]);
            }

            return total;
        }
        static bool SafeReport(List<int> levels)
        {
            Console.WriteLine("---- Safe Report Day -------");

            if (levels.Count < 2)
            {
                return false;
            }

            bool increasing = levels[1] > levels[0];

            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i - 1];

                if (diff == 0 || Math.Abs(diff) > 3)
                {
                    return false;
                }

                if ((levels[i] > levels[i - 1]) != increasing)
                {
                    return false;
                }
            }

            return true;
        }
        static int CountXMASOccurrences(string[] grid)
        {
            Console.WriteLine("---- Find XMAS Day -------");

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

        static bool IsXMAS(string[] grid, int r, int c, int dr, int dc)
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
    static class Logger
    {
        private static readonly string logFile = "historian_log.txt"; // Logs Errors And Outputs Status To Historian Text File 

        public static void Log(string message)
        {
            string entry = $"[{DateTime.Now}] {message}";

            File.AppendAllText(logFile, entry + Environment.NewLine);
        }
    }
}