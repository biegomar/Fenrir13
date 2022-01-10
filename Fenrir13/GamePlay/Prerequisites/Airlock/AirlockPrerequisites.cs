using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Airlock;

internal class AirlockPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var airlock = new Location()
        {
            Key = Keys.AIRLOCK,
            Name = Locations.AIRLOCK,
            Description = Descriptions.AIRLOCK
        };

        AddSurroundings(airlock);
        
        return airlock;
    }
    
    private static void AddSurroundings(Location airlock)
    {
        
    }
}