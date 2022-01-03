using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDWEST,
            Name = Locations.CORRIDOR_MIDWEST,
            Description = Descriptions.CORRIDOR_MIDWEST
        };

        AddSurroundings(corridor);
        
        return corridor;
    }

    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.BULKHEAD, Descriptions.BULKHEAD);
        corridor.Surroundings.Add(Keys.BULKHEAD_WINDOW, Descriptions.BULKHEAD_WINDOW);
        corridor.Surroundings.Add(Keys.CORRIDOR_PAINTING, Descriptions.CORRIDOR_PAINTING_MIDWEST);
    }
}