using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Heretic.InteractiveFiction.Resources;

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
        
        AddSurroundings(cryoChamber, eventProvider);

        AddChangeLocationEvents(cryoChamber, eventProvider);
        
        return cryoChamber;
    }

    private static Item GetChocolateBar(EventProvider eventProvider)
    {
        var bar = new Item
        {
            Key = Keys.CHOCOLATEBAR,
            Description = Descriptions.CHOCOLATEBAR,
            Name = Items.CHOCOLATEBAR,
            IsHidden = true,
            IsUnveilable = false,
            IsEatable = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };

        AddEatEvents(bar, eventProvider);

        return bar;
    }

    private static Item GetSpaceSuite(EventProvider eventProvider)
    {
        var spaceSuite = new Item
        {
            Key = Keys.SPACE_SUIT,
            Description = Descriptions.SPACE_SUIT,
            Name = Items.SPACE_SUIT,
            IsDropable = false,
            ContainmentDescription = Descriptions.SPACE_SUIT_CONTAINMENT,
            UnDropAbleDescription = Descriptions.SPACE_SUIT_UNDROPABLE,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };

        AddAfterTakeEvents(spaceSuite, eventProvider);

        return spaceSuite;
    }

    private static void AddSurroundings(Location location, EventProvider eventProvider)
    {
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(wall);
        
        var floor = new Item()
        {
            Key = Keys.CHAMBER_FLOOR,
            Name = Items.CHAMBER_FLOOR,
            Description = Descriptions.CHAMBER_FLOOR,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(floor);
        
        var table = new Item()
        {
            Key = Keys.TABLE,
            Name = Items.TABLE,
            Description = Descriptions.TABLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(table);
        
        var chair = new Item()
        {
            Key = Keys.CHAIR,
            Name = Items.CHAIR,
            Description = Descriptions.CHAIR,
            IsSurrounding = true,
            IsPickable = false,
            IsSeatable = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(chair);
        AddSitDownEvents(chair, eventProvider);
        
        var pod = new Item()
        {
            Key = Keys.CRYOPOD,
            Name = Items.CRYOPOD,
            Description = Descriptions.CRYOPOD,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(pod);
        
        var bed = new Item()
        {
            Key = Keys.CRYOPOD_BED,
            Name = Items.CRYOPOD_BED,
            Description = Descriptions.CRYOPOD,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(bed);
        
        var workbench = new Item()
        {
            Key = Keys.WORKBENCH,
            Name = Items.WORKBENCH,
            Description = Descriptions.WORKBENCH,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(workbench);
        
        var laptop = new Item()
        {
            Key = Keys.LAPTOP,
            Name = Items.LAPTOP,
            Description = Descriptions.LAPTOP,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        AddBeforeTakeEvents(laptop, eventProvider);
        location.Items.Add(laptop);
        
        var pierHole = new Item()
        {
            Key = Keys.PIERHOLE,
            Name = Items.PIERHOLE,
            Description = Descriptions.PIERHOLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(pierHole);
        
        var officeChair = new Item()
        {
            Key = Keys.OFFICECHAIR,
            Name = Items.OFFICECHAIR,
            Description = Descriptions.OFFICECHAIR,
            IsSurrounding = true,
            IsPickable = false,
            IsSeatable = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(officeChair);
        AddSitDownEvents(officeChair, eventProvider);
        
        var closet = new Item()
        {
            Key = Keys.CLOSET,
            Name = Items.CLOSET,
            Description = Descriptions.CLOSET,
            IsSurrounding = true,
            IsPickable = false,
            IsCloseable = true,
            IsClosed = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(closet);
        AddOpenCloseClosetEvents(closet, eventProvider);

        var closetDoor = new Item()
        {
            Key = Keys.CLOSET_DOOR,
            Name = Items.CLOSET_DOOR,
            Description = Descriptions.CLOSET_DOOR,
            IsSurrounding = true,
            IsPickable = false,
            IsCloseable = true,
            IsClosed = true,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(closetDoor);
        AddOpenCloseClosetEvents(closetDoor, eventProvider);
        AddAfterLookEventsForClosetDoor(closetDoor, eventProvider);
        
        var drawer = new Item()
        {
            Key = Keys.DRAWER,
            Name = Items.DRAWER,
            Description = Descriptions.DRAWER,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(drawer);
        
        var wardrobe = new Item()
        {
            Key = Keys.WARDROBE,
            Name = Items.WARDROBE,
            Description = Descriptions.WARDROBE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(wardrobe);
        
        var partitionWall = new Item()
        {
            Key = Keys.PARTITION_WALL,
            Name = Items.PARTITION_WALL,
            Description = Descriptions.PARTITION_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(partitionWall);
        
        var washingArea = new Item()
        {
            Key = Keys.WASHINGAREA,
            Name = Items.WASHINGAREA,
            Description = Descriptions.WASHINGAREA,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(washingArea);
        
        var washingAreaBasin = new Item()
        {
            Key = Keys.WASHINGAREA_BASIN,
            Name = Items.WASHINGAREA_BASIN,
            Description = Descriptions.WASHINGAREA,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(washingAreaBasin);
        
        var toilet = new Item()
        {
            Key = Keys.TOILET,
            Name = Items.TOILET,
            Description = Descriptions.TOILET,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(toilet);
        
        var toiletSeat = new Item()
        {
            Key = Keys.TOILET_SEAT,
            Name = Items.TOILET_SEAT,
            Description = Descriptions.TOILET_SEAT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(toiletSeat);
        
        var mirror = new Item()
        {
            Key = Keys.MIRROR,
            Name = Items.MIRROR,
            Description = Descriptions.MIRROR,
            FirstLookDescription = Descriptions.MIRROR_FIRSTLOOK,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(mirror);
        
        var outlines = new Item()
        {
            Key = Keys.OUTLINES,
            Name = Items.OUTLINES,
            Description = Descriptions.OUTLINES,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(outlines);
        
        var peephole = new Item()
        {
            Key = Keys.PEEPHOLE,
            Name = Items.PEEPHOLE,
            Description = Descriptions.PEEPHOLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(peephole);
        
        var latch = new Item()
        {
            Key = Keys.LATCH,
            Name = Items.LATCH,
            Description = Descriptions.LATCH,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(latch);
        
        var door = new Item()
        {
            Key = Keys.CRYOCHAMBER_DOOR,
            Name = Items.CRYOCHAMBER_DOOR,
            Description = Descriptions.CRYOCHAMBER_DOOR,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(door);
        AddAfterOpenEvents(door, eventProvider);
        
        var display = new Item()
        {
            Key = Keys.DISPLAY,
            Name = Items.DISPLAY,
            Description = Descriptions.DISPLAY,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(display);
        AddAfterLookEventsForDisplay(display, eventProvider);
        
        var materials = new Item()
        {
            Key = Keys.WRITING_MATERIALS,
            Name = Items.WRITING_MATERIALS,
            Description = Descriptions.WRITING_MATERIALS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(materials);
        
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(ceiling);
        
        var bulkhead = new Item()
        {
            Key = Keys.CRYOCHAMBER_BULKHEAD,
            Name = Items.CRYOCHAMBER_BULKHEAD,
            Description = Descriptions.CRYOCHAMBER_BULKHEAD,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(bulkhead);
        
        var lamp = new Item()
        {
            Key = Keys.CRYOCHAMBER_LAMP,
            Name = Items.CRYOCHAMBER_LAMP,
            Description = Descriptions.CRYOCHAMBER_LAMP,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(lamp);
        
        var cloths = new Item()
        {
            Key = Keys.CRYOCHAMBER_CLOTHS,
            Name = Items.CRYOCHAMBER_CLOTHS,
            Description = Descriptions.CRYOCHAMBER_CLOTHS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(cloths);
        
        var proxima = new Item()
        {
            Key = Keys.PROXIMA_CENTAURI,
            Name = Items.PROXIMA_CENTAURI,
            Description = Descriptions.PROXIMA_CENTAURI,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(proxima);
        
        var paper = new Item()
        {
            Key = Keys.PAPER,
            Name = Items.PAPER,
            Description = Descriptions.PAPER,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(paper);
        
        var pencils = new Item()
        {
            Key = Keys.PENCILS,
            Name = Items.PENCILS,
            Description = Descriptions.PENCILS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male, isSingular: false)
        };
        location.Items.Add(pencils);
        
        var pencilOne = new Item()
        {
            Key = Keys.PENCIL_I,
            Name = Items.PENCIL_I,
            Description = Descriptions.PENCIL_I,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(pencilOne);
        
        var pencilTwo = new Item()
        {
            Key = Keys.PENCIL_II,
            Name = Items.PENCIL_II,
            Description = Descriptions.PENCIL_II,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(pencilTwo);
        
        var felt = new Item()
        {
            Key = Keys.FELT,
            Name = Items.FELT,
            Description = Descriptions.FELT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(felt);
        
        var wolf = new Item()
        {
            Key = Keys.PANEL_TOP_WOLF,
            Name = Items.PANEL_TOP_WOLF,
            Description = Descriptions.PANEL_TOP_WOLF,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(wolf);
    }

    private static void AddAfterLookEventsForClosetDoor(Item closetDoor, EventProvider eventProvider)
    {
        closetDoor.AfterLook += eventProvider.LookAtClosetDoor;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtClosetDoor), 1);
    }
    
    private static void AddAfterLookEventsForDisplay(Item display, EventProvider eventProvider)
    {
        display.AfterLook += eventProvider.LookAtDisplay;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtDisplay), 1);
    }

    private static void AddEatEvents(Item chocolateBar, EventProvider eventProvider)
    {
        chocolateBar.Eat += eventProvider.EatChocolateBar;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.EatChocolateBar), 5);
    }

    private static void AddAfterTakeEvents(Item item, EventProvider eventProvider)
    {
        item.AfterTake += eventProvider.TakeSpaceSuite;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.TakeSpaceSuite), 1);
    }
    
    private static void AddBeforeTakeEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeTake += eventProvider.TakeLaptop;
    }

    private static void AddOpenCloseClosetEvents(Item item, EventProvider eventProvider)
    {
        item.Open += eventProvider.OpenCloset;
        item.Close += eventProvider.CloseCloset;
    }
    
    private static void AddAfterOpenEvents(Item cryoChamberDoor, EventProvider eventProvider)
    {
        cryoChamberDoor.AfterOpen += eventProvider.TryToOpenCryoChamberDoor;
    }
    
    private static void AddSitDownEvents(Item item, EventProvider eventProvider)
    {
        item.SitDown += eventProvider.SitDownOnChairInCryoChamber;
    }
    
    private static void AddChangeLocationEvents(Location room, EventProvider eventProvider)
    {
        room.BeforeLeaveLocation += eventProvider.CantLeaveWithoutSuiteAndUneatenBar;
    }
}