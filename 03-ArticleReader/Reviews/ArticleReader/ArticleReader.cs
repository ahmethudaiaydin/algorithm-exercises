using System;
using System.IO;
using System.Text;

namespace ArticleReader
{
    //TODO: Vakit kalmadığı için yapamadığım bazı şeyler var.
    //1. Stream tarafını bir şekilde mock'layıp test yazabilirdim.
    //2. Private metotların test edilebilmesi için ya public yapacaktım, ya da ayrı bir sınıfa taşıyıp, utility gibi kullanacaktım. Yetişmedi :)
    //3. Şu anda chunk base read yapıyor, ancak dosya size'ına bakarak direk memory'e dosyayı çekebilecek bir implementation da yazılabilir. Dosya boyutu küçükse o implementation ile devam edilebilir. Strategy pattern di sanırım ya da template pattern olabilir.

    public class ArticleReader
    {
        // REVIEW: "#region Private Methods" güzel, peki neden bu method da ayrıca region'lı' değil?
        public string GetUnusedASCIICharacters(string filePath)
        {
            // REVIEW: Hangi parametreleri validate ediyor?
            ValidateParameters(filePath);

            byte[] asciiBytes = GetFoundASCIIBytes(filePath);
            
            return GetUnFoundCharactersAsString(asciiBytes); 
        }

        #region Private Methods
        private string GetUnFoundCharactersAsString(byte[] byteArray)
        {
            // REVIEW: StringBuilder'da append etmeden daha efektif bir yöntem yok mu? Bknz: string de bir objedir, 
            // obje ise construct edilemez mi, edilebilirse constructor'ı hangi parametreleri alır?
            // https://referencesource.microsoft.com/#mscorlib/system/string.cs,8281103e6f23cb5c
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 128; i++)
            {
                if (byteArray[i] == 0)
                {
                    builder.Append((char)i);
                }
            }
            return builder.ToString();

        }
        private byte[] GetFoundASCIIBytes(string filePath)
        {
            // REVIEW: Dosya ANSI
            var foundASCIIBytes = new byte[128];
            // REVIEW: Bu  const neden class scopunda değil?
            const int chunkSize = 84000;
            using (var file = File.OpenRead(filePath))
            {
                var buffer = new byte[chunkSize];
                // REVIEW: Dosyadan kaç byte okundu? Dosyanın boyutu < 84000 olduğunda buffer.Length ne işimize yarıyor?
                while (file.Read(buffer, 0, chunkSize) > 0)
                {
                    // REVIEW: Buffer'ın boyu değişmiyor, neden uzunluğu her defasında while'ın her loop'unda alıyoruz?
                    var len = buffer.Length;
                    for (int i = 0; i < len; i++)
                    {
                        if (buffer[i] < 128 && foundASCIIBytes[buffer[i]] == 0)
                        {
                            foundASCIIBytes[buffer[i]] = 1;
                        }
                    }
                }
            }

            return foundASCIIBytes;
        }

        private void ValidateParameters(string filePath)
        {
            // REVIEW: filePath "  " olarak verilmişse?
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(nameof(filePath));
        }
        #endregion
    }
}
