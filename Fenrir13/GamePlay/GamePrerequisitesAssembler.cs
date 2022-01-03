using Fenrir13.Events;
using Fenrir13.GamePlay.Prerequisites.CommandBridge;
using Fenrir13.GamePlay.Prerequisites.Corridor;
using Fenrir13.GamePlay.Prerequisites.CryoChamber;
using Fenrir13.GamePlay.Prerequisites.Gym;
using Fenrir13.GamePlay.Prerequisites.MachineCorridor;
using Fenrir13.GamePlay.Prerequisites.PlayerConfig;
using Fenrir13.GamePlay.Prerequisites.SocialRoom;
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
        var bridge = CommandBridgePrerequisites.Get(this.eventProvider);
        var computerTerminal = ComputerTerminalPrerequisites.Get(this.eventProvider);
        var machineCorridorMid = MachineCorridorMidPrerequisites.Get(this.eventProvider);
        var gym = GymPrerequisites.Get(this.eventProvider);
        var socialRoom = SocialRoomPrerequisites.Get(this.eventProvider);
        
        map.Add(cryoChamber, CryoChamberLocationMap(corridorEast));
        map.Add(corridorEast, CorridorEastLocationMap(cryoChamber, corridorMidEast, emptyChamberOne, emptyChamberTwo));
        map.Add(corridorMidEast, CorridorMidEastLocationMap(corridorEast, corridorMid, socialRoom));
        map.Add(corridorMid, CorridorMidLocationMap(corridorMidEast, corridorMidWest, bridge, machineCorridorMid));
        map.Add(corridorMidWest, CorridorMidWestLocationMap(corridorMid, corridorWest, gym));
        map.Add(corridorWest, CorridorWestLocationMap(corridorMidWest));
        map.Add(bridge, BridgeLocationMap(corridorMid));
        map.Add(computerTerminal, new List<DestinationNode>());
        map.Add(machineCorridorMid, MachineCorridorMidLocationMap(corridorMid));
        map.Add(gym, GymLocationMap(corridorMidWest));
        map.Add(socialRoom, SocialRoomLocationMap(corridorMidEast));
        
        var activeLocation = cryoChamber;
        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        
        return (map, activeLocation, activePlayer);
    }

    private static IEnumerable<DestinationNode> MachineCorridorMidLocationMap(Location corridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.UP, Location = corridorMid, IsHidden = false},
        };
        return locationMap;  
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
    
    private static List<DestinationNode> CorridorMidEastLocationMap(Location corridorEast, Location corridorMid, Location socialRoom)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorEast, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMid, IsHidden = false},
            new() {Direction = Directions.S, Location = socialRoom, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> CorridorMidLocationMap(Location corridorMidEast, Location corridorMidWest, Location bridge, Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidEast, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMidWest, IsHidden = false},
            new() {Direction = Directions.UP, Location = bridge, IsHidden = false},
            new() {Direction = Directions.DOWN, Location = machineCorridorMid, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> BridgeLocationMap(Location corridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.DOWN, Location = corridorMid, IsHidden = false},
        };
        return locationMap;
    }

    private static List<DestinationNode> CorridorMidWestLocationMap(Location corridorMid, Location corridorWest, Location gym)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMid, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorWest, IsHidden = false},
            new() {Direction = Directions.S, Location = gym, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> GymLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }
    
    private static List<DestinationNode> SocialRoomLocationMap(Location corridorMidEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidEast, IsHidden = false},
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