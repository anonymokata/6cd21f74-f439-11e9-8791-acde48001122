using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordSearchKata
{
    public class WordSearch
    {
        public const char DELIMITER = ',';

        public string[] Words { get; private set; }
        public char[,] Grid { get; private set; }

        public WordSearch(string path)
        {
            LoadFromFile(path);
        }

        private void LoadFromFile(string path)
        {
            using (var file = File.OpenRead(path))
            using (var stream = new StreamReader(file))
            {
                Words = stream.ReadLine().Split(DELIMITER);

                var lines = new List<string>();
                while (!stream.EndOfStream)
                    lines.Add(stream.ReadLine().Replace(DELIMITER.ToString(), ""));

                Grid = new char[lines.Count, lines.Count];
                for (int row = 0; row < lines.Count; row++)
                {
                    for (int col = 0; col < lines.Count; col++)
                        Grid[row, col] = lines[row][col];
                }
            }
        }

        public List<(int, int)> FindWord(string word)
        {
            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    //if first letter found, search the eight directions for the remaining letters, otherwise move on
                    if (Grid[row, col] != word[0])
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
                            if (left < 0 || right >= Grid.GetLength(1) || top < 0 || bottom >= Grid.GetLength(0))
                                continue;

                            var locations = new List<(int, int)>() { (col, row) };
                            bool found = true;
                            for (int i = 1; i < word.Length; i++)
                            {
                                int newRow = directionY == 0 ? top : top + i;
                                newRow = directionY < 0 ? bottom - i : newRow;

                                int newCol = directionX == 0 ? left : left + i;
                                newCol = directionX < 0 ? right - i : newCol;

                                if (Grid[newRow, newCol] != word[i])
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
