//Bir matematik ifade(expression) girdi olarak string cinsinden veriliyor.Bu matematiksel ifadeyi parçalarına ayırıp string dizisi şeklinde çıktı olarak veren algoritma?
//Input = "3 * (4 / (1+1))"
//Output = ["3", "*", "(", "4", "/", "(", "1", "+", "1", ")", ")"]
using System;
using System.Text;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Disassembler disassembler = new Disassembler();
            var disassembledString = disassembler.Disassemble("3 * (4 / (1+1))");
            Console.WriteLine(GetStringResult(disassembledString));
        }
        
        private static string GetStringResult(string[] disassembledString)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            var len = disassembledString.Length;
            for (int i = 0; i < len; i++)
            {
                builder.Append("\"");
                builder.Append(disassembledString[i]);
                builder.Append("\",");
            }

            builder.Append("]");
            return builder.ToString();
        }
    }
}
