using System; 

namespace puzzle
{
    class Program
    {
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
