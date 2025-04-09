using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Models
{
    public class PrintQueueChallenge : IServiceChallenge
    {
        public void Run()
        {
            var inputLines = File.ReadAllLines("input.txt"); //Note: Load Input Data From A Text File Stored On ".bin" Folder 

            var input = string.Join("\n", inputLines);

            var sections = input.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var rules = sections[0].Split('\n').Select(r => r.Trim()).Where(r => r.Contains('|')).Select(r =>
                {
                    var parts = r.Split('|');

                    if (parts.Length != 2)
                    {
                        return (-1, -1);
                    }

                    return (From: int.Parse(parts[0]), To: int.Parse(parts[1]));

                }).Where(rule => rule.Item1 != -1 && rule.Item2 != -1).ToList();

                var updates = sections[1].Split('\n').Select(u => u.Split(',').Select(int.Parse).ToList()).ToList();

                var validMiddles = new List<int>();

                foreach (var update in updates)
                {
                    bool isValid = true;

                    foreach (var rule in rules)
                    {
                        if (update.Contains(rule.Item1) && update.Contains(rule.Item2))
                        {
                            int indexFrom = update.IndexOf(rule.Item1);

                            int indexTo = update.IndexOf(rule.Item2);

                            if (indexFrom >= indexTo)
                            {
                                isValid = false;

                                break;
                            }
                        }
                    }

                    if (isValid)
                    {
                        int middle = update[update.Count / 2];

                        validMiddles.Add(middle);
                    }
                }

                Console.WriteLine($"Sum Of Middle Values From Valid Updates: {validMiddles.Sum()}");

                Logger.Log($"Sum Of Middle Values From Valid Updates: {validMiddles.Sum()}");
            }
            catch(Exception)
            {
                Logger.Log($"Corrupted Input File: {inputLines}");

                Console.WriteLine("Corrupted Input File...");
            }
           
        }
    }
}
