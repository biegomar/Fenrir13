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
        
        AddSurroundings(socialRoom);

        return socialRoom;
    }

    private static void AddSurroundings(Location socialRoom)
    {
        socialRoom.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
        socialRoom.Surroundings.Add(Keys.CHAMBER_WALL, Descriptions.CHAMBER_WALL);
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
}