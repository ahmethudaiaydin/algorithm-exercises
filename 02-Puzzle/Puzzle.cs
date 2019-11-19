using System;

namespace ConsoleApp1
{
    class Puzzle
    {
        private string[] InitialData { get; set; }
        public Puzzle(string[] initialData)
        {
            InitialData = initialData ?? throw new ArgumentNullException(nameof(initialData));
        }
        public byte[][] Solve()
        {
            var board = GetBoard(InitialData);
            int biggest = 0;
            int rowOfMax = 0;
            int colOfMax = 0;

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

                    boardShadow[row][col] = minimumNeighbour != null ? 1 + minimumNeighbour.Value : board[row][col];

                    if (biggest < boardShadow[row][col])
                    {
                        biggest = boardShadow[row][col];
                        rowOfMax = row;
                        colOfMax = col;
                    }

                    //Console.Write(boardShadow[row][col]);
                    minimumNeighbour = null;
                }
                //Console.WriteLine();
            }
            
            return GetResultIn2DArray(boardShadow,rowOfMax,colOfMax,biggest);
        }

        private byte[][]  GetResultIn2DArray(int[][] boardShadow, int rowOfMax, int colOfMax, int biggest)
        {
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
                        result[row][col] = 1;
                    }
                }
            }
         
            return result;
        }
        private byte[][] GetBoard(string[] data)
        {
            byte[][] board = new byte[data.Length][];

            for (int i = 0; i < data.Length; i++)
            {
                board[i] = new byte[data[i].Length];
                for (int j = 0; j < data[i].Length; j++)
                {
                    board[i][j] = Convert.ToByte(data[i][j].ToString());
                }
            }
            return board;
        }
    }
}
