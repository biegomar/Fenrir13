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
        
        AddChangeRoomEvents(airlock, eventProvider);
        
        return airlock;
    }
    
    private static void AddSurroundings(Location airlock)
    {
        airlock.Surroundings.Add(Keys.AIRLOCK_KEYPAD, Descriptions.AIRLOCK_KEYPAD);
        airlock.Surroundings.Add(Keys.AIRLOCK_KEYPAD_GREEN_BUTTON, Descriptions.AIRLOCK_KEYPAD_GREEN_BUTTON);
        airlock.Surroundings.Add(Keys.AIRLOCK_KEYPAD_RED_BUTTON, Descriptions.AIRLOCK_KEYPAD_RED_BUTTON);
    }
    
    private static void AddChangeRoomEvents(Location airlock, EventProvider eventProvider)
    {
        airlock.AfterChangeLocation += eventProvider.EnterAirlock;
        airlock.BeforeChangeLocation += eventProvider.LeaveAirlock;
    }
}