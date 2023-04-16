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
using Fenrir13.Printing;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Comparer;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Subsystems;

namespace Fenrir13.GamePlay;

internal sealed class GamePrerequisitesAssembler: IGamePrerequisitesAssembler
{
    private EventProvider eventProvider;
    private IPrintingSubsystem printingSubsystem;
    private IResourceProvider resourceProvider;
    private IHelpSubsystem helpSubsystem;
    private IGrammar grammar;
    private IVerbHandler verbHandler;
    private ScoreBoard scoreBoard;
    private Universe universe;

    public GamePrerequisitesAssembler()
    {
        InitializeSystem();
    }

    private void InitializeSystem()
    {
        this.resourceProvider = new ResourceProvider();
        this.printingSubsystem = new ConsolePrintingSubsystem();

        this.universe = new Universe(printingSubsystem, resourceProvider);
        this.scoreBoard = new ScoreBoard(printingSubsystem);
        this.eventProvider = new EventProvider(universe, printingSubsystem, scoreBoard);

        this.verbHandler = new GermanVerbHandler(universe, resourceProvider);
        this.grammar = new GermanGrammar(resourceProvider, verbHandler);
        this.helpSubsystem = new BaseHelpSubsystem(grammar, printingSubsystem);
    }

    public IPrintingSubsystem PrintingSubsystem
    {
        get => printingSubsystem;
        set => printingSubsystem = value;
    }

    public IResourceProvider ResourceProvider
    {
        get => resourceProvider;
        set => resourceProvider = value;
    }

    public IHelpSubsystem HelpSubsystem
    {
        get => helpSubsystem;
        set => helpSubsystem = value;
    }

    public IGrammar Grammar
    {
        get => grammar;
        set => grammar = value;
    }

    public IVerbHandler VerbHandler
    {
        get => verbHandler;
        set => verbHandler = value;
    }

    public ScoreBoard ScoreBoard
    {
        get => scoreBoard;
        set => scoreBoard = value;
    }

    public Universe Universe
    {
        get => universe;
        set => universe = value;
    }

    public void AssembleGame()
    {
        var locationMap = new LocationMap(new LocationComparer());

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
        
        locationMap.Add(cryoChamber, CryoChamberLocationMap(corridorEast));
        locationMap.Add(emptyChamberOne, new List<DestinationNode>());
        locationMap.Add(emptyChamberTwo, new List<DestinationNode>());
        locationMap.Add(corridorEast, CorridorEastLocationMap(cryoChamber, corridorMidEast, emptyChamberOne, emptyChamberTwo));
        locationMap.Add(corridorMidEast, CorridorMidEastLocationMap(corridorEast, corridorMid, socialRoom, kitchen));
        locationMap.Add(corridorMid, CorridorMidLocationMap(corridorMidEast, corridorMidWest, bridge, machineCorridorMid));
        locationMap.Add(corridorMidWest, CorridorMidWestLocationMap(corridorMid, corridorWest, gym, ambulance));
        locationMap.Add(corridorWest, CorridorWestLocationMap(corridorMidWest));
        locationMap.Add(bridge, BridgeLocationMap(corridorMid));
        locationMap.Add(computerTerminal, new List<DestinationNode>());
        locationMap.Add(machineCorridorMid, MachineCorridorMidLocationMap(corridorMid, airlock, engineRoom, equipmentRoom, maintenanceRoom));
        locationMap.Add(gym, GymLocationMap(corridorMidWest));
        locationMap.Add(ambulance, AmbulanceLocationMap(corridorMidWest));
        locationMap.Add(socialRoom, SocialRoomLocationMap(corridorMidEast));
        locationMap.Add(kitchen, KitchenLocationMap(corridorMidEast));
        locationMap.Add(airlock, AirlockLocationMap(machineCorridorMid, jetty));
        locationMap.Add(jetty, JettyLocationMap(airlock, roofTop, panelTop));
        locationMap.Add(roofTop, RoofTopLocationMap(jetty));
        locationMap.Add(panelTop, PanelTopLocationMap(jetty));
        locationMap.Add(engineRoom, EngineRoomLocationMap(machineCorridorMid));
        locationMap.Add(equipmentRoom, EquipmentRoomLocationMap(machineCorridorMid));
        locationMap.Add(maintenanceRoom, MaintenanceRoomLocationMap(machineCorridorMid));

        var activeLocation = cryoChamber;
        var activePlayer = PlayerPrerequisites.Get(this.eventProvider);
        var actualQuests = GetQuests();
        
        this.universe.LocationMap = locationMap;
        this.universe.ActiveLocation = activeLocation;
        this.universe.ActivePlayer = activePlayer;
        this.universe.Quests = actualQuests;
    }

    public void Restart()
    {
        InitializeSystem();
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