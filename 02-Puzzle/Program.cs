using System;
namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] InitialData = new string[] {
                                                    "01111100000000000000",
                                                    "01111101110000000000",
                                                    "00000001110000000000",
                                                    "00000001110000000000",
                                                    "01111100000000000000",
                                                    "01111101110000000000",
                                                    "00000001110000000000",
                                                    "00000001110000000000",
                                                    "00000011111111000000",
                                                    "00000011111110000000",
                                                    "00000011111111100000",
                                                    "00000011111111110000",
                                                    "00000011111100000000",
                                                    "00000011110111100000",
                                                };
            try
            {
                var puzzle = new Puzzle(InitialData);
                byte[][] result = puzzle.Solve();
                WriteSolutionToConsole(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private static void WriteSolutionToConsole(byte[][] result)
        {
            for (int row = 0; row < result.Length; row++)
            {
                for (int col = 0; col < result[row].Length; col++)
                {
                    Console.Write(result[row][col]);
                }
                Console.WriteLine();
            }
        }
    }
}
