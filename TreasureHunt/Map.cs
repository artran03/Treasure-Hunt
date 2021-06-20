using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureHunt
{
    public class Map
    {
        public Map(string mapConfiguration)
        {
            IList<string> conf = mapConfiguration.Split("-");
            Width = int.Parse(conf[1]);
            Height = int.Parse(conf[2]);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return string.Concat($"C-{Width}-{Height}");
        }
    }
}
