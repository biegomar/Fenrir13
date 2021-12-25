using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

public static class CorridorMidPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MID,
            Name = Locations.CORRIDOR_MID,
            Description = Descriptions.CORRIDOR_MID
        };

        return corridor;
    }
}