using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class RedNosedChallenge : IServiceChallenge
    {
        public void Run()
        {
            Console.WriteLine("\n----- Red-Nosed Reports  ------");
            Console.WriteLine("Enter Each Report As Space-Separated Numbers (Press Enter On Empty Line To Finish):");
            Console.WriteLine("Example:");
            Console.WriteLine("2 3 9");
            Console.WriteLine("5 8 8");
            Console.WriteLine("3 4 3");
            Console.WriteLine("\n--------------------------");

            List<List<int>> reports = new List<List<int>>();

            while (true)
            {
                string input = Console.ReadLine() ?? string.Empty;

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
                    Logger.Log($"Invalid Report Skipped: {input}");

                    Console.WriteLine("Invalid Input. Skipping...");
                }
            }

            int safeCount = reports.Count(SafeReport);

            Console.WriteLine($"Number Of Safe Reports: {safeCount}");

            Logger.Log($"Safe Reports Count: {safeCount}");
        }
        private bool SafeReport(List<int> levels)
        {
            Console.WriteLine("---- Safe Report -------");

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
}
