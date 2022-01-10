using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CryoChamber;

internal static class CryoChamberPrerequisites
{
    private const string FENRIR13 = "\u16b1\u16a8\u16a2\u16d7\u16a0\u16b1\u16a8\u16b2\u16bb\u16cf\u16d6\u16b1 \u16a0\u16d6\u16be\u16b1\u16c1\u16b1";

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
            IsUnveilAble = false
        };

        AddAfterEatEvents(bar, eventProvider);
        
        return bar;
    }
    
    private static Item GetSpaceSuite(EventProvider eventProvider)
    {
        var spaceSuite = new Item
        {
            Key = Keys.SPACE_SUITE,
            Description = Descriptions.SPACE_SUITE,
            Name = Items.SPACE_SUITE,
            ContainmentDescription = Descriptions.SPACE_SUITE_CONTAINMENT
        };
        
        AddAfterTakeEvents(spaceSuite, eventProvider);
        
        return spaceSuite;
    }

    private static void AddSurroundings(Location cryoChamber)
    {
        cryoChamber.Surroundings.Add(Keys.CRYOPOD, Descriptions.CRYOPOD);
        cryoChamber.Surroundings.Add(Keys.CHAMBER_FLOOR, Descriptions.CHAMBER_FLOOR);
        cryoChamber.Surroundings.Add(Keys.CARPET, Descriptions.CARPET);
        cryoChamber.Surroundings.Add(Keys.WORKBENCH, Descriptions.WORKBENCH);
        cryoChamber.Surroundings.Add(Keys.LAPTOP, Descriptions.LAPTOP);
        cryoChamber.Surroundings.Add(Keys.CHAMBER_WALL, Descriptions.CHAMBER_WALL);
        cryoChamber.Surroundings.Add(Keys.PIERHOLE, string.Format(Descriptions.PIERHOLE, FENRIR13));
        cryoChamber.Surroundings.Add(Keys.OFFICECHAIR, Descriptions.OFFICECHAIR);
        cryoChamber.Surroundings.Add(Keys.RUBBER_ROLLS, Descriptions.RUBBER_ROLLS);
        cryoChamber.Surroundings.Add(Keys.CLOSET, Descriptions.CLOSET);
        cryoChamber.Surroundings.Add(Keys.DRAWER, Descriptions.DRAWER);
        cryoChamber.Surroundings.Add(Keys.CLOSET_DOOR, Descriptions.CLOSET_DOOR);
        cryoChamber.Surroundings.Add(Keys.WARDROBE, Descriptions.WARDROBE);
        cryoChamber.Surroundings.Add(Keys.TABLE, Descriptions.TABLE);
        cryoChamber.Surroundings.Add(Keys.CHAIR, Descriptions.CHAIR);
        cryoChamber.Surroundings.Add(Keys.PARTITION_WALL, Descriptions.PARTITION_WALL);
        cryoChamber.Surroundings.Add(Keys.WASHINGAREA, Descriptions.WASHINGAREA);
        cryoChamber.Surroundings.Add(Keys.TOILET, Descriptions.TOILET);
        cryoChamber.Surroundings.Add(Keys.MIRROR, Descriptions.MIRROR);
        cryoChamber.Surroundings.Add(Keys.OUTLINES, Descriptions.OUTLINES);
        cryoChamber.Surroundings.Add(Keys.PEEPHOLE, Descriptions.PEEPHOLE);
        cryoChamber.Surroundings.Add(Keys.LATCH, Descriptions.LATCH);
        cryoChamber.Surroundings.Add(Keys.CRYOCHAMBER_DOOR, Descriptions.CRYOCHAMBER_DOOR);
        cryoChamber.Surroundings.Add(Keys.DISPLAY, Descriptions.DISPLAY);
        cryoChamber.Surroundings.Add(Keys.TOILET_SEAT, Descriptions.TOILET_SEAT);
    }
    
    private static void AddAfterLookEvents(Location cryoChamber, EventProvider eventProvider)
    {
        cryoChamber.AfterLook += eventProvider.LookAtPierhole;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtPierhole), 1);

        cryoChamber.AfterLook += eventProvider.LookAtClosedDoor;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtClosedDoor), 1);

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
    }
    
    private static void AddChangeLocationEvents(Location room, EventProvider eventProvider)
    {
        room.BeforeChangeLocation += eventProvider.CantLeaveWithoutSuiteAndUneatenBar;
    }
}