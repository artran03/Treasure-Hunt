using System.Collections.Generic;

namespace TreasureHunt
{
    public interface IHuntService
    {        
        HuntContext LaunchHunt(IList<string> fileContent);
    }
}
