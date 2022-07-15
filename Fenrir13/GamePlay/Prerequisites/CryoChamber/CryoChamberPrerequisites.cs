using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CryoChamber;

internal static class CryoChamberPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var cryoChamber = new Location()
        {
            Key = Keys.CRYOCHAMBER,
            Name = Locations.CRYOCHAMBER,
            Description = Descriptions.CRYOCHAMBER,
            FirstLookDescription = Descriptions.CRYOCHAMBER_FIRSTLOOK
        };

        cryoChamber.Items.Add(GetChocolateBar(eventProvider));
        cryoChamber.Items.Add(GetSpaceSuite(eventProvider));

        AddSurroundings(cryoChamber);

        AddAfterLookEvents(cryoChamber, eventProvider);

        AddChangeLocationEvents(cryoChamber, eventProvider);

        AddAfterOpenEvents(cryoChamber, eventProvider);
        
        AddSitDownEvents(cryoChamber, eventProvider);
        
        AddTakeEvents(cryoChamber, eventProvider);

        return cryoChamber;
    }

    private static Item GetChocolateBar(EventProvider eventProvider)
    {
        var bar = new Item
        {
            Key = Keys.CHOCOLATEBAR,
            Description = Descriptions.CHOCOLATEBAR,
            Name = Items.CHOCOLATEBAR,
            IsEatable = true,
            IsHidden = true,
            IsUnveilAble = false,
            Grammar = new Grammars(Genders.Male)
        };

        AddAfterEatEvents(bar, eventProvider);

        return bar;
    }

    private static Item GetSpaceSuite(EventProvider eventProvider)
    {
        var spaceSuite = new Item
        {
            Key = Keys.SPACE_SUIT,
            Description = Descriptions.SPACE_SUIT,
            Name = Items.SPACE_SUIT,
            IsDropAble = false,
            ContainmentDescription = Descriptions.SPACE_SUIT_CONTAINMENT,
            UnDropAbleDescription = Descriptions.SPACE_SUIT_UNDROPABLE,
            Grammar = new Grammars(Genders.Male)
        };

        AddAfterTakeEvents(spaceSuite, eventProvider);

        return spaceSuite;
    }

    private static void AddSurroundings(Location location)
    {
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(wall);
        
        var floor = new Item()
        {
            Key = Keys.CHAMBER_FLOOR,
            Name = Items.CHAMBER_FLOOR,
            Description = Descriptions.CHAMBER_FLOOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(floor);
        
        var table = new Item()
        {
            Key = Keys.TABLE,
            Name = Items.TABLE,
            Description = Descriptions.TABLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(table);
        
        var chair = new Item()
        {
            Key = Keys.CHAIR,
            Name = Items.CHAIR,
            Description = Descriptions.CHAIR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(chair);
        
        var pod = new Item()
        {
            Key = Keys.CRYOPOD,
            Name = Items.CRYOPOD,
            Description = Descriptions.CRYOPOD,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(pod);
        
        var bed = new Item()
        {
            Key = Keys.CRYOPOD_BED,
            Name = Items.CRYOPOD_BED,
            Description = Descriptions.CRYOPOD,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(bed);
        
        var workbench = new Item()
        {
            Key = Keys.WORKBENCH,
            Name = Items.WORKBENCH,
            Description = Descriptions.WORKBENCH,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(workbench);
        
        var laptop = new Item()
        {
            Key = Keys.LAPTOP,
            Name = Items.LAPTOP,
            Description = Descriptions.LAPTOP,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(laptop);
        
        var pierHole = new Item()
        {
            Key = Keys.PIERHOLE,
            Name = Items.PIERHOLE,
            Description = Descriptions.PIERHOLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(pierHole);
        
        var officeChair = new Item()
        {
            Key = Keys.OFFICECHAIR,
            Name = Items.OFFICECHAIR,
            Description = Descriptions.OFFICECHAIR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(officeChair);
        
        var closet = new Item()
        {
            Key = Keys.CLOSET,
            Name = Items.CLOSET,
            Description = Descriptions.CLOSET,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(closet);
        
        var drawer = new Item()
        {
            Key = Keys.DRAWER,
            Name = Items.DRAWER,
            Description = Descriptions.DRAWER,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(drawer);
        
        var closetDoor = new Item()
        {
            Key = Keys.CLOSET_DOOR,
            Name = Items.CLOSET_DOOR,
            Description = Descriptions.CLOSET_DOOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(closetDoor);
        
        var wardrobe = new Item()
        {
            Key = Keys.WARDROBE,
            Name = Items.WARDROBE,
            Description = Descriptions.WARDROBE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(wardrobe);
        
        var partitionWall = new Item()
        {
            Key = Keys.PARTITION_WALL,
            Name = Items.PARTITION_WALL,
            Description = Descriptions.PARTITION_WALL,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(partitionWall);
        
        var washingArea = new Item()
        {
            Key = Keys.WASHINGAREA,
            Name = Items.WASHINGAREA,
            Description = Descriptions.WASHINGAREA,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(washingArea);
        
        var washingAreaBasin = new Item()
        {
            Key = Keys.WASHINGAREA_BASIN,
            Name = Items.WASHINGAREA_BASIN,
            Description = Descriptions.WASHINGAREA,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(washingAreaBasin);
        
        var toilet = new Item()
        {
            Key = Keys.TOILET,
            Name = Items.TOILET,
            Description = Descriptions.TOILET,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(toilet);
        
        var toiletSeat = new Item()
        {
            Key = Keys.TOILET_SEAT,
            Name = Items.TOILET_SEAT,
            Description = Descriptions.TOILET_SEAT,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(toiletSeat);
        
        var mirror = new Item()
        {
            Key = Keys.MIRROR,
            Name = Items.MIRROR,
            Description = Descriptions.MIRROR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(mirror);
        
        var outlines = new Item()
        {
            Key = Keys.OUTLINES,
            Name = Items.OUTLINES,
            Description = Descriptions.OUTLINES,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(outlines);
        
        var peephole = new Item()
        {
            Key = Keys.PEEPHOLE,
            Name = Items.PEEPHOLE,
            Description = Descriptions.PEEPHOLE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(peephole);
        
        var latch = new Item()
        {
            Key = Keys.LATCH,
            Name = Items.LATCH,
            Description = Descriptions.LATCH,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(latch);
        
        var door = new Item()
        {
            Key = Keys.CRYOCHAMBER_DOOR,
            Name = Items.CRYOCHAMBER_DOOR,
            Description = Descriptions.CRYOCHAMBER_DOOR,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(door);
        
        var display = new Item()
        {
            Key = Keys.DISPLAY,
            Name = Items.DISPLAY,
            Description = Descriptions.DISPLAY,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(display);
        
        var materials = new Item()
        {
            Key = Keys.WRITING_MATERIALS,
            Name = Items.WRITING_MATERIALS,
            Description = Descriptions.WRITING_MATERIALS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(materials);
        
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(ceiling);
        
        var bulkhead = new Item()
        {
            Key = Keys.CRYOCHAMBER_BULKHEAD,
            Name = Items.CRYOCHAMBER_BULKHEAD,
            Description = Descriptions.CRYOCHAMBER_BULKHEAD,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(bulkhead);
        
        var lamp = new Item()
        {
            Key = Keys.CRYOCHAMBER_LAMP,
            Name = Items.CRYOCHAMBER_LAMP,
            Description = Descriptions.CRYOCHAMBER_LAMP,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(lamp);
        
        var cloths = new Item()
        {
            Key = Keys.CRYOCHAMBER_CLOTHS,
            Name = Items.CRYOCHAMBER_CLOTHS,
            Description = Descriptions.CRYOCHAMBER_CLOTHS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(cloths);
        
        var proxima = new Item()
        {
            Key = Keys.PROXIMA_CENTAURI,
            Name = Items.PROXIMA_CENTAURI,
            Description = Descriptions.PROXIMA_CENTAURI,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(proxima);
        
        var paper = new Item()
        {
            Key = Keys.PAPER,
            Name = Items.PAPER,
            Description = Descriptions.PAPER,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(paper);
        
        var pencils = new Item()
        {
            Key = Keys.PENCILS,
            Name = Items.PENCILS,
            Description = Descriptions.PENCILS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male, isSingular: false)
        };
        location.Items.Add(pencils);
        
        var pencilOne = new Item()
        {
            Key = Keys.PENCIL_I,
            Name = Items.PENCIL_I,
            Description = Descriptions.PENCIL_I,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(pencilOne);
        
        var pencilTwo = new Item()
        {
            Key = Keys.PENCIL_II,
            Name = Items.PENCIL_II,
            Description = Descriptions.PENCIL_II,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(pencilTwo);
        
        var felt = new Item()
        {
            Key = Keys.FELT,
            Name = Items.FELT,
            Description = Descriptions.FELT,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(felt);
        
        var wolf = new Item()
        {
            Key = Keys.PANEL_TOP_WOLF,
            Name = Items.PANEL_TOP_WOLF,
            Description = Descriptions.PANEL_TOP_WOLF,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(wolf);
    }

    private static void AddAfterLookEvents(Location cryoChamber, EventProvider eventProvider)
    {
        cryoChamber.AfterLook += eventProvider.LookAtClosetDoor;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtClosetDoor), 1);

        cryoChamber.AfterLook += eventProvider.LookAtDisplay;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtDisplay), 1);
    }

    private static void AddAfterEatEvents(Item chocolateBar, EventProvider eventProvider)
    {
        chocolateBar.AfterEat += eventProvider.EatChocolateBar;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.EatChocolateBar), 5);
    }

    private static void AddAfterTakeEvents(Item item, EventProvider eventProvider)
    {
        item.AfterTake += eventProvider.TakeSpaceSuite;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.TakeSpaceSuite), 1);
    }

    private static void AddAfterOpenEvents(Location room, EventProvider eventProvider)
    {
        room.AfterOpen += eventProvider.TryToOpenClosedDoor;
        room.Open += eventProvider.OpenClosetDoor;
    }
    
    private static void AddSitDownEvents(Location room, EventProvider eventProvider)
    {
        room.SitDown += eventProvider.SitDownOnChairInCryoChamber;
    }
    
    private static void AddTakeEvents(Location room, EventProvider eventProvider)
    {
        room.Take += eventProvider.TryToTakeThingsFromCryoChamber;
    }

    private static void AddChangeLocationEvents(Location room, EventProvider eventProvider)
    {
        room.BeforeChangeLocation += eventProvider.CantLeaveWithoutSuiteAndUneatenBar;
    }
}