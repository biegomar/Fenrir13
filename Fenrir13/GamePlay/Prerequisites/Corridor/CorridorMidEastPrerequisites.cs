using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

public class CorridorMidEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDEAST,
            Name = Locations.CORRIDOR_MIDEAST,
            Description = Descriptions.CORRIDOR_MIDEAST
        };

        return corridor;
    }
}