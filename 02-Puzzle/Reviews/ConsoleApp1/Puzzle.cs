using System;

namespace ConsoleApp1
{
    // REVIEW: Kod okunaklılığı için methodlar arasında boşluk koymak okunaklılığı arttırır.
    class Puzzle
    {
        // REVIEW: #region (#region Properties, #region ctors, #region Field members, #region XYZ methods gibi)
        // ile region kullanımı uzun classlar içerinde okunaklılığı arttırır.
        // Aynı zamanda aynı tipten işleri bir arada gruplayarak organize etme alışkalmılığı getirir ki bu da kod takibini kolaylaştırır.
        private string[] InitialData { get; set; }
        public Puzzle(string[] initialData)
        {
            // REVIEW: Güzel yaklaşım. Bu şekildeki input validasyonları kod okunaklılığı arttırır.
            InitialData = initialData ?? throw new ArgumentNullException(nameof(initialData));
        }
        public byte[][] Solve()
        {
            var board = GetBoard(InitialData);
            int biggest = 0;
            int rowOfMax = 0;
            int colOfMax = 0;

            // REVIEW: "WriteSolutionToConsole"da yazdığım review burada da geçerli
            // REVIEW: Nullable bir değişkenle takip etmeye gerek var mı? Nullable olmadan int tipini kullansak yeterli değil mi?
            int? minimumNeighbour = null;
            int[][] boardShadow = new int[board.Length][];
            for (int row = 0; row < board.Length; row++)
            {
                boardShadow[row] = new int[board[row].Length];
                for (int col = 0; col < board[row].Length; col++)
                {
                    if (row > 0 && col > 0 && board[row][col] > 0)
                    {
                        minimumNeighbour = Math.Min(Math.Min((int)boardShadow[row - 1][col], (int)boardShadow[row - 1][col - 1]), (int)boardShadow[row][col - 1]);
                    }

                    // REVIEW: boardShadow[row][col] = minimumNeighbour ?? board[row][col];
                    // şeklinde yazmank kodu kısaltacak ve daha okunaklı yapacaktır.
                    boardShadow[row][col] = minimumNeighbour != null ? 1 + minimumNeighbour.Value : board[row][col];

                    if (biggest < boardShadow[row][col])
                    {
                        biggest = boardShadow[row][col];
                        rowOfMax = row;
                        colOfMax = col;
                    }

                    //Console.Write(boardShadow[row][col]);
                    // REVIEW: Kodun sonunda null'a set etme işlemini kodun başında initialize etmek için yapsak değişkenin takibi daha kolay olmaz mı?
                    minimumNeighbour = null;
                }
                //Console.WriteLine();
            }

            return GetResultIn2DArray(boardShadow,rowOfMax,colOfMax,biggest);
        }

        private byte[][]  GetResultIn2DArray(int[][] boardShadow, int rowOfMax, int colOfMax, int biggest)
        {
            // REVIEW: rowOfMax ve colOfMax'i biliyorsak tüm board'u dolaşmaya gerek var mı?
            // REVIEW: "WriteSolutionToConsole"da yazdığım review burada da geçerli
            byte[][] result = new byte[boardShadow.Length][];
            for (int row = 0; row < boardShadow.Length; row++)
            {
                result[row] = new byte[boardShadow[row].Length];
                for (int col = 0; col < boardShadow[row].Length; col++)
                {
                    if ((row < rowOfMax - biggest + 1 || row > rowOfMax)
                     || (col < colOfMax - biggest + 1 || col > colOfMax)
                         )
                    {
                        result[row][col] = 0;
                    }
                    else
                    {
                        // REVIEW: Initial values
                        // Bir array construct edildiğinde array item'ların initial değerleri nedir?
                        // Hücreleri '0'lamaya gerek var mı?
                        result[row][col] = 1;
                    }
                }
            }

            return result;
        }
        private byte[][] GetBoard(string[] data)
        {
            // REVIEW: "WriteSolutionToConsole"da yazdığım review burada da geçerli
            /*
                var rowCount = data.Length;
                var result = new byte[rowCount][];

                int colCount;
                string srcRow;
                byte[] destRow;
                for (var rowIndex = 0; rowIndex < rowCount; rowIndex++) {
                    srcRow = data[i];
                    colCount = srcRow.Length;
                    result[i] = (destRow = new byte[colCount]);

                    for (var colIndex = 0; colIndex < colCount; colIndex++) {
                        destRow[colIndex] = (byte)srcRow[colIndex];
                    }
                }
                return result;
            */
            // REVIEW: Data temiz bir kod olması açısından:
            /*
            private byte[] StringAsByteArray(string data) {
                if (data == null) {
                    throw new ArgumentNullException(nameof(data));
                }

                var dataLen = data.Length;
                var result = new byte[dataLen];

                for (var i = 0; i < dataLen; i++) {
                    destRow[i] = (byte)srcRow[i];
                }
                return result;
            }

            private byte[][] GetBoard(string[] data) {
                if (data == null) {
                    throw new ArgumentNullException(nameof(data));
                }

                var rowCount = data.Length;
                var result = new byte[rowCount][];

                for (var i = 0; i < rowCount; i++) {
                    result[i] = StringAsByteArray(data[i]);
                }
                return result;
            }
            */
            byte[][] board = new byte[data.Length][];

            for (int i = 0; i < data.Length; i++)
            {
                board[i] = new byte[data[i].Length];
                for (int j = 0; j < data[i].Length; j++)
                {
                    // REVIEW: Convert class'ını anlamak önemli
                    // https://referencesource.microsoft.com/#mscorlib/system/convert.cs,fc990bd1275d43d6
                    // REVIEW: data[i][j] ne döner? data[i][j].ToString()'e gerek var mı?
                    board[i][j] = Convert.ToByte(data[i][j].ToString());
                }
            }
            return board;
        }
    }
}
