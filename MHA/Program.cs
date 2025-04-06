using System.IO;
using System.Linq;

namespace HistorianHysteria
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("---- Day One -------");
                Logger.Log("Start Historian Hysteria");

                List<int> leftList = GetListInput("Enter Left Numbers: ");

                List<int> rightList = GetListInput("Enter Right Numbers: ");

                if (leftList.Count != rightList.Count)
                {
                    Console.WriteLine("Number entered must be of the same lenght for both left and right list");
                }
                else 
                {
                    int totalDistance = CalculateTotalDistance(leftList, rightList);

                    Console.WriteLine(totalDistance);

                    Logger.Log($"Total distance calculated: {totalDistance}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred. Please check the log for details.");

                Logger.Log($"Error: {ex.Message}");
            }

            Logger.Log("End Historian Hysteria");
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