using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_WEST,
            Name = Locations.CORRIDOR_WEST,
            Description = Descriptions.CORRIDOR_WEST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_WEST_LOCKDESCRIPTION,
            FirstLookDescription = Descriptions.CORRIDOR_WEST_FIRSTLOOK,
            Grammar = new Grammars(Genders.Male)
        };

        AddSurroundings(corridor);
        
        return corridor;
    }
    
    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.CORRIDOR_PAINTING, () => Descriptions.CORRIDOR_PAINTING_WEST);
        corridor.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        corridor.Surroundings.Add(Keys.CORRIDOR_WALLS, () => Descriptions.CORRIDOR_WALLS);
        corridor.Surroundings.Add(Keys.CORRIDOR_WEST_HOLE, () => Descriptions.CORRIDOR_WEST_HOLE);
        corridor.Surroundings.Add(Keys.CORRIDOR_WEST_WELD_SEAM, () => Descriptions.CORRIDOR_WEST_WELD_SEAM);
        corridor.Surroundings.Add(Keys.CORRIDOR_WEST_BULLEYE, () => Descriptions.CORRIDOR_WEST_BULLEYE);
    }
}