using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasureHunt.Helpers;

namespace TreasureHunt.Test.Input
{
    public static class HuntData
    {
        private static readonly string _inputFilePath = $"{Directory.GetCurrentDirectory()}/Input/txt";

        public static IEnumerable<object[]> SingleAdventurerTreasureFound =>
          new List<object[]>
          {
               new object[]
               {
                   FileHelper.GetFileContent($"{_inputFilePath}/SingleAdventurerTreasureFound.txt")
               }
          };

        public static IEnumerable<object[]> MultipleAdventurersWithConflicts =>
          new List<object[]>
          {
               new object[]
               {
                   FileHelper.GetFileContent($"{_inputFilePath}/MultipleAdventurersWithConflicts.txt")
               }
          };
    }
}
