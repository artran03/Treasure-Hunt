using System.Collections.Generic;

namespace TreasureHunt
{
    public class Mountain
    {
        public Mountain(string mountConfiguration)
        {
            IList<string> conf = mountConfiguration.Split("-");
            XPosition = int.Parse(conf[1]);
            YPosition = int.Parse(conf[2]);
        }

        public int XPosition { get; set; }

        public int YPosition { get; set; }

        public override string ToString()
        {
            return string.Concat($"M-{XPosition}-{YPosition}");
        }
    }
}
