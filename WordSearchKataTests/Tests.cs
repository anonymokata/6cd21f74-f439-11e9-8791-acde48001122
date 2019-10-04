using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearchKata;

namespace WordSearchKataTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetWordsFromFileReturnsArrayOfWords()
        {
            var expectedWords = new string[]
            {
                "BONES", "KHAN", "KIRK", "SCOTTY", "SPOCK", "SULU", "UHURA"
            };

            string[] loadedWords = WordSearch.GetWordsFromFile("input.txt");
            Assert.IsTrue(loadedWords.SequenceEqual(expectedWords));
        }
    }
}
