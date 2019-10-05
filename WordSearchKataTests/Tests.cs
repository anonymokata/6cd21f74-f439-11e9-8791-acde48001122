using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearchKata;

namespace WordSearchKataTests
{
    [TestClass]
    public class Tests
    {
        //hardcoded answers to check against
        Dictionary<string, List<(int, int)>> locations = new Dictionary<string, List<(int, int)>>()
        {
            { "SNICKERS",
                new List<(int, int)>() { (6, 16), (7, 15), (8, 14), (9, 13), (10, 12), (11, 11), (12, 10), (13, 9) } },
            { "KITKAT",
                new List<(int, int)>() { (17, 20), (16, 21), (15, 22), (14, 23), (13, 24), (12, 25) } },
            { "REECES",
                new List<(int, int)>() { (4, 21), (4, 22), (4, 23), (4, 24), (4, 25), (4, 26) } },
            { "BUTTERFINGER",
                new List<(int, int)>() { (27, 11), (27, 10), (27, 9), (27, 8), (27, 7), (27, 6), (27, 5), (27, 4), (27, 3), (27, 2), (27, 1), (27, 0) } },
            { "TWIX",
                new List<(int, int)>() { (8, 31), (7, 30), (6, 29), (5, 28) } },
            { "MILKYWAY",
                new List<(int, int)>() { (5, 0), (6, 1), (7, 2), (8, 3), (9, 4), (10, 5), (11, 6), (12, 7) } },
            { "CRUNCH",
                new List<(int, int)>() { (21, 10), (20, 10), (19, 10), (18, 10), (17, 10), (16, 10) } },
            { "HERSHEYS",
                new List<(int, int)>() { (2, 20), (3, 20), (4, 20), (5, 20), (6, 20), (7, 20), (8, 20), (9, 20) } }
        };

        //hardcoded puzzle to check against
        char[,] expectedLetters = new char[,]
        {
            {'B','P','O','Z','C','M','N','T','O','F','W','W','E','U','C','R','P','K','Y','H','K','Y','H','K','V','W','K','R','N','F','R','K'},
            {'T','K','R','U','O','Z','I','A','T','B','U','Z','Z','G','U','L','K','U','E','S','S','T','Q','L','M','Y','A','E','Z','F','B','A'},
            {'W','V','O','N','O','M','M','L','O','O','J','K','F','N','A','D','G','W','R','D','I','O','D','N','F','B','N','G','D','K','P','P'},
            {'Y','A','U','F','U','U','G','A','K','B','I','T','H','B','Y','J','J','G','F','C','X','A','F','M','Q','J','V','N','V','I','H','G'},
            {'H','A','Z','W','N','M','P','I','H','Y','U','C','Y','Q','L','H','D','B','M','K','H','C','Y','Q','Z','Z','O','I','B','D','U','U'},
            {'R','P','P','X','Y','D','B','N','K','J','W','E','G','J','C','F','T','K','Y','D','T','J','F','Y','S','J','V','F','Q','K','J','T'},
            {'Z','W','P','R','B','Q','Q','O','M','G','J','A','A','L','U','R','K','N','B','R','O','C','O','Y','O','R','C','R','L','V','Y','M'},
            {'D','K','L','K','H','P','E','Q','U','U','U','O','Y','W','L','Q','U','V','N','Q','H','J','C','M','H','X','A','E','I','B','D','F'},
            {'K','H','B','S','E','I','B','O','L','G','F','L','A','I','C','U','H','S','N','D','N','J','U','T','V','L','Z','T','H','W','K','T'},
            {'B','Q','D','Z','H','G','O','F','R','M','Z','S','E','S','E','N','K','F','Y','X','P','X','G','F','F','Q','R','T','I','B','Y','K'},
            {'E','O','Q','B','N','R','W','Q','Z','Z','D','U','R','E','A','S','H','C','N','U','R','C','I','A','A','X','C','U','U','V','L','V'},
            {'S','X','Y','N','K','V','S','Y','D','V','Z','E','I','U','X','B','L','N','R','B','W','F','Y','Q','M','T','H','B','Z','H','U','A'},
            {'L','I','U','E','U','Y','B','C','F','M','K','H','X','I','W','J','L','O','Q','C','O','R','G','O','G','P','S','O','L','Q','C','H'},
            {'W','O','S','I','H','K','M','R','Q','C','C','D','N','Z','V','G','R','D','Y','G','O','Y','A','N','P','T','I','Z','K','T','Z','Z'},
            {'T','G','L','C','F','N','W','P','I','S','G','B','V','T','J','P','A','C','R','Z','J','W','C','J','D','T','E','K','J','F','O','F'},
            {'N','M','X','T','L','K','D','N','J','W','O','T','S','R','V','J','U','S','A','W','F','Z','X','H','V','S','J','J','O','X','V','C'},
            {'M','F','Z','Q','C','V','S','U','M','B','V','P','V','T','Q','S','G','Q','V','X','N','J','D','P','V','O','S','X','B','J','P','E'},
            {'Q','X','Q','P','E','F','H','G','T','C','A','G','I','U','V','G','H','F','Y','D','K','C','W','V','Y','R','R','U','C','O','N','E'},
            {'M','R','H','T','I','S','U','S','H','Q','X','Y','A','N','F','L','U','S','N','C','I','A','E','N','Z','K','E','J','E','T','Q','T'},
            {'Y','Y','S','W','X','G','K','C','J','V','C','B','S','O','V','D','A','H','D','F','Z','V','D','O','E','W','S','C','C','P','A','U'},
            {'G','W','H','E','R','S','H','E','Y','S','C','Z','D','Q','T','F','F','K','B','I','W','G','E','T','W','U','F','U','J','S','T','S'},
            {'E','S','K','J','R','M','F','H','G','V','P','Y','P','K','B','D','I','E','F','U','L','D','C','I','Z','Q','T','G','P','N','W','R'},
            {'B','J','E','P','E','Z','O','S','X','S','V','P','G','W','Z','T','W','R','I','S','P','X','Q','Z','J','I','N','H','V','Z','N','N'},
            {'Q','B','B','X','E','P','F','V','C','Y','R','C','R','F','K','B','Q','Y','V','Z','S','U','P','H','G','V','S','H','L','B','T','K'},
            {'P','I','H','X','C','W','L','G','E','V','K','D','S','A','W','Q','U','D','W','X','D','O','J','H','N','D','U','E','Z','V','U','W'},
            {'V','R','I','H','E','R','Y','L','X','N','R','N','T','V','D','S','A','O','Q','R','B','G','N','E','J','D','R','O','U','J','W','R'},
            {'O','Q','H','Z','S','I','W','N','T','V','D','R','V','L','W','R','I','R','J','B','T','S','K','U','K','R','F','J','T','T','V','F'},
            {'M','V','G','M','L','A','K','Q','B','B','B','X','V','D','W','N','E','U','Z','H','P','J','S','R','X','I','M','U','V','H','Y','U'},
            {'V','O','P','P','F','X','B','J','U','M','E','P','P','U','C','G','M','Y','K','I','R','J','P','R','A','K','K','U','H','Z','V','E'},
            {'J','K','V','Q','A','M','I','W','D','T','R','E','R','Z','X','X','K','J','H','O','H','L','E','X','Q','P','M','X','C','D','J','Z'},
            {'W','S','X','Q','Z','J','A','W','D','J','H','E','D','J','I','B','T','X','W','H','T','W','E','V','Z','D','V','W','G','P','C','G'},
            {'M','Q','Y','B','I','S','A','B','T','T','I','A','C','Y','A','F','C','S','X','H','Q','R','H','O','W','E','M','C','H','E','Z','K'}
        };

        WordSearch wordSearch = new WordSearch("input.txt");

        [TestMethod]
        public void GetWordsFromFileReturnsArrayOfWords()
        {
            var expectedWords = locations.Keys.ToArray();
            Assert.IsTrue(wordSearch.Words.SequenceEqual(expectedWords));
        }

        [TestMethod]
        public void GetGridFromFileReturnsArrayOfLetters()
        {
            bool lettersAreSame = true;
            for (int row = 0; row < expectedLetters.GetLength(0); row++)
            {
                for (int col = 0; col < expectedLetters.GetLength(1); col++)
                {
                    if (wordSearch.Grid[row, col] != expectedLetters[row, col])
                    {
                        lettersAreSame = false;
                        row = int.MaxValue - 1;
                        break;
                    }
                }
            }
            Assert.IsTrue(lettersAreSame);
        }

        [TestMethod]
        public void FindHorizontalLeftToRightWordReturnsListOfLocations()
        {
            string wordToFind = "HERSHEYS";
            var expectedLocations = locations[wordToFind];
            var returnedLocations = wordSearch.FindWord(wordToFind);
            Assert.IsTrue(returnedLocations.SequenceEqual(expectedLocations));
        }

        [TestMethod]
        public void FindHorizontalRightToLeftWordReturnsListOfLocations()
        {
            string wordToFind = "CRUNCH";
            var expectedLocations = locations[wordToFind];
            var returnedLocations = wordSearch.FindWord(wordToFind);
            Assert.IsTrue(returnedLocations.SequenceEqual(expectedLocations));
        }

        [TestMethod]
        public void FindVerticalWordsInBothDirectionsReturnsListsOfLocations()
        {
            new List<string> { "REECES", "BUTTERFINGER" }.ForEach(wordToFind =>
            {
                var expectedLocations = locations[wordToFind];
                var returnedLocations = wordSearch.FindWord(wordToFind);
                Assert.IsTrue(returnedLocations.SequenceEqual(expectedLocations));
            });
        }

        [TestMethod]
        public void FindDiagonalWordsInAllFourDirectionsReturnsListsOfLocations()
        {
            new List<string> { "KITKAT", "TWIX", "MILKYWAY", "SNICKERS" }.ForEach(wordToFind =>
            {
                var expectedLocations = locations[wordToFind];
                var returnedLocations = wordSearch.FindWord(wordToFind);
                Assert.IsTrue(returnedLocations.SequenceEqual(expectedLocations));
            });
        }

        [TestMethod]
        public void FindAllWordsReturnsListsOfAllWordLocations()
        {
            var found = wordSearch.FindAllWords();
            foreach (var word in found.Keys)
                Assert.IsTrue(found[word].SequenceEqual(locations[word]));
        }

        [TestMethod]
        public void PrintCreatesFile()
        {
            string file = "output.txt";
            wordSearch.Print(file);
            Assert.IsTrue(File.Exists(file));
        }
    }
}