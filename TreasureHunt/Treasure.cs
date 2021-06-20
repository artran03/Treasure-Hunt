using System.Collections.Generic;

namespace TreasureHunt
{
    public class Treasure
    {
        public Treasure(string treasConfiguration)
        {
            IList<string> conf = treasConfiguration.Split("-");
            XPosition = int.Parse(conf[1]);
            YPosition = int.Parse(conf[2]);
            TreasuresCount = int.Parse(conf[3]);
        }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public int TreasuresCount { get; set; }

        public override string ToString()
        {
            return string.Concat($"T-{XPosition}-{YPosition}-{TreasuresCount}");
        }
    }
}
