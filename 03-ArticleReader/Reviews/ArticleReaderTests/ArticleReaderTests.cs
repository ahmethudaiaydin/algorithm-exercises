using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ArticleReaderTests
{
    [TestClass]
    // REVIEW: ArticleReader class'ı sadece file ismi null mı veya dosya var mı kontrolu yapan bir utility sınıfı mı?
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
