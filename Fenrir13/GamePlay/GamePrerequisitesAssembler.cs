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
using Fenrir13.GamePlay.Prerequisites.PanelTop;
using Fenrir13.GamePlay.Prerequisites.PlayerConfig;
using Fenrir13.GamePlay.Prerequisites.RoofTop;
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

    public Universe AssembleGame(Universe universe)
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
        var roofTop = RoofTopPrerequisites.Get(this.eventProvider);
        var panelTop = PanelTopPrerequisites.Get(this.eventProvider);
        
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
        map.Add(jetty, JettyLocationMap(airlock, roofTop, panelTop));
        map.Add(roofTop, RoofTopLocationMap(jetty));
        map.Add(panelTop, PanelTopLocationMap(jetty));
        map.Add(engineRoom, EngineRoomLocationMap(machineCorridorMid));
        map.Add(equipmentRoom, EquipmentRoomLocationMap(machineCorridorMid));
        map.Add(maintenanceRoom, MaintenanceRoomLocationMap(machineCorridorMid));

        var activeLocation = cryoChamber;
        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        var actualQuests = GetQuests();
        
        universe.LocationMap = map;
        universe.ActiveLocation = activeLocation;
        universe.ActivePlayer = activePlayer;
        universe.Quests = actualQuests;
        
        return universe;
    }

    private static ICollection<string> GetQuests()
    {
        var result = new List<string>
        {
            MetaData.QUEST_I, 
            MetaData.QUEST_II, 
            MetaData.QUEST_III,
            MetaData.QUEST_IV,
            MetaData.QUEST_V,
            MetaData.QUEST_VI,
            MetaData.QUEST_VII,
            MetaData.QUEST_VIII,
            MetaData.QUEST_IX
        };

        return result;
    }

    private static IEnumerable<DestinationNode> MachineCorridorMidLocationMap(Location corridorMid, Location airlock, Location engineRoom, Location equipmentRoom, Location maintenanceRoom)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.UP, Location = corridorMid, DestinationDescription = Descriptions.CORRIDOR_MID_DESTINATION},
            new() {Direction = Directions.S, Location = airlock, DestinationDescription = Descriptions.AIRLOCK_DESTINATION},
            new() {Direction = Directions.N, Location = engineRoom, DestinationDescription = Descriptions.ENGINE_ROOM_DESTINATION},
            new() {Direction = Directions.W, Location = equipmentRoom, ShowInDescription = false},
            new() {Direction = Directions.E, Location = maintenanceRoom, ShowInDescription = false}
            
        };
        return locationMap;  
    }
    
    private static IEnumerable<DestinationNode> EquipmentRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = machineCorridorMid},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> MaintenanceRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.W, Location = machineCorridorMid},
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> AirlockLocationMap(Location machineCorridorMid, Location jetty)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = machineCorridorMid},
            new() {Direction = Directions.S, Location = jetty},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> JettyLocationMap(Location airlock, Location roofTop, Location panelTop)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = airlock},
            new() {Direction = Directions.UP, Location = roofTop, DestinationDescription = Descriptions.ROOF_TOP_DESTINATION},
            new() {Direction = Directions.DOWN, Location = panelTop, ShowInDescription = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> RoofTopLocationMap(Location jetty)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.DOWN, Location = jetty, DestinationDescription = Descriptions.ROOF_TOP_WAY_TO_JETTY}
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> PanelTopLocationMap(Location jetty)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.UP, Location = jetty, DestinationDescription = Descriptions.PANEL_TOP_WAY_TO_JETTY}
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> EngineRoomLocationMap(Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = machineCorridorMid}
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> CryoChamberLocationMap(Location corridorEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.W, Location = corridorEast},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorEastLocationMap(Location cryoChamber, Location corridorMidEast, Location emptyChamberOne, Location emptyChamberTwo)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = cryoChamber},
            new() {Direction = Directions.W, Location = corridorMidEast},
            new() {Direction = Directions.S, Location = emptyChamberOne, DestinationDescription = Descriptions.EMPTYCREWCHAMBERONE_DESTINATION,},
            new() {Direction = Directions.N, Location = emptyChamberTwo, ShowInDescription = false},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorMidEastLocationMap(Location corridorEast, Location corridorMid, Location socialRoom, Location kitchen)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorEast, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorMid, DestinationDescription = Descriptions.CORRIDOR_DESTINATION},
            new() {Direction = Directions.S, Location = socialRoom},
            new() {Direction = Directions.N, Location = kitchen},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorMidLocationMap(Location corridorMidEast, Location corridorMidWest, Location bridge, Location machineCorridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidEast, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorMidWest, ShowInDescription = false},
            new() {Direction = Directions.UP, Location = bridge, DestinationDescription = Descriptions.BRIDGE_DESTINATION},
            new() {Direction = Directions.DOWN, Location = machineCorridorMid, DestinationDescription = Descriptions.MACHINE_DESTINATION},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> BridgeLocationMap(Location corridorMid)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.DOWN, Location = corridorMid, DestinationDescription = Descriptions.COMMANDBRIDGE_TO_CORRIDOR_DESTINATION},
        };
        return locationMap;
    }

    private static IEnumerable<DestinationNode> CorridorMidWestLocationMap(Location corridorMid, Location corridorWest, Location gym, Location ambulance)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMid, ShowInDescription = false},
            new() {Direction = Directions.W, Location = corridorWest, DestinationDescription = Descriptions.CORRIDOR_DESTINATION},
            new() {Direction = Directions.S, Location = gym},
            new() {Direction = Directions.N, Location = ambulance},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> GymLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidWest},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> AmbulanceLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = corridorMidWest},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> SocialRoomLocationMap(Location corridorMidEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.N, Location = corridorMidEast},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> KitchenLocationMap(Location corridorMidEast)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.S, Location = corridorMidEast},
        };
        return locationMap;
    }
    
    private static IEnumerable<DestinationNode> CorridorWestLocationMap(Location corridorMidWest)
    {
        var locationMap = new List<DestinationNode>
        {
            new() {Direction = Directions.E, Location = corridorMidWest},
        };
        return locationMap;
    }
}