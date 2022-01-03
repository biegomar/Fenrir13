using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Gym;

public class GymPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var gym = new Location()
        {
            Key = Keys.GYM,
            Name = Locations.GYM,
            Description = Descriptions.GYM
        };

        AddSurroundings(gym);
        
        return gym;
    }
    
    private static void AddSurroundings(Location gym)
    {
        
    }
}