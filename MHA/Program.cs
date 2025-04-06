using System.IO;
using System.Linq;

namespace HistorianHysteria
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log("Start Challenge");

            Console.WriteLine("Choose a challenge to solve:");
            Console.WriteLine("1 - Day 1: Historian Hysteria");
            Console.WriteLine("2 - Day 2: Red-Nosed Reports");
            Console.Write("Enter 1 or 2: ");

            string choice = Console.ReadLine() ?? string.Empty;

            try
            {
                if (choice == "1")
                {
                    StartHistorianHysteria();
                }
                else if (choice == "2")
                {
                    StartRedNosedReport();
                }
                else
                {
                    throw new InvalidOperationException("Invalid option selected.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred. Please check the log for details.");
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
                    Console.WriteLine("Number Entered Must Be Of The Same Lenght For Both Left And Right List");
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
    }
    static class Logger
    {
        private static readonly string logFile = "historian_log.txt";

        public static void Log(string message)
        {
            string entry = $"[{DateTime.Now}] {message}";

            File.AppendAllText(logFile, entry + Environment.NewLine);
        }
    }
}