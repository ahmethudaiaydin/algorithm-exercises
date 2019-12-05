using System;
using System.Data;
using System.Collections.Generic;

namespace ExpressionEvaluator
{
    public class ExpressionHandler
    {
        #region Private Variables

        private readonly Disassembler disassembler = new Disassembler();

        #endregion

        #region Public Methods

        public double Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new InvalidOperationException("Expression is invalid");
            }

            var bracketIndexList = GetBracketIndexList(expression);
            var resultExpression = "";
            while (bracketIndexList.Count > 0)
            {
                var bracketIndex = bracketIndexList.Pop();

                var parenthesisExpression = GetParenthesisExpression(expression, bracketIndex.Item1, bracketIndex.Item2);

                var disassmbledMathCharacters = disassembler.Disassemble(parenthesisExpression);

                var resultOfPartOfExpression = CalculateExpression(string.Join(' ', disassmbledMathCharacters));

                resultExpression = expression.Remove(bracketIndex.Item1, bracketIndex.Item2 - bracketIndex.Item1 + 1);
                resultExpression = resultExpression.Insert(bracketIndex.Item1, resultOfPartOfExpression.ToString());

            }

            return CalculateExpression(resultExpression);
        }

        public double CalculateExpression(string expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return (double.Parse(new DataTable().Compute(expression, null).ToString())); // Bu satırı kuzen yazmış. :)
        }

        public string GetParenthesisExpression(string expression, int leftHandBracketIndex, int rightHandBracketIndex)
        {
            return expression.Substring(leftHandBracketIndex, rightHandBracketIndex - leftHandBracketIndex + 1);
        }

        public Stack<Tuple<int, int>> GetBracketIndexList(string expression)
        {
            var bracketList = new Stack<Tuple<int, int>>();
            if (string.IsNullOrWhiteSpace(expression))
            {
                return bracketList;
            }

            var lastIndexToCheck = expression.Length - 1;
            for (var i = 0; i < expression.Length; i++)
            {

                if (IsLeftHandBracket(expression[i]))
                {
                    Tuple<int, int> bracketIndex = null;

                    //Find the RightHandBracket
                    for (int j = lastIndexToCheck; j >= 0; j--)
                    {
                        if (IsRightHandBracket(expression[j]))
                        {
                            bracketIndex = new Tuple<int, int>(i, j);
                            lastIndexToCheck = j - 1;
                            break;
                        }
                    }

                    bracketList.Push(bracketIndex ?? throw new InvalidOperationException("Expression is invalid"));
                }
            }

            return bracketList;
        }

        public bool IsLeftHandBracket(char v) => v == '(';

        public bool IsRightHandBracket(char v) => v == ')';
        #endregion
    }
}
