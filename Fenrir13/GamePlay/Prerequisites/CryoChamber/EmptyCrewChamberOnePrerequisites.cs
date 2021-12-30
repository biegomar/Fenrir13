using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CryoChamber;

internal static class EmptyCrewChamberOnePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var crewChamber = new Location()
        {
            Key = Keys.EMPTYCREWCHAMBERONE,
            Name = Locations.EMPTYCREWCHAMBERONE,
            Description = Descriptions.EMPTYCREWCHAMBERONE,
            LockDescription = Descriptions.EMPTYCREWCHAMBERONE_LOCKDESCRIPTION,
            IsLocked = true
        };

        return crewChamber;
    }
}