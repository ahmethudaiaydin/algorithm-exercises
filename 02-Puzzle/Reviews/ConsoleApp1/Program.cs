using System;
namespace ConsoleApp1
{
    // REVIEW: Çoklu satırda comment yazacaksan, bilhassa örnek kodu comment olarak vermek istiyorsan
    // çoklu satır commanetini kullanmak okunaklılığı arttırır
    /*
    Eleman değerleri 1 ve 0'lardan oluşan 2 boyutlu bir byte vektörü içerisinde 1'lerin oluşturduğu
    en büyük kare alanını bulup yine aynı boyuttaki vektör üzerinde çıktı olarak dönen algoritma?
    Örnek Input = [
    [01111100000000000000],
    [01111101110000000000],
    [00000001110000000000],
    [00000001110000000000],
    [00000011111111000000],
    [00000011111111000000],
    [00000011111111100000],
    [00000011111111110000],
    [00000011111100000000],
    [00000011110111100000]
    ]

    Örnek Output = [
    [00000000000000000000],
    [00000000000000000000],
    [00000000000000000000],
    [00000000000000000000],
    [00000011111000000000],
    [00000011111000000000],
    [00000011111000000000],
    [00000011111000000000],
    [00000011111000000000],
    [00000000000000000000]
    ]
    */

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
            // REVIEW: İsimlendirme
            // Local değişken isimlerini küçük harfle başlatmak
            // daha okunaklı olmasını sağlar. Büyük harfle başlatmak genelde Method,
            // Property, Constant ve public değişken ve isimleri için daha kullanışlıdır.
            // Okunaklılık önemli
            /*
            public class ABC {
                private string _a;
                private const int Abc;

                private string Asd { get; }

                private void Xyz(int a, string b) {
                    var initialData = 0;
                }
            }
             */
            // REVIEW: Bize multidimentional byte array (byte[,]) verileceği söyleniyor, string array değil
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
                // REVIEW: Constructor'ı parametre geçmek class'ın state tutmasını sağlamak içindir.
                // Solve methodu parametre almadığı için her defasında aynı sonucu verecek, bu durumda class'a
                // state tutturmak çok avantajlı bir durum olmadığı gibi çoğu durumda istenmeyen durumlara sebep olabilir.
                // Örnek: Test edilebilirliği düşürecektir. Bknz: OOP vs Functional Programming.
                var puzzle = new Puzzle(InitialData);
                // REVIEW: Geri dönüş değeri 2D vektör olarak isteniyor, Jagged Array olarak değil.
                // Jagged array nedir, multidimentional array (vektör) nedir? Birbirlerine göre artı ve eksilerine bakman lazım.
                // Aslında Jagged array daha avantajlıdır ama sabit boyutlu vektörel array sağlamak için sağlıklı yöntem değildir.
                // https://www.geeksforgeeks.org/c-sharp-jagged-arrays/
                // https://www.pluralsight.com/guides/multidimensional-arrays-csharp
                // https://stackoverflow.com/questions/4648914/why-we-have-both-jagged-array-and-multidimensional-array
                byte[][] result = puzzle.Solve();
                WriteSolutionToConsole(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        // REVIEW: Değişken isimlendirme önemli, input tipinden bir parametreye 'result' ismi vermek kafa karışıklığına neden olur.
        private static void WriteSolutionToConsole(byte[][] result)
        {
            // REVIEW: Console işlemleri her işletim sisteminde en yavaş işlemdlerdendir.
            // Zaten yavaş olan bir işlem içerisinde tek tek her döngüde Console'a yazmak performans kaybına neden olur.
            // Mümkünse Console'a tek seferde yazdırmak çok daha hızlı olur.
            // Bunun için StringBuilder kullanka bize çok fazla avantaj sağlayacaktır.
            /*
            var sBuilder = new StringBuilder();

            string row;
            int rowLength;
            var dataLen = result.Length;

            for (var rowIndex = 0; rowIndex < dataLen; rowIndex++) {
                row = result[row];
                rowLength = row.Length;

                for (var colIndex = 0; colIndex < rowLength; colIndex++) {
                    sBuilder.Append(row[col]);
                }
            }
            Console.WriteLine(sBuilder.ToString());
            */

            // REVIEW: 'var' Keyword kullanımı
            // Değişken tipi verirken "var" kullanmanın runtime'a etksi yoktur
            // ama compiler'a çok az da olsa bir ek iş çıkarır, diğer taraftan kod okunaklılığını arttırır.
            // https://docs.microsoft.com/tr-tr/dotnet/csharp/language-reference/keywords/var
            for (int row = 0; row < result.Length; row++)
            {
                // REVIEW: Property'ler aslında birer get ve set method çağrısıdır.
                // for mekanizmasının çalışması açısından, condition check kısmında method call yapmak
                // her döngüde local değişken kullanmaya göre biraz daha pahalı bir işlemdir.
                // Döngü sayısı arttıkça bu overhead'de artacaktır.
                // Her döngüde değişen bir koşulu kontrol etmemizin gerektiği durumlar bundan kaçamayacağımız bir durumlardır.
                // Ancak sabit değer kontrolü yaptığımız durumlar metod çağrısı yapmak bize avantaj sağlamaz.
                // Burada result[row] -> result.get_this(row).get_Length() şeklinde iki metod çağrısına neden olur ve her döngüde tekrar tekrar çağrı yapılır.
                // Bu ise Stack'e her döngüde iki frame daha eklenmesi, iki frame'in temizlenmesi ve metod çağrısının diğer overhead'lerine,
                // 2 defa execution context switch, return değişkenlerinin initialize edilmesi gibi bir etki yaratır.
                // Burada Length değeri sürekli her döngüde değişmeyeceği için bunu local bir değişken üzerinden kontrol etmek avantaj sağlar.
                /*
                var row = result[row];
                var rowLength = row.Length;
                for (var col = 0; col < rowLength; col++) {
                    Console.Write(row[col]);
                }
                 */
                for (int col = 0; col < result[row].Length; col++)
                {
                    Console.Write(result[row][col]);
                }
                Console.WriteLine();
            }
        }
    }
}
