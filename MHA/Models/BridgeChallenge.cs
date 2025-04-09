using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class BridgeChallenge : IServiceChallenge
    {
        public void Run()
        {
            string[] lines = File.ReadAllLines("bridge.txt"); //Note: Load Bridge Data From A Text File Stored On ".bin" Folder 

            try
            {
                long totalCalibrationResult = 0;

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var parts = line.Split(':');

                    int target = int.Parse(parts[0].Trim());

                    var numbers = Array.ConvertAll(parts[1].Trim().Split(' '), int.Parse);

                    if (ReachTarget(numbers, target))
                    {
                        totalCalibrationResult += target;
                    }
                }

                Console.WriteLine($"Total calibration result: {totalCalibrationResult}");

                Logger.Log($"Total calibration result: {totalCalibrationResult}");
            }
            catch 
            {
                Logger.Log($"Corrupted Bridge File: {lines}");

                Console.WriteLine("Corrupted Bridge File...");
            }
            
        }

        public bool ReachTarget(int[] numbers, int target)
        {
            return CalculatingCombinations(numbers, 1, numbers[0], target);
        }

        public bool CalculatingCombinations(int[] nums, int index, long currentValue, int target)
        {
            if (index == nums.Length)
            {
                return currentValue == target;
            }

            // Apply Addition
            if (CalculatingCombinations(nums, index + 1, currentValue + nums[index], target))
            {
                return true;
            }

            //Apply Mulitplication 
            if (CalculatingCombinations(nums, index + 1, currentValue * nums[index], target)) 
            {
                return true;
            }

            return false;
        }
    }
}
