using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MID,
            Name = Locations.CORRIDOR_MID,
            Description = Descriptions.CORRIDOR_MID
        };
        
        AddSurroundings(corridor);
        corridor.Items.Add(GetPortrait(eventProvider));

        return corridor;
    }
    
    private static Item GetPortrait(EventProvider eventProvider)
    {
        var portrait = new Item()
        {
            Key = Keys.PORTRAIT,
            Name = Items.PORTRAIT,
            Description = Descriptions.PORTRAIT
        };

        return portrait;
    }
    
    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
    }
}