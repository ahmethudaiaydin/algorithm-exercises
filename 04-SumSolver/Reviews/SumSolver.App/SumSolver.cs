using System;
using System.Collections.Generic;

namespace HaftaninSorusu
{
    public interface ISumSolver
    {
        int[][] GetPossibleSumsOf(int[] list, long sumToCheck);
    }

    // REVIEW: Eğer interface kullanacaksan bu class'ın adı "SumSolver" yerine
    // yaptığı işi yansıtacak şekliyle "BruteForceSumSolver" gibi bişey olsa veya 
    // abstract bir "AbstractSumSolver" class üzerinde ortak işler ile ilerlenip 
    // "BruteForceSumSolver" abstract'dan türese interface daha anlamlı olmaz mı?
    public class SumSolver : ISumSolver
    {
        #region Private Methods
        private void ValidateParameter(int[] list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
        }

        private bool IsListEmpty(int[] list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Length == 0;
        }
        #endregion // REVIEW: #endregion'a da (örn: #endregion Private Methods) region ismini verirsen 
        // uzun regionlarda şu anda hangi alan içerisinde olduğunu takip etmesi kolay olur 

        #region Public Methods
        public int[][] GetPossibleSumsOf(int[] list, long sumToCheck)
        {
            ValidateParameter(list);

            var lengthOfNumbers = list.Length;
            var possibleSumsOfList = new List<int[]>();

            // REVIEW: Yukarıda ayrı bir metod içerisinde null check yapmışsın, ayrıca "lengthOfNumbers" ile uzunluğu da almışsın, 
            // burada sadece iş lengthOfNumbers kontrolüne kalmış, ayrı bir metoda gerek var mı?
            if (IsListEmpty(list))
            {
                return new int[0][];
            }

            //Algoritma sadece sıralı olan toplamları bulabiliyor. İlk sayı hariç, ardışık olmayan toplamları hesaplayamıyor.
            // REVIEW: Bu dış for loop'una neden gerek var anlayamadım, aslında tüm for loop'larının ne yaptığını da tam olarak anlayamadım :) 
            // Bu bile clean code için bir ipucu veriyor sanırım
            for (var lengthOfSubSet = 1; lengthOfSubSet < lengthOfNumbers; lengthOfSubSet++)
            {
                for (var numberListIndex = 0; numberListIndex < lengthOfNumbers; numberListIndex++)
                {
                    var currentNumber = list[numberListIndex];
                    for (var subSetIndex = numberListIndex + 1; subSetIndex < lengthOfNumbers; subSetIndex++)
                    {
                        // REVIEW: Her defasında olası büyük bir array üzerinde toplamak yerine, önce toplamları bulmak için for'da dönüp
                        // eğer toplam sağlanıyorsa başlangıç ve bitiş indekslerini custom bir struct'da veya ArraySegment<> sınıfında tutup 
                        // en dış possibleSumsOfList'i oluşturmak memory kullanımı açısından daha efektif bir yöntem olacaktır.
                        // https://referencesource.microsoft.com/#mscorlib/system/arraysegment.cs,d2e9d465e1639af7
                        var possibleSum = new int[lengthOfSubSet + 1];
                        var currentSum = currentNumber;

                        possibleSum[0] = currentNumber;
                        for (var possibleSumIndex = 0; possibleSumIndex < lengthOfSubSet
                                                    && subSetIndex + possibleSumIndex < lengthOfNumbers; possibleSumIndex++)
                        {
                            possibleSum[possibleSumIndex + 1] = list[subSetIndex + possibleSumIndex];
                            currentSum += possibleSum[possibleSumIndex + 1];
                        }

                        if (currentSum == sumToCheck)
                        {
                            possibleSumsOfList.Add(possibleSum);
                        }
                    }
                }
            }
            return possibleSumsOfList.ToArray();
        }
        #endregion
    }
}
