using MHA;
using MHA.Models;
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
            Console.WriteLine("5 - Day 5: Challenge ");
            Console.WriteLine("6 - Day 6: Challenge ");
            Console.Write("Enter 1,2,3,4..... ");

            IServiceChallenge? challenge = Console.ReadLine()?.Trim() switch
                {
                    "1" => new HistorianChallenge(),
                    "2" => new RedNosedChallenge(),
                    "3" => new MullChallenge(),
                    "4" => new XmasChallenge(),
                    "5" => new PrintQueueChallenge(),
                    "6" => new GuardChallenge(),
                    _ => null
                };

                try
                {
                    if (challenge == null)
                    {
                        throw new InvalidOperationException("Invalid Option Selected.");
                    }

                    challenge.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An Error Occurred. Please Check The Log For Details.");
                    Logger.Log($"Error: {ex.Message}");
                }

                Logger.Log("End Challenge");
        }
    }
}