using System;

namespace ExpressionEvaluator
{

    public class Disassembler
    {
        #region Private Variables

        private readonly byte _smallestMathCharacterValue = (byte)'(';
        private readonly byte _biggestMathCharacterValue = (byte)'9';

        #endregion

        #region Public Methods
        
        public string[] Disassemble(string input)
        {
            ValidateInput(input);

            var inputLength = input.Length;
            var stringArrayLen = 0;
            var stringArray = new string[inputLength];
            for (int i = 0; i < inputLength; i++)
            {
                if (IsMathCharacter(input[i]))
                {
                    stringArray[stringArrayLen++] = new string(input[i], 1);
                }
            }
            var resultArray = new string[stringArrayLen];
            for (int i = 0; i < stringArrayLen; i++)
            {
                resultArray[i] = stringArray[i];
            }
            return resultArray;
        }

        #endregion

        #region Private Methods

        private bool IsMathCharacter(char characterToCheck)
        {
            var charAsByte = (byte)characterToCheck;
            return charAsByte >= _smallestMathCharacterValue && charAsByte <= _biggestMathCharacterValue;

        }
        private void ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) throw new ArgumentNullException(nameof(input));
        }

        #endregion
    }
}
