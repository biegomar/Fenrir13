using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.RoofTop;

internal static class RoofTopPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var room = new Location()
        {
            Key = Keys.ROOF_TOP,
            Name = Locations.ROOF_TOP,
            Description = Descriptions.ROOF_TOP,
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        AddSurroundings(room);
        
        room.Items.Add(GetDroid(eventProvider));

        return room;
    }

    private static Item GetDroid(EventProvider eventProvider)
    {
        var droid = new Item
        {
            Key = Keys.DROID,
            Name = Items.DROID,
            Description = Descriptions.DROID,
            FirstLookDescription = Descriptions.DROID_FIRSTLOOK,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddUseEvents(droid, eventProvider);

        return droid;
    }
    
    private static void AddSurroundings(Location room)
    {
        room.Surroundings.Add(Keys.SPACE_LADDER, () => Descriptions.SPACE_LADDER);
        room.Surroundings.Add(Keys.JETTY_SPACE, () => Descriptions.JETTY_SPACE);
        room.Surroundings.Add(Keys.PROXIMA_CENTAURI, () => Descriptions.PROXIMA_CENTAURI);
        room.Surroundings.Add(Keys.ROOF_TOP_CLAW, () => Descriptions.ROOF_TOP_CLAW);
        room.Surroundings.Add(Keys.ROOF_TOP_DOCK, () => Descriptions.ROOF_TOP_DOCK);
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.MountAntennaToDroid;
    }
}