using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;
using Microsoft.VisualBasic;

namespace Fenrir13.GamePlay.Prerequisites.CommandBridge;

internal class CommandBridgePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var commandBridge = new Location()
        {
            Key = Keys.COMMANDBRIDGE,
            Name = Locations.COMMANDBRIDGE,
            Description = Descriptions.COMMANDBRIDGE,
            FirstLookDescription = Descriptions.COMMANDBRIDGE_FIRSTLOOK
        };
        
        AddSurroundings(commandBridge);
        
        AddAfterLookEvents(commandBridge, eventProvider);
        
        commandBridge.Items.Add(GetPilotSeat(eventProvider));
        commandBridge.Items.Add(GetStickyNote(eventProvider));

        return commandBridge;
    }
    
    private static void AddSurroundings(Location location)
    {
        location.Surroundings.Add(Keys.CONTROL_PANEL, Descriptions.CONTROL_PANEL);
    }
    
    private static void AddAfterLookEvents(Location bridge, EventProvider eventProvider)
    {
        bridge.AfterLook += eventProvider.LookAtControlPanel;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.LookAtControlPanel), 1);
    }

    private static Item GetPilotSeat(EventProvider eventProvider)
    {
        var pilotSeat = new Item()
        {
            Key = Keys.PILOT_SEAT,
            Name = Items.PILOT_SEAT,
            Description = Descriptions.PILOT_SEAT,
            IsSeatAble = true
        };
        
        return pilotSeat;
    }
    
    private static Item GetStickyNote(EventProvider eventProvider)
    {
        var stickyNote = new Item()
        {
            Key = Keys.STICKY_NOTE,
            Name = Items.STICKY_NOTE,
            Description = string.Format(Descriptions.STICKY_NOTE, Descriptions.TERMINAL_PASSWORD_HINT),
            IsHidden = true,
            IsUnveilAble = false
        };
        
        return stickyNote;
    }
}