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
        var emptyChamberOne = EmptyCrewChamberOnePrerequisites.Get(this.eventProvider);
        var emptyChamberTwo = EmptyCrewChamberTwoPrerequisites.Get(this.eventProvider);
        var corridorMidEast = CorridorMidEastPrerequisites.Get(this.eventProvider);
        var corridorMid = CorridorMidPrerequisites.Get(this.eventProvider);
        var corridorMidWest = CorridorMidWestPrerequisites.Get(this.eventProvider);
        var corridorWest = CorridorWestPrerequisites.Get(this.eventProvider);
        
        map.Add(cryoChamber, CryoChamberLocationMap(corridorEast));
        map.Add(corridorEast, CorridorEastLocationMap(cryoChamber, corridorMidEast, emptyChamberOne, emptyChamberTwo));
        map.Add(corridorMidEast, CorridorMidEastLocationMap(corridorEast, corridorMid));
        map.Add(corridorMid, CorridorMidLocationMap(corridorMidEast, corridorMidWest));
        map.Add(corridorMidWest, CorridorMidWestLocationMap(corridorMid, corridorWest));
        map.Add(corridorWest, CorridorWestLocationMap(corridorMidWest));
        
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
    
    private static List<DestinationNode> CorridorEastLocationMap(Location cryoChamber, Location corridorMidEast, Location emptyChamberOne, Location emptyChamberTwo)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = cryoChamber, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMidEast, IsHidden = false},
            new() {Direction = Directions.S, Location = emptyChamberOne, IsHidden = false},
            new() {Direction = Directions.N, Location = emptyChamberTwo, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> CorridorMidEastLocationMap(Location corridorEast, Location corridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorEast, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMid, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> CorridorMidLocationMap(Location corridorMidEast, Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidEast, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }

    private static List<DestinationNode> CorridorMidWestLocationMap(Location corridorMid, Location corridorWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMid, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorWest, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> CorridorWestLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }
}