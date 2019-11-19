using System;
namespace ConsoleApp1
{
    // Eleman değerleri 1 ve 0'lardan oluşan 2 boyutlu bir byte vektörü içerisinde 1'lerin oluşturduğu en büyük kare alanını bulup yine aynı boyuttaki vektör üzerinde çıktı olarak dönen algoritma?
    // Örnek Input = [
    // [01111100000000000000],
    // [01111101110000000000],
    // [00000001110000000000],
    // [00000001110000000000],
    // [00000011111111000000],
    // [00000011111111000000],
    // [00000011111111100000],
    // [00000011111111110000],
    // [00000011111100000000],
    // [00000011110111100000]
    // ]

    // Örnek Output = [
    // [00000000000000000000],
    // [00000000000000000000],
    // [00000000000000000000],
    // [00000000000000000000],
    // [00000011111000000000],
    // [00000011111000000000],
    // [00000011111000000000],
    // [00000011111000000000],
    // [00000011111000000000],
    // [00000000000000000000]
    // ]

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
