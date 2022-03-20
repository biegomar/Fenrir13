using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Jetty;

internal static class JettyPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var jetty = new Location()
        {
            Key = Keys.JETTY,
            Name = Locations.JETTY,
            Description = Descriptions.JETTY,
            Grammar = new Grammars(Genders.Male)
        };

        AddSurroundings(jetty);
        
        return jetty;
    }
    
    private static void AddSurroundings(Location jetty)
    {
        jetty.Surroundings.Add(Keys.SPACE_LADDER, () => Descriptions.SPACE_LADDER);
        jetty.Surroundings.Add(Keys.JETTY_HULL, () => Descriptions.JETTY_HULL);
        jetty.Surroundings.Add(Keys.JETTY_SPACE, () => Descriptions.JETTY_SPACE);
        jetty.Surroundings.Add(Keys.JETTY_ENGINE, () => Descriptions.JETTY_ENGINE);
    }
}