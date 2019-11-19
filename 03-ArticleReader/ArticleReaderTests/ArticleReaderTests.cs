using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ArticleReaderTests
{
    [TestClass]
    public class ArticleReaderTests
    {
        [TestMethod]
        public void Should_Throw_Exception_If_FilePath_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ArticleReader.ArticleReader().GetUnusedASCIICharacters(null));
        }

        [TestMethod]
        public void Should_Throw_Exception_If_File_Not_Found()
        {
            Assert.ThrowsException<FileNotFoundException>(() => new ArticleReader.ArticleReader().GetUnusedASCIICharacters("notFound.txt"));
        }
    }
}
