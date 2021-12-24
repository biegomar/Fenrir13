using Fenrir13.Events;
using Fenrir13.GamePlay.Prerequisites.Corridor;
using Fenrir13.GamePlay.Prerequisites.CryoChamber;
using Fenrir13.GamePlay.Prerequisites.PlayerConfig;
using Heretic.InteractiveFiction.Comparer;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay;

internal class GamePrerequisitesAssembler: IGamePrerequisitesAssembler
{
    private readonly EventProvider eventProvider;

    public GamePrerequisitesAssembler(EventProvider eventProvider)
    {
        this.eventProvider = eventProvider;
    }

    public (LocationMap map, Location activeLocation, Player activePlayer) AssembleGame()
    {
        var map = new LocationMap(new LocationComparer());

        var cryoChamber = CryoChamberPrerequisites.Get(this.eventProvider);
        var corridorEast = CorridorEastPrerequisites.Get(this.eventProvider);
        
        map.Add(cryoChamber, CryoChamberLocationMap(corridorEast));
        map.Add(corridorEast, CorridorEastLocationMap(cryoChamber));
        
        var activeLocation = cryoChamber;
        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        
        return (map, activeLocation, activePlayer);
    }
    
    private static List<DestinationNode> CryoChamberLocationMap(Location corridorEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.W, Location = corridorEast, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> CorridorEastLocationMap(Location cryoChamber)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = cryoChamber, IsHidden = false},
        };
        return locationMap;
    }
}