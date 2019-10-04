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

        public static List<(int, int)> FindWordHorizontal(string word, char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    //if first letter found and enough space on the line for the word to fit
                    if (grid[row, col] == word[0])
                    { 
                        for (int direction = -1; direction <= 1; direction += 2)
                        {
                            var locations = new List<(int, int)>() { (col, row) };
                            int end = col + (word.Length - 1) * direction;
                            int left = Math.Min(col, end);
                            int right = Math.Max(col, end);

                            //skip check if word can't fit within bounds in given direction
                            if (left >= 0 && right < grid.GetLength(1))
                            {
                                bool found = true;
                                for (int i = 1; i < word.Length; i++)
                                {
                                    int newCol = direction == 1 ? left + i : right - i;
                                    if (grid[row, newCol] != word[i])
                                    {
                                        found = false;
                                        break;
                                    }
                                    locations.Add((newCol, row));
                                }

                                if (found)
                                    return locations;
                            }
                        }
                    }
                }
            }
            return new List<(int, int)>();
        }

        public static List<(int, int)> FindWordVertical(string word, char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    //if first letter found and enough space on the line for the word to fit
                    if (grid[row, col] == word[0])
                    {
                        for (int direction = -1; direction <= 1; direction += 2)
                        {
                            var locations = new List<(int, int)>() { (col, row) };
                            int end = row + (word.Length - 1) * direction;
                            int top = Math.Min(row, end);
                            int bottom = Math.Max(row, end);

                            //skip check if word can't fit within bounds in given direction
                            if (top >= 0 && bottom < grid.GetLength(0))
                            {
                                bool found = true;
                                for (int i = 1; i < word.Length; i++)
                                {
                                    int newRow = direction == 1 ? top + i : bottom - i;
                                    if (grid[newRow, col] != word[i])
                                    {
                                        found = false;
                                        break;
                                    }
                                    locations.Add((col, newRow));
                                }

                                if (found)
                                    return locations;
                            }
                        }
                    }
                }
            }
            return new List<(int, int)>();
        }

        public static List<(int, int)> FindWordDiagonal(string word, char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    //if first letter found and enough space on the line for the word to fit
                    if (grid[row, col] == word[0])
                    {
                        for (int directionX = -1; directionX <= 1; directionX += 2)
                        {
                            for (int directionY = -1; directionY <= 1; directionY += 2)
                            {
                                var locations = new List<(int, int)>() { (col, row) };
                                int endX = col + (word.Length - 1) * directionX;
                                int endY = row + (word.Length - 1) * directionY;

                                int left = Math.Min(col, endX);
                                int right = Math.Max(col, endX);
                                int top = Math.Min(row, endY);
                                int bottom = Math.Max(row, endY);

                                //skip check if word can't even fit within bounds in given direction
                                if (left >= 0 && right < grid.GetLength(1) && top >= 0 && bottom < grid.GetLength(0))
                                {
                                    bool found = true;
                                    for (int i = 1; i < word.Length; i++)
                                    {
                                        int newCol = directionX == 1 ? left + i : right - i;
                                        int newRow = directionY == 1 ? top + i : bottom - i;

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
                }
            }
            return new List<(int, int)>();
        }
    }
}
