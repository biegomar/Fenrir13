using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_EAST,
            Name = Locations.CORRIDOR_EAST,
            Description = Descriptions.CORRIDOR_EAST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_EAST_LOCKDESCRIPTION
        };

        return corridor;
    }
}