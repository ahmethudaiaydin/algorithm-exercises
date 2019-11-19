using System;
using System.Text; 

namespace ArticleReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // REVIEW: Daha önce de 'var' kullanımını belirtmiştim
            ArticleReader reader = new ArticleReader();
            string result = reader.GetUnusedASCIICharacters("abc.txt");
            Console.OutputEncoding = Encoding.ASCII;
            Console.Write(result);
            Console.ReadLine();
        }
    }
}
