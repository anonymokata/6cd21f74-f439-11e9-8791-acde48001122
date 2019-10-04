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
    }
}
