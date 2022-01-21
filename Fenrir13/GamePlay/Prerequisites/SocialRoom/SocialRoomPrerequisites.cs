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

        AddSurroundings(socialRoom);
        
        return socialRoom;
    }
    
    private static void AddSurroundings(Location socialRoom)
    {
        socialRoom.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
    }
}