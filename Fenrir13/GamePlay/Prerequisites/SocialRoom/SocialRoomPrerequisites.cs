using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.SocialRoom;

public class SocialRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var socialRoom = new Location()
        {
            Key = Keys.SOCIALROOM,
            Name = Locations.SOCIALROOM,
            Description = Descriptions.SOCIALROOM
        };

        socialRoom.Items.Add(GetCouch(eventProvider));
        socialRoom.Items.Add(GetAntennaConstruction(eventProvider));
        
        AddSurroundings(socialRoom);

        return socialRoom;
    }

    private static void AddSurroundings(Location socialRoom)
    {
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_CEILING, Descriptions.SOCIALROOM_CEILING);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_MONITOR, Descriptions.SOCIALROOM_MONITOR);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_SEAT, Descriptions.SOCIALROOM_SEAT);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_EAST_WALL, Descriptions.SOCIALROOM_EAST_WEST_WALL);
        socialRoom.Surroundings.Add(Keys.SOCIALROOM_WEST_WALL, Descriptions.SOCIALROOM_EAST_WEST_WALL);
    }

    private static Item GetCouch(EventProvider eventProvider)
    {
        var couch = new Item()
        {
            Key = Keys.SOCIALROOM_COUCH,
            Name = Items.SOCIALROOM_COUCH,
            Description = Descriptions.SOCIALROOM_COUCH,
            IsPickAble = false,
        };

        return couch;
    }
    
    private static Item GetAntennaConstruction(EventProvider eventProvider)
    {
        var antennaConstruction = new Item()
        {
            Key = Keys.SOCIALROOM_ANTENNA_CONSTRUCTION,
            Name = Items.SOCIALROOM_ANTENNA_CONSTRUCTION,
            Description = Descriptions.SOCIALROOM_ANTENNA_CONSTRUCTION,
            ContainmentDescription = Descriptions.SOCIALROOM_ANTENNA_CONSTRUCTION_CONTAINMENT,
            IsPickAble = false,
        };

        return antennaConstruction;
    }
}