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
            Description = Descriptions.JETTY
        };

        return jetty;
    }
}