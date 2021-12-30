using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.CommandBridge;

internal static class ComputerTerminalPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var terminal = new Location()
        {
            Key = Keys.COMPUTER_TERMINAL,
            Name = Locations.COMPUTER_TERMINAL,
            Description = Descriptions.COMPUTER_TERMINAL,
            FirstLookDescription = Descriptions.COMPUTER_TERMINAL_FIRSTLOOK
        };
        
        return terminal;
    }
}