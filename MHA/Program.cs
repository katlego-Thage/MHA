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
            bool running = true;

            while (running)
            {
                Console.Clear();
                Logger.Log("Start Challenge");

                Console.WriteLine("Choose a challenge to solve:");
                Console.WriteLine("1 - Day 1: Challenge");
                Console.WriteLine("2 - Day 2: Challenge");
                Console.WriteLine("3 - Day 3: Challenge");
                Console.WriteLine("4 - Day 4: Challenge");
                Console.WriteLine("5 - Day 5: Challenge");
                Console.WriteLine("6 - Day 6: Challenge");
                Console.WriteLine("Q - Quit");
                Console.Write("Enter 1, 2, 3, ..., or Q to quit: ");

                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "q")
                {
                    running = false;
                    Console.WriteLine("Exiting program...");
                    break;
                }

                IServiceChallenge? challenge = input switch
                {
                    "1" => new HistorianChallenge(),
                    "2" => new RedNosedChallenge(),
                    "3" => new MullChallenge(),
                    "4" => new XmasChallenge(),
                    "5" => new PrintQueueChallenge(),
                    "6" => new GuardChallenge(),
                    "7" => new BridgeChallenge(),
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

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}