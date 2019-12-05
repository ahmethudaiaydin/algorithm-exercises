using ExpressionEvaluator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExpressionEvaluatorTest
{
    [TestClass]
    public class ExpressionEvaluatorTest
    {
        [TestMethod]
        public void Should_Find_Left_Hand_Bracket()
        {
            var expressionHandler = new ExpressionHandler();
            Assert.IsTrue(expressionHandler.IsLeftHandBracket('('));
        }

        [TestMethod]
        public void Should_Find_Right_Hand_Bracket()
        {
            var expressionHandler = new ExpressionHandler();
            Assert.IsTrue(expressionHandler.IsRightHandBracket(')'));
        }

        [TestMethod]
        public void Should_Be_False_Left_And_Right_Hand_Bracket_For_Wrong_Char()
        {
            var expressionHandler = new ExpressionHandler();
            Assert.IsFalse(expressionHandler.IsRightHandBracket('+'));
            Assert.IsFalse(expressionHandler.IsLeftHandBracket('*'));
        }

        [TestMethod]
        public void Should_Throw_Bracket_Exception_For_Invalid_Expression()
        {
            var expressionHandler = new ExpressionHandler();
            var expression = "3 + ( 5-1";
            Assert.ThrowsException<InvalidOperationException>(() => expressionHandler.GetBracketIndexList(expression));
        }

        [TestMethod]
        public void Should_Return_Empty_List_If_Expression_Is_Null()
        {
            var expressionHandler = new ExpressionHandler();
            Assert.IsTrue(expressionHandler.GetBracketIndexList(null).Count == 0);
        }

        [TestMethod]
        public void Should_Return_Correct_Count_For_Brackets()
        {
            var expressionHandler = new ExpressionHandler();
            var expression = "3 + (5-1(4*6))";
            Assert.IsTrue(expressionHandler.GetBracketIndexList(expression).Count == 2);
        }

        [TestMethod]
        public void Should_Throw_Exception_For_Null_Or_WhiteSpace_Expression()
        {
            var expressionHandler = new ExpressionHandler();
            Assert.ThrowsException<InvalidOperationException>(() => expressionHandler.Evaluate(null));
            Assert.ThrowsException<InvalidOperationException>(() => expressionHandler.Evaluate(" "));
        }

        [TestMethod]
        public void Should_Return_Expression_In_Parenthesis_With_Given_Indexes()
        {
            var expressionHandler = new ExpressionHandler();
            var expression = "3 + (5-1(4*6))";
            var expressionInParantehesis1 = expressionHandler.GetParenthesisExpression(expression, 8, 12);
            var expressionInParantehesis2 = expressionHandler.GetParenthesisExpression(expression, 4, 13);
            Assert.AreEqual("(4*6)", expressionInParantehesis1);
            Assert.AreEqual("(5-1(4*6))", expressionInParantehesis2);
        }

        [TestMethod]
        public void Should_Return_Disassembled_Math_Chars()
        {
            var disassembler = new Disassembler();
            var input = "(4*6)";
            var disasembledArray = disassembler.Disassemble(input);
            CollectionAssert.Contains(disasembledArray, "(");
            CollectionAssert.Contains(disasembledArray, "4");
            CollectionAssert.Contains(disasembledArray, "*");
            CollectionAssert.Contains(disasembledArray, "6");
            CollectionAssert.Contains(disasembledArray, ")");
        }

        [TestMethod]
        public void Should_Calculate_Expression()
        {
            var expressionHandler = new ExpressionHandler();
            var expression = "(4*6)";
            Assert.AreEqual(24, expressionHandler.CalculateExpression(expression));
        }

        [TestMethod]
        public void Should_Evaluate()
        {
            var expressionHandler = new ExpressionHandler();
            var expression = "3 * (4 / (1+1))";
            Assert.AreEqual(6,(int) expressionHandler.Evaluate(expression));

        }
    }
}
