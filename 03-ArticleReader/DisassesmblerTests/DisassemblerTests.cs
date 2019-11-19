using ConsoleApp3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class DisassemblerTests
    {
        [TestMethod]
        public void Should_Throw_Argument_Null_Exception_If_Input_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Disassembler().Disassemble(null));
        }

        [TestMethod]
        public void Should_Throw_Argument_Null_Exception_If_Input_Is_Space()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Disassembler().Disassemble(" "));
        }

        [TestMethod]
        public void Should_Return_Correct_String_Array_For_Test_Case_1()
        {
            var input = "3 * (4 / (1+1))";
            Disassembler disassembler = new Disassembler();
            var result = disassembler.Disassemble(input);
            CollectionAssert.AreEqual(result, new string[] { "3", "*", "(", "4", "/", "(", "1", "+", "1", ")", ")" });
        }


        [TestMethod]
        public void Should_Return_Correct_String_Array_For_Test_Case_2()
        {
            var input = "5 * (1 / (3-1))";
            Disassembler disassembler = new Disassembler();
            var result = disassembler.Disassemble(input);
            CollectionAssert.AreEqual(result, new string[] { "5", "*", "(", "1", "/", "(", "3", "-", "1", ")", ")" });
        }
    }
}
