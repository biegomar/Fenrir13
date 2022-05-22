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

    private static void AddSurroundings(Location cryoChamber)
    {
        cryoChamber.Surroundings.Add(Keys.CRYOPOD, () => Descriptions.CRYOPOD);
        cryoChamber.Surroundings.Add(Keys.CHAMBER_FLOOR, () => Descriptions.CHAMBER_FLOOR);
        cryoChamber.Surroundings.Add(Keys.WORKBENCH, () => Descriptions.WORKBENCH);
        cryoChamber.Surroundings.Add(Keys.LAPTOP, () => Descriptions.LAPTOP);
        cryoChamber.Surroundings.Add(Keys.CHAMBER_WALL, () => Descriptions.CHAMBER_WALL);
        cryoChamber.Surroundings.Add(Keys.PIERHOLE, () => Descriptions.PIERHOLE);
        cryoChamber.Surroundings.Add(Keys.OFFICECHAIR, () => Descriptions.OFFICECHAIR);
        cryoChamber.Surroundings.Add(Keys.CLOSET, () => Descriptions.CLOSET);
        cryoChamber.Surroundings.Add(Keys.DRAWER, () => Descriptions.DRAWER);
        cryoChamber.Surroundings.Add(Keys.CLOSET_DOOR, () => Descriptions.CLOSET_DOOR);
        cryoChamber.Surroundings.Add(Keys.WARDROBE, () => Descriptions.WARDROBE);
        cryoChamber.Surroundings.Add(Keys.TABLE, () => Descriptions.TABLE);
        cryoChamber.Surroundings.Add(Keys.CHAIR, () => Descriptions.CHAIR);
        cryoChamber.Surroundings.Add(Keys.PARTITION_WALL, () => Descriptions.PARTITION_WALL);
        cryoChamber.Surroundings.Add(Keys.WASHINGAREA, () => Descriptions.WASHINGAREA);
        cryoChamber.Surroundings.Add(Keys.TOILET, () => Descriptions.TOILET);
        cryoChamber.Surroundings.Add(Keys.MIRROR, () => Descriptions.MIRROR);
        cryoChamber.Surroundings.Add(Keys.OUTLINES, () => Descriptions.OUTLINES);
        cryoChamber.Surroundings.Add(Keys.PEEPHOLE, () => Descriptions.PEEPHOLE);
        cryoChamber.Surroundings.Add(Keys.LATCH, () => Descriptions.LATCH);
        cryoChamber.Surroundings.Add(Keys.CRYOCHAMBER_DOOR, () => Descriptions.CRYOCHAMBER_DOOR);
        cryoChamber.Surroundings.Add(Keys.DISPLAY, () => Descriptions.DISPLAY);
        cryoChamber.Surroundings.Add(Keys.TOILET_SEAT, () => Descriptions.TOILET_SEAT);
        cryoChamber.Surroundings.Add(Keys.WRITING_MATERIALS, () => Descriptions.WRITING_MATERIALS);
        cryoChamber.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        cryoChamber.Surroundings.Add(Keys.CRYOCHAMBER_BULKHEAD, () => Descriptions.CRYOCHAMBER_BULKHEAD);
        cryoChamber.Surroundings.Add(Keys.CRYOCHAMBER_LAMP, () => Descriptions.CRYOCHAMBER_LAMP);
        cryoChamber.Surroundings.Add(Keys.CRYOCHAMBER_CLOTHS, () => Descriptions.CRYOCHAMBER_CLOTHS);
        cryoChamber.Surroundings.Add(Keys.PROXIMA_CENTAURI, () => Descriptions.PROXIMA_CENTAURI);
        cryoChamber.Surroundings.Add(Keys.PAPER, () => Descriptions.PAPER);
        cryoChamber.Surroundings.Add(Keys.PENCILS, () => Descriptions.PENCILS);
        cryoChamber.Surroundings.Add(Keys.PENCIL_I, () => Descriptions.PENCIL_I);
        cryoChamber.Surroundings.Add(Keys.PENCIL_II, () => Descriptions.PENCIL_II);
        cryoChamber.Surroundings.Add(Keys.FELT, () => Descriptions.FELT);
        cryoChamber.Surroundings.Add(Keys.PANEL_TOP_WOLF, () => Descriptions.PANEL_TOP_WOLF);
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