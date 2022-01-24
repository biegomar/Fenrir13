using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Kitchen;

internal static class KitchenPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var kitchen = new Location()
        {
            Key = Keys.KITCHEN,
            Name = Locations.KITCHEN,
            Description = Descriptions.KITCHEN
        };

        AddSurroundings(kitchen);
        
        return kitchen;
    }
    
    private static void AddSurroundings(Location kitchen)
    {
        kitchen.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
    }
}