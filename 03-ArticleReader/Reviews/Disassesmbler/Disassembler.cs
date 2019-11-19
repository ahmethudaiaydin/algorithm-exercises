using System;

namespace ConsoleApp3
{
    public class Disassembler
    {
        // REVIEW: #region?
        // REVIEW: Neden const değil de readonly? const gibi kullanacağın bir parametre ve 
        // const değer atayamayacağın birşeyse neden "static readonly" değil de sadece readnly?
        private readonly byte _smallestMathCharacterValue = (byte)'(';
        private readonly byte _biggestMathCharacterValue = (byte)'9';

        // REVIEW: "(-3 + ( +5 * -3) % 3) + 渼" yi dener misin?
        public string[] Disassemble(string input)
        {
            ValidateInput(input);

            var inputLength = input.Length;
            var stringArrayLen = 0;
            var stringArray = new string[inputLength];
            // REVIEW: Neden yukarıda "var" burada "int"?
            for (int i = 0; i < inputLength; i++)
            {
                // REVIEW: Match etmiyors bişey yapmak gerekmez mi?
                if (IsMathCharacter(input[i]))
                {
                    stringArray[stringArrayLen++] = new string(input[i], 1);
                }
            } // REVIEW: Scope'lar ve ilgili bloklar arasında mantıksal boşluk?
            // REVIEW: stringArray'i neden dönmedik de resultArray'e kopyalayıp dönüyoruz.
            // REVIEW: Array.Copy'e bakalım
            var resultArray = new string[stringArrayLen];
            // REVIEW: Neden yukarıda "var" burada "int"?
            for (int i = 0; i < stringArrayLen; i++)
            {
                resultArray[i] = stringArray[i];
            }
            return resultArray;
        }

        #region Private Methods

        private bool IsMathCharacter(char characterToCheck)
        {
            // REVIEW: characterToCheck = '渼' olduğunda ne olur?
            var charAsByte = (byte)characterToCheck;
            return charAsByte >= _smallestMathCharacterValue && charAsByte <= _biggestMathCharacterValue;

        } // REVIEW: Metodlar arasında boşluk?
        private void ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        } 
        #endregion

    }
}