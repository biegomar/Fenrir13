using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.MaintenanceRoom;

internal static class MaintenanceRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var room = new Location()
        {
            Key = Keys.MAINTENANCE_ROOM,
            Name = Locations.MAINTENANCE_ROOM,
            Description = Descriptions.MAINTENANCE_ROOM
        };

        return room;
    }
}