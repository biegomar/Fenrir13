using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

public class CorridorMidWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDWEST,
            Name = Locations.CORRIDOR_MIDWEST,
            Description = Descriptions.CORRIDOR_MIDWEST
        };

        return corridor;
    }
}