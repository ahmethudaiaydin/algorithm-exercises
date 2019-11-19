using System;
using System.Linq;
using System.Text;

namespace HaftaninSorusu
{
    class Program
    {

        //Ben region kullanmayı sevmiyorum :) Bundan sonraki çözümlerde elzem görmediğim yerlerde kullanmayacağım.
        #region Private Methods
        private static long GetSum()
        {
            Console.Write("Enter the sum: ");
            return long.Parse(Console.ReadLine());
        }

        private static int[] GetNumbersArray()
        {
            Console.Write("Enter the list with spaces: ");
            return Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        }
        #endregion

        static void Main(string[] args)
        {
            ISumSolver sumSolver = new SumSolver();
            long sum = GetSum();

            var numberArray = GetNumbersArray();
            var result = sumSolver.GetPossibleSumsOf(numberArray, sum);

            StringBuilder builder = new StringBuilder();
            foreach (var item in result)
            {
                builder.Append("{");
                builder.Append(string.Join(",", item));
                builder.AppendLine("}");
            }

            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
    }
}
