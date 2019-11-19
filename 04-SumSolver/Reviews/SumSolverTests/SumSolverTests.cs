using System;
using HaftaninSorusu;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SumSolverTests
{
    // REVIEW: Daha fazla fail test'leri yazmak güzel olur
    [TestClass]
    public class SumSolverTests
    {
        [TestMethod]
        public void Should_Return_Empty_Array_If_Input_Is_Empty()
        {
            ISumSolver solver = new SumSolver();
            var result = solver.GetPossibleSumsOf(new int[0], 0);
            CollectionAssert.AreEqual(new long[0][], result);
        }

        [TestMethod]
        public void Should_Throw_Null_Exception_If_Input_Is_Null()
        {
            ISumSolver solver = new SumSolver();
            Assert.ThrowsException<ArgumentNullException>(() => solver.GetPossibleSumsOf(null, 0));
        }

        [TestMethod]
        public void Should_Work_Correct_With_Given_Input()
        {
            ISumSolver solver = new SumSolver();
            long sum = 8;
            var list = new int[7] { 1, 3, 5, -1, 7, 8, 9 };
            var expected = new int[][] {
                new int[] { 1, 7 },
                new int[] { 3, 5 },
                new int[] { -1, 9 },
                new int[] { -1, 1, 8 },
                new int[] { -1, 1, 3, 5 },
                 };

            var result = solver.GetPossibleSumsOf(list, sum);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void Should_Work_Correct_With_Given_Input_2()
        {
            ISumSolver solver = new SumSolver();
            long sum = 4;
            var list = new int[3] { 1, 3, 5 };
            var expected = new int[] { 1, 3 };

            var result = solver.GetPossibleSumsOf(list, sum);
            CollectionAssert.AreEquivalent(expected, result[0]);
        }

    }
}
