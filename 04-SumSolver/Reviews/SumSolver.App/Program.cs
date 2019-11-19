using System;
using System.Linq;
using System.Text;

namespace HaftaninSorusu
{
    class Program
    {
        // REVIEW: Ben de biçok şeyi yapmayı pek sevmiyorum ama yapıyorum :) 
        // Sen de hak verirsin ki hayatta sadece sevdiğimiz şeyleri hoşlandığımız şekilde yapmıyoruz Alışkanlık kazanmak önemli
        // Ben region kullanmayı sevmiyorum :) Bundan sonraki çözümlerde elzem görmediğim yerlerde kullanmayacağım.
        #region Private Methods
        private static long GetSum()
        {
            Console.Write("Enter the sum: ");
            // REVIEW: Girilen değer numeric olmazsa? Parse yerine TryParse'ı Retry 
            return long.Parse(Console.ReadLine());
        }

        // REVIEW: Console işlemleriyle string split ve int convert işlemlerini ayrıştırsan daha şık olurdu
        /* 
        private static int[] ToIntArray(this IEnumerable<string> strings) { ... }

        private static IEnumerable<string> Split(string line, char splitChar) { ... }

        private static string ReadNumbers() { ... }
        */
        private static int[] GetNumbersArray()
        {
            Console.Write("Enter the list with spaces: ");
            // REVIEW: Bu
            return Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        }

        #endregion

        static void Main(string[] args)
        {
            // REVIEW: "var" kullanımı
            ISumSolver sumSolver = new SumSolver();
            long sum = GetSum();

            var numberArray = GetNumbersArray();
            var result = sumSolver.GetPossibleSumsOf(numberArray, sum);

            // REVIEW: "var" kullanımı
            StringBuilder builder = new StringBuilder();
            foreach (var item in result)
            {
                // REVIEW: StringBuilder'a tek karakterlik string append edeceğinde Append('{') şeklinde char append daha hızlıdır
                // https://referencesource.microsoft.com/#mscorlib/system/text/stringbuilder.cs,a2e7c78d85807da5,references
                // https://referencesource.microsoft.com/#mscorlib/system/text/stringbuilder.cs,e8eaef3c361184bc,references
                builder.Append("{");
                // REVIEW: StringBuilder'a tek tek append etmek, string.Join ile uzun tek bir string yaratarak append etmekten kurtarmak için bir yöntemdir. 
                // Arka planda zaten bir StringBuilder kullanarak StringBuilder.ToString ile string oluşturur.
                // StringBuilder string concat'in overhead'inden kaçmak içindir. Bu şekilde StringBuilder'ın amacından saptırıyor, 
                // her loop'da iki defa SttringBuilder ve ToString ile yaratılmış iki büyük string kullanmış oluyorsun.
                // https://referencesource.microsoft.com/#mscorlib/system/string.cs,815fed0ee06a7bc8
                builder.Append(string.Join(",", item));
                builder.AppendLine("}");
            }

            Console.WriteLine(builder.ToString());
            Console.ReadLine();
        }
    }
}
