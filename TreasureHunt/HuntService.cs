using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TreasureHunt
{
    public class HuntService : IHuntService
    {
        private HuntContext _huntContext;

        public HuntService()
        {
        }

        public HuntContext LaunchHunt(IList<string> fileContent)
        {
            _huntContext = GetHuntContext(fileContent);
            var maxSequenceSize = _huntContext.Adventurers.Max(x => x.MovementSequence.Length);
            for (int i = 0; i < maxSequenceSize; i++)
            {
                MoveAdventurers(i);
            }
            Console.WriteLine("End of the hunt.");
            return _huntContext;
        }

        private void MoveAdventurers(int lapNumber)
        {
            foreach(var adventurer in _huntContext.Adventurers)
            {
                adventurer.Move(_huntContext, lapNumber);
            }
        }

        /// <summary>
        /// GetHuntContext permet d'initialiser l'objet GetHuntContext à partir du fichier .txt passé en entrée
        /// </summary>
        /// <param name="inputFileContent"></param>
        /// <returns></returns>
        private static HuntContext GetHuntContext(IList<string> inputFileContent)
        {
            var map = new Map(inputFileContent.Where(m => m.StartsWith("C")).Single());
            
            var adventurers = new List<Adventurer>();
            int position = 0;
            foreach(var adv in inputFileContent.Where(m => m.StartsWith("A")).ToList())
            {
                adventurers.Add(new Adventurer(adv, position));
                position++;
            }

            var mountains = new List<Mountain>();
            foreach (var moun in inputFileContent.Where(m => m.StartsWith("M")).ToList())
            {
                mountains.Add(new Mountain(moun));
            }

            var treasures = new List<Treasure>();
            foreach (var treas in inputFileContent.Where(m => m.StartsWith("T")).ToList())
            {
                treasures.Add(new Treasure(treas));
            }

            return new HuntContext
            {
                Map = map,
                Mountains = mountains,
                Treasures = treasures,
                Adventurers = adventurers
            };
        }
    }
}
