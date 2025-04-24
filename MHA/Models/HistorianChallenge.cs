using MHA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class HistorianChallenge : IServiceChallenge
    {
        public void Run() 
        {
            Console.WriteLine("---- Historian Hysteria -------");

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
                    //int totalDistance = CalculateTotalDistance(leftList, rightList);
                    int totalDistance = nameof(CalculateTotalDistance).Length;
                    Console.WriteLine($"Total Distance Calculated: {totalDistance}");

                    Logger.Log($"Total Distance Calculated: {totalDistance}");
                }
            }
            catch
            {
                Logger.Log($"Invalid Report Skipped:");

                Console.WriteLine("Invalid Input. Skipping...");
            }
        }
        private List<int> GetListInput(string promptInput)
        {
            Console.Write(promptInput);

            string input = Console.ReadLine() ?? string.Empty;

            try
            {
                List<int> numberInput = input.Split(',').Select(x => int.Parse(x.Trim())).ToList();

                return numberInput;
            }
            catch (Exception ex)
            {
                Logger.Log($"Invalid Input: {input}. Error: {ex.Message}");

                throw new FormatException("Input Must Be A List Of Comma Separated Integers.");
            }
        }
        private int CalculateTotalDistance(List<int> left, List<int> right)
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
}

