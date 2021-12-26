using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CommandBridge;

public class CommandBridgePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.COMMANDBRIDGE,
            Name = Locations.COMMANDBRIDGE,
            Description = Descriptions.COMMANDBRIDGE,
            FirstLookDescription = Descriptions.COMMANDBRIDGE_FIRSTLOOK
        };

        return corridor;
    }
}