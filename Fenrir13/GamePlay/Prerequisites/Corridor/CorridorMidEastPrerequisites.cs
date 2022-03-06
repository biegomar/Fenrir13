using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDEAST,
            Name = Locations.CORRIDOR_MIDEAST,
            Description = Descriptions.CORRIDOR_MIDEAST,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddSurroundings(corridor);

        return corridor;
    }
    
    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.CORRIDOR_PAINTING, () => Descriptions.CORRIDOR_PAINTING_MIDEAST);
        corridor.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        corridor.Surroundings.Add(Keys.CORRIDOR_WALLS, () => Descriptions.CORRIDOR_WALLS);
    }
}