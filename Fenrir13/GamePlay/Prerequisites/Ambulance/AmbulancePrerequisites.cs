using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Ambulance;

internal static class AmbulancePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var ambulance = new Location()
        {
            Key = Keys.AMBULANCE,
            Name = Locations.AMBULANCE,
            Description = Descriptions.AMBULANCE
        };
        
        AddSurroundings(ambulance);
        
        return ambulance;
    }
    
    private static void AddSurroundings(Location location)
    {
        location.Surroundings.Add(Keys.AMBULANCE_OP_TABLE, Descriptions.AMBULANCE_OP_TABLE);
        location.Surroundings.Add(Keys.AMBULANCE_OP_ROBOTER, Descriptions.AMBULANCE_OP_ROBOTER);
        location.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
        location.Surroundings.Add(Keys.CHAMBER_WALL, Descriptions.CHAMBER_WALL);
        location.Surroundings.Add(Keys.AMBULANCE_BED, Descriptions.AMBULANCE_BED);
        location.Surroundings.Add(Keys.AMBULANCE_OP_ITEMS, Descriptions.AMBULANCE_OP_ITEMS);
    }
}