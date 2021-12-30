using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_WEST,
            Name = Locations.CORRIDOR_WEST,
            Description = Descriptions.CORRIDOR_WEST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_WEST_LOCKDESCRIPTION
            
        };

        return corridor;
    }
}