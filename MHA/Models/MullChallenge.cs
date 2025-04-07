using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class MullChallenge : IServiceChallenge
    {
        public void Run()
        {
            Console.WriteLine("\n--- Mull It Over ---");

            Console.WriteLine("Enter The Corrupted Memory String:");

            string input = Console.ReadLine() ?? string.Empty;

            try
            {
                int sum = 0;

                Regex pattern = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");

                foreach (Match match in pattern.Matches(input))
                {
                    int x = int.Parse(match.Groups[1].Value);

                    int y = int.Parse(match.Groups[2].Value);

                    sum += x * y;
                }

                Console.WriteLine($"Total Sum Of Valid Mull Results: {sum}");

                Logger.Log($"Sum Of Mull Results: {sum}");
            }
            catch (Exception ex)
            {
                Logger.Log($"Invalid Input: {input}. Error: {ex.Message}");

                throw new FormatException("Incorrect Mull.");
            }
        }
    }        
}
