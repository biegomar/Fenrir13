using Fenrir13.Events;
using Fenrir13.GamePlay.Prerequisites.Airlock;
using Fenrir13.GamePlay.Prerequisites.Ambulance;
using Fenrir13.GamePlay.Prerequisites.CommandBridge;
using Fenrir13.GamePlay.Prerequisites.Corridor;
using Fenrir13.GamePlay.Prerequisites.CryoChamber;
using Fenrir13.GamePlay.Prerequisites.EngineRoom;
using Fenrir13.GamePlay.Prerequisites.EquipmentRoom;
using Fenrir13.GamePlay.Prerequisites.Gym;
using Fenrir13.GamePlay.Prerequisites.Jetty;
using Fenrir13.GamePlay.Prerequisites.Kitchen;
using Fenrir13.GamePlay.Prerequisites.MachineCorridor;
using Fenrir13.GamePlay.Prerequisites.MaintenanceRoom;
using Fenrir13.GamePlay.Prerequisites.PlayerConfig;
using Fenrir13.GamePlay.Prerequisites.SocialRoom;
using Fenrir13.Resources;
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
        var ambulance = AmbulancePrerequisites.Get(this.eventProvider);
        var kitchen = KitchenPrerequisites.Get(this.eventProvider);
        var airlock = AirlockPrerequisites.Get(this.eventProvider);
        var engineRoom = EngineRoomPrerequisites.Get(this.eventProvider);
        var equipmentRoom = EquipmentRoomPrerequisites.Get(this.eventProvider);
        var maintenanceRoom = MaintenanceRoomPrerequisites.Get(this.eventProvider);
        var jetty = JettyPrerequisites.Get(this.eventProvider);
        
        map.Add(cryoChamber, CryoChamberLocationMap(corridorEast));
        map.Add(corridorEast, CorridorEastLocationMap(cryoChamber, corridorMidEast, emptyChamberOne, emptyChamberTwo));
        map.Add(corridorMidEast, CorridorMidEastLocationMap(corridorEast, corridorMid, socialRoom, kitchen));
        map.Add(corridorMid, CorridorMidLocationMap(corridorMidEast, corridorMidWest, bridge, machineCorridorMid));
        map.Add(corridorMidWest, CorridorMidWestLocationMap(corridorMid, corridorWest, gym, ambulance));
        map.Add(corridorWest, CorridorWestLocationMap(corridorMidWest));
        map.Add(bridge, BridgeLocationMap(corridorMid));
        map.Add(computerTerminal, new List<DestinationNode>());
        map.Add(machineCorridorMid, MachineCorridorMidLocationMap(corridorMid, airlock, engineRoom, equipmentRoom, maintenanceRoom));
        map.Add(gym, GymLocationMap(corridorMidWest));
        map.Add(ambulance, AmbulanceLocationMap(corridorMidWest));
        map.Add(socialRoom, SocialRoomLocationMap(corridorMidEast));
        map.Add(kitchen, KitchenLocationMap(corridorMidEast));
        map.Add(airlock, AirlockLocationMap(machineCorridorMid, jetty));
        map.Add(jetty, JettyLocationMap(airlock));
        map.Add(engineRoom, EngineRoomLocationMap(machineCorridorMid));
        map.Add(equipmentRoom, EquipmentRoomLocationMap(machineCorridorMid));
        map.Add(maintenanceRoom, MaintenanceRoomLocationMap(machineCorridorMid));

        var activeLocation = cryoChamber;
        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        
        return (map, activeLocation, activePlayer);
    }

    private static IEnumerable<DestinationNode> MachineCorridorMidLocationMap(Location corridorMid, Location airlock, Location engineRoom, Location equipmentRoom, Location maintenanceRoom)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.UP, Location = corridorMid, IsHidden = false},
            new() {Direction = Directions.S, Location = airlock, IsHidden = false},
            new() {Direction = Directions.N, Location = engineRoom, IsHidden = false},
            new() {Direction = Directions.W, Location = equipmentRoom, IsHidden = false},
            new() {Direction = Directions.E, Location = maintenanceRoom, IsHidden = false},
            
        };
        return locationMap;  
    }
    
    private static IEnumerable<DestinationNode> EquipmentRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = machineCorridorMid, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> MaintenanceRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.W, Location = machineCorridorMid, IsHidden = false},
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> AirlockLocationMap(Location machineCorridorMid, Location jetty)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = machineCorridorMid, IsHidden = false},
            new() {Direction = Directions.S, Location = jetty, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> JettyLocationMap(Location airlock)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = airlock, IsHidden = false}
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> EngineRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = machineCorridorMid, IsHidden = false}
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> CryoChamberLocationMap(Location corridorEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.W, Location = corridorEast, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorEastLocationMap(Location cryoChamber, Location corridorMidEast, Location emptyChamberOne, Location emptyChamberTwo)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = cryoChamber, IsHidden = false},
            new() {Direction = Directions.W, Location = corridorMidEast, IsHidden = false},
            new() {Direction = Directions.S, Location = emptyChamberOne, IsHidden = false, DestinationDescription = Descriptions.EMPTYCREWCHAMBERONE_DESTINATION,},
            new() {Direction = Directions.N, Location = emptyChamberTwo, IsHidden = false, ShowInDescription = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorMidEastLocationMap(Location corridorEast, Location corridorMid, Location socialRoom, Location kitchen)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorEast, IsHidden = false, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorMid, IsHidden = false, DestinationDescription = Descriptions.CORRIDOR_DESTINATION},
            new() {Direction = Directions.S, Location = socialRoom, IsHidden = false},
            new() {Direction = Directions.N, Location = kitchen, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorMidLocationMap(Location corridorMidEast, Location corridorMidWest, Location bridge, Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidEast, IsHidden = false, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorMidWest, IsHidden = false, ShowInDescription = false},
            new() {Direction = Directions.UP, Location = bridge, IsHidden = false, DestinationDescription = Descriptions.BRIDGE_DESTINATION},
            new() {Direction = Directions.DOWN, Location = machineCorridorMid, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> BridgeLocationMap(Location corridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.DOWN, Location = corridorMid, IsHidden = false, DestinationDescription = Descriptions.COMMANDBRIDGE_TO_CORRIDOR_DESTINATION},
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> CorridorMidWestLocationMap(Location corridorMid, Location corridorWest, Location gym, Location ambulance)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMid, IsHidden = false, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorWest, IsHidden = false, DestinationDescription = Descriptions.CORRIDOR_DESTINATION},
            new() {Direction = Directions.S, Location = gym, IsHidden = false},
            new() {Direction = Directions.N, Location = ambulance, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> GymLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> AmbulanceLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> SocialRoomLocationMap(Location corridorMidEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidEast, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> KitchenLocationMap(Location corridorMidEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = corridorMidEast, IsHidden = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorWestLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidWest, IsHidden = false},
        };
        return locationMap;
    }
}