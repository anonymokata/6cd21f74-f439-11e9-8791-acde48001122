using System;
using System.Collections.Generic;
using System.IO;

namespace WordSearchKata
{
    public class WordSearch
    {
        public const char DELIMITER = ',';

        public static string[] GetWordsFromFile(string path)
        {
            using (var file = File.OpenRead(path))
            using (var stream = new StreamReader(file))
            {
                return stream.ReadLine().Split(DELIMITER);
            }
        }

        public static char[,] GetGridFromFile(string path)
        {
            using (var file = File.OpenRead(path))
            using (var stream = new StreamReader(file))
            {
                //skip first line with list of words
                stream.ReadLine();

                var lines = new List<string>();

                while (!stream.EndOfStream)
                    lines.Add(stream.ReadLine().Replace(DELIMITER.ToString(), ""));

                char[,] grid = new char[lines.Count, lines.Count];
                for (int row = 0; row < lines.Count; row++)
                {
                    for (int col = 0; col < lines.Count; col++)
                        grid[row, col] = lines[row][col];
                }
                return grid;
            }
        }
    }
}
