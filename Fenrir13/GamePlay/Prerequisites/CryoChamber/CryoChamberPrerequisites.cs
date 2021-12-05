using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CryoChamber;

public class CryoChamberPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        return new()
        {
            Key = Keys.CRYOCHAMBER,
            Name = Locations.CRYOCHAMBER,
            Description = Descriptions.CRYOCHAMBER
        };
    }
}