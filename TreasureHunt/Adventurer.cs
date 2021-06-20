using System.Collections.Generic;
using System.Linq;

namespace TreasureHunt
{
    public class Adventurer
    {
        public Adventurer(string adventurerConfiguration, int positionInFile)
        {
            IList<string> conf = adventurerConfiguration.Split("-");
            Name = conf[1];
            XPosition = int.Parse(conf[2]);
            YPosition = int.Parse(conf[3]);
            Orientation = conf[4];
            MovementSequence = conf[5];
            PositionInFile = positionInFile;
        }

        // Constructeur pour les TUs
        public Adventurer(string adventurerConfiguration)
        {
            IList<string> conf = adventurerConfiguration.Split("-");
            Name = conf[1];
            XPosition = int.Parse(conf[2]);
            YPosition = int.Parse(conf[3]);
            Orientation = conf[4];
            FoundTreasuresCount = int.Parse(conf[5]);
        }

        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int NextXPosition { get; set; }
        public int NextYPosition { get; set; }
        public string Orientation { get; set; }
        public string MovementSequence { get; set; }
        public int PositionInFile { get; set; }
        public int FoundTreasuresCount { get; set; }

        public override string ToString()
        {
            return string.Concat($"A-{Name}-{XPosition}-{YPosition}-{Orientation}-{FoundTreasuresCount}");
        }

        public void Move(HuntContext huntContext, int lapNumber)
        {
            var movement = GetMovement(lapNumber);
            switch(movement)
            {
                case "A":
                    MoveForward(huntContext);
                    break;
                case "G":
                    ChangeOrientation("G");
                    break;
                case "D":
                    ChangeOrientation("D");
                    break;
                case "EndOfHunt":
                    break;
            }
        }

        private string GetMovement(int lapNumber)
        {
            if (lapNumber < MovementSequence.Length)
                return MovementSequence[lapNumber].ToString();
            return "EndOfHunt";
        }

        private void MoveForward(HuntContext huntContext)
        {
            switch (Orientation)
            {
                case "N":
                    MoveNorth(huntContext);
                    break;
                case "S":
                    MoveSouth(huntContext);
                    break;
                case "E":
                    MoveEast(huntContext);
                    break;
                case "O":
                    MoveWest(huntContext);
                    break;
            }
        }

        private void ChangeOrientation(string direction)
        {
            if(direction == "G")
            {
                switch(Orientation)
                {
                    case "N":
                        Orientation = "O";
                        break;
                    case "S":
                        Orientation = "E";
                        break;
                    case "E":
                        Orientation = "N";
                        break;
                    case "O":
                        Orientation = "S";
                        break;
                }
            }
            else if (direction == "D")
            {
                switch (Orientation)
                {
                    case "N":
                        Orientation = "E";
                        break;
                    case "S":
                        Orientation = "O";
                        break;
                    case "E":
                        Orientation = "S";
                        break;
                    case "O":
                        Orientation = "N";
                        break;
                }
            }
        }

        private void MoveNorth(HuntContext huntContext)
        {
            NextYPosition = YPosition - 1;
            NextXPosition = XPosition;
            if (NextYPosition < 0 || HasMountain(huntContext, XPosition, NextYPosition) || IsBloquedByAnotherAdventurer(huntContext))
                return;
            YPosition--;
            FindTreasure(huntContext);
        }
        private void MoveSouth(HuntContext huntContext)
        {
            NextYPosition = YPosition + 1;
            NextXPosition = XPosition;
            if (NextXPosition > huntContext.Map.Height - 1 || HasMountain(huntContext, XPosition, NextXPosition) || IsBloquedByAnotherAdventurer(huntContext))
                return;
            YPosition++;
            FindTreasure(huntContext);
        }

        private void MoveEast(HuntContext huntContext)
        {
            NextYPosition = YPosition;
            NextXPosition = XPosition + 1;
            if (NextXPosition > huntContext.Map.Width - 1 || HasMountain(huntContext, NextXPosition, YPosition) || IsBloquedByAnotherAdventurer(huntContext))
                return;
            XPosition++;
            FindTreasure(huntContext);
        }

        private void MoveWest(HuntContext huntContext)
        {
            NextYPosition = YPosition;
            NextXPosition = XPosition - 1;
            if (NextXPosition < 0 || HasMountain(huntContext, NextXPosition, YPosition) || IsBloquedByAnotherAdventurer(huntContext))
                return;
            XPosition--;
            FindTreasure(huntContext);
        }

        private bool HasMountain(HuntContext huntContext, int xPosition, int yPosition)
        {
            return huntContext.Mountains.Any(m => m.XPosition == xPosition && m.YPosition == yPosition);
        }

        private void FindTreasure(HuntContext huntContext)
        {
            var foundTreasurer = huntContext.Treasures.Where(t => t.XPosition == XPosition && t.YPosition == YPosition && t.TreasuresCount > 0).SingleOrDefault();
            if (foundTreasurer != null)
            {
                FoundTreasuresCount++;
                foundTreasurer.TreasuresCount--;
            }
        }

        private bool IsBloquedByAnotherAdventurer(HuntContext huntContext)
        {
            var adventurerInConflict = huntContext.Adventurers.Where(a => a.Name != Name && a.NextXPosition == NextXPosition && a.NextYPosition == NextYPosition).FirstOrDefault();
            if (adventurerInConflict != null)
                return true;
            return false;
        }
    }
}
