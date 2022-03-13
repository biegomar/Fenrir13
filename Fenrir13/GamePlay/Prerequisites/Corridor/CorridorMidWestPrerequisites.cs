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
            Description = Descriptions.CORRIDOR_MIDWEST,
            Grammar = new Grammars(Genders.Male)
        };

        AddSurroundings(corridor);
        
        return corridor;
    }

    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.BULKHEAD, () => Descriptions.BULKHEAD);
        corridor.Surroundings.Add(Keys.BULKHEAD_WINDOW, () => Descriptions.BULKHEAD_WINDOW);
        corridor.Surroundings.Add(Keys.CORRIDOR_PAINTING, () => Descriptions.CORRIDOR_PAINTING_MIDWEST);
        corridor.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        corridor.Surroundings.Add(Keys.CORRIDOR_WALLS, () => Descriptions.CORRIDOR_WALLS);
        corridor.Surroundings.Add(Keys.HOLE, () => Descriptions.HOLE);
        corridor.Surroundings.Add(Keys.SIDEWALL, () => Descriptions.SIDEWALL);
        corridor.Surroundings.Add(Keys.CREWCHAMBERS, () => Descriptions.CREWCHAMBERS);
        corridor.Surroundings.Add(Keys.CREW_DOORS, () => Descriptions.CREW_DOORS);
    }
}