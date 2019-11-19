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
        public string GetUnusedASCIICharacters(string filePath)
        {
            ValidateParameters(filePath);

            byte[] asciiBytes = GetFoundASCIIBytes(filePath);
            
            return GetUnFoundCharactersAsString(asciiBytes); 
        }

        #region Private Methods
        private string GetUnFoundCharactersAsString(byte[] byteArray)
        {
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
            var foundASCIIBytes = new byte[128];
            const int chunkSize = 84000;
            using (var file = File.OpenRead(filePath))
            {
                var buffer = new byte[chunkSize];
                while (file.Read(buffer, 0, chunkSize) > 0)
                {
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
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException(nameof(filePath));
        }
        #endregion
    }
}
