using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        public static List<(int, int)> FindWord(string word, char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    //if first letter found, search the eight directions for the remaining letters, otherwise move on
                    if (grid[row, col] != word[0])
                        continue;

                    //-1: left-right, 0: constant, 1: right-left
                    for (int directionX = -1; directionX <= 1; directionX++)
                    {
                        //-1: bottom-top, 0: constant, 1: top-bottom
                        for (int directionY = -1; directionY <= 1; directionY++)
                        {
                            //skip this iteration if both directions are 0 (not a valid word orientation)
                            if (directionX == 0 && directionY == 0)
                                continue;

                            int endX = col + (word.Length - 1) * directionX;
                            int endY = row + (word.Length - 1) * directionY;
                            int left = Math.Min(col, endX);
                            int right = Math.Max(col, endX);
                            int top = Math.Min(row, endY);
                            int bottom = Math.Max(row, endY);

                            //skip check if word can't fit within bounds in given direction
                            if (left < 0 || right >= grid.GetLength(1) || top < 0 || bottom >= grid.GetLength(0))
                                continue;

                            var locations = new List<(int, int)>() { (col, row) };
                            bool found = true;
                            for (int i = 1; i < word.Length; i++)
                            {
                                int newCol = directionX == 0 ? left : left + i;
                                if (directionX < 0)
                                    newCol = right - i;

                                int newRow = directionY == 0 ? top : top + i;
                                if (directionY < 0)
                                    newRow = bottom - i;

                                if (grid[newRow, newCol] != word[i])
                                {
                                    found = false;
                                    break;
                                }
                                locations.Add((newCol, newRow));
                            }

                            if (found)
                                return locations;
                        }
                    }
                }
            }
            return new List<(int, int)>();
        }
    }
}
