using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

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
        
        commandBridge.Items.Add(GetPilotSeat(eventProvider));

        return commandBridge;
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
}