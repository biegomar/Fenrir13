using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CryoChamber;

internal static class EmptyCrewChamberTwoPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var crewChamber = new Location()
        {
            Key = Keys.EMPTYCREWCHAMBERTWO,
            Name = Locations.EMPTYCREWCHAMBERTWO,
            Description = Descriptions.EMPTYCREWCHAMBERTWO,
            LockDescription = Descriptions.EMPTYCREWCHAMBER_LOCKDESCRIPTION,
            IsLocked = true
        };

        return crewChamber;
    }
}