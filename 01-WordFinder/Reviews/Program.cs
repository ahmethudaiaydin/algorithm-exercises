using System;

namespace puzzle
{
    class Program
    {
        // REVIEW: Vektor nedir? C#'ta nasil kullanilir?
        readonly static string[] vector = new string[] {
            "xxxxxxxnxx",
            "xxxxxxaxxx",
            "xnxxxgxxxx",
            "xxaxoxxxxx",
            "xaadaxxxxx",
            "xxkixxxxxx",
            "xxxxfxxxxx"};

        static void Main(string[] args)
        {
            string[] keys = new string[] { "dogan", "akif", "kaan", "kagan" };

            WordFinder wf = new WordFinder(vector);
            foreach (var key in keys)
            {
                Console.WriteLine(wf.IsWordExists(key));
            }

            Console.ReadLine();
        }
    }
}
