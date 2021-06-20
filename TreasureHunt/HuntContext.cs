using System.Collections.Generic;

namespace TreasureHunt
{
    public class HuntContext
    {
        public Map Map { get; set; }
        public IList<Mountain> Mountains { get; set; }
        public IList<Treasure> Treasures { get; set; }
        public IList<Adventurer> Adventurers { get; set; }
    }
}
