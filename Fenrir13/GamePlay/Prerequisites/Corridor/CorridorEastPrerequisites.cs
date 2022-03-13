using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_EAST,
            Name = Locations.CORRIDOR_EAST,
            Description = Descriptions.CORRIDOR_EAST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_EAST_LOCKDESCRIPTION,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddSurroundings(corridor);

        return corridor;
    }

    private static void AddSurroundings(Location corridor)
    {
        corridor.Surroundings.Add(Keys.EMPTYCREWCHAMBERONE, () => Descriptions.EMPTYCREWCHAMBERONE);
        corridor.Surroundings.Add(Keys.EMPTYCREWCHAMBERTWO, () => Descriptions.EMPTYCREWCHAMBERTWO);
        corridor.Surroundings.Add(Keys.CORRIDOR_PAINTING, () => Descriptions.CORRIDOR_PAINTING_EAST);
        corridor.Surroundings.Add(Keys.OUTLINES, () => Descriptions.CORRIDOR_PAINTING_EAST);
        corridor.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        corridor.Surroundings.Add(Keys.CORRIDOR_WALLS, () => Descriptions.CORRIDOR_WALLS);
        corridor.Surroundings.Add(Keys.RED_DOT, () => Descriptions.RED_DOT);
        corridor.Surroundings.Add(Keys.LOCATION, () => Descriptions.LOCATION);
        corridor.Surroundings.Add(Keys.GREEN_ARROWS, () => Descriptions.GREEN_ARROWS);
        corridor.Surroundings.Add(Keys.EMERGENCY_LIGHT, () => Descriptions.EMERGENCY_LIGHT);
    }
}