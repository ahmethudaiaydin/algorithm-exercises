using System;
using System.Collections.Generic;

namespace HaftaninSorusu
{
    public interface ISumSolver
    {
        int[][] GetPossibleSumsOf(int[] list, long sumToCheck);
    }

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
        #endregion 

        #region Public Methods
        public int[][] GetPossibleSumsOf(int[] list, long sumToCheck)
        {
            ValidateParameter(list);

            var lengthOfNumbers = list.Length;
            var possibleSumsOfList = new List<int[]>();

            if (IsListEmpty(list))
            {
                return new int[0][];
            }

            //Algoritma sadece sıralı olan toplamları bulabiliyor. İlk sayı hariç, ardışık olmayan toplamları hesaplayamıyor.
            for (var lengthOfSubSet = 1; lengthOfSubSet < lengthOfNumbers; lengthOfSubSet++)
            {
                for (var numberListIndex = 0; numberListIndex < lengthOfNumbers; numberListIndex++)
                {
                    var currentNumber = list[numberListIndex];
                    for (var subSetIndex = numberListIndex + 1; subSetIndex < lengthOfNumbers; subSetIndex++)
                    {

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
