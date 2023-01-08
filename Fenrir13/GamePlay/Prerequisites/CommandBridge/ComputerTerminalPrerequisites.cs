using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
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
            FirstLookDescription = Descriptions.COMPUTER_TERMINAL_FIRSTLOOK,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        
        AddWriteEvents(terminal, eventProvider);
        
        return terminal;
    }

    private static void AddWriteEvents(Location terminal, EventProvider eventProvider)
    {
        terminal.Write += eventProvider.WriteTextToComputerTerminal;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.WriteTextToComputerTerminal), 5);
    }
}