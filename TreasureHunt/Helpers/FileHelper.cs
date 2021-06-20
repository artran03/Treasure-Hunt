using System.Collections.Generic;
using System.IO;

namespace TreasureHunt.Helpers
{
    public class FileHelper
    {
        public static IList<string> GetFileContent(string filePath)
        {
            return File.ReadAllLines(filePath);
        }
    }
}
