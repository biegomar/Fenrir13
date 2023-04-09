using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;
using Microsoft.VisualBasic;

namespace Fenrir13.GamePlay.Prerequisites.CommandBridge;

internal static class CommandBridgePrerequisites
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
        var ceiling = new Item()
        {
            Key = Keys.CEILING,
            Name = Items.CEILING,
            Description = Descriptions.CEILING,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CHAMBER_WALL,
            Name = Items.CHAMBER_WALL,
            Description = Descriptions.CHAMBER_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(wall);
        
        var controlPanel = new Item()
        {
            Key = Keys.CONTROL_PANEL,
            Name = Items.CONTROL_PANEL,
            Description = Descriptions.CONTROL_PANEL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(controlPanel);
        
        var controlPanelContent = new Item()
        {
            Key = Keys.CONTROL_PANEL_CONTENT,
            Name = Items.CONTROL_PANEL_CONTENT,
            Description = Descriptions.CONTROL_PANEL_CONTENT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(controlPanelContent);
        
        var ovoid = new Item()
        {
            Key = Keys.OVOID,
            Name = Items.OVOID,
            Description = Descriptions.OVOID,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(ovoid);
        
        var frontSide = new Item()
        {
            Key = Keys.FRONTSIDE,
            Name = Items.FRONTSIDE,
            Description = Descriptions.FRONTSIDE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(frontSide);
        
        var frontWindow = new Item()
        {
            Key = Keys.FRONTWINDOW,
            Name = Items.FRONTWINDOW,
            Description = Descriptions.FRONTWINDOW,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(frontWindow);
        
        var necessities = new Item()
        {
            Key = Keys.NECESSITIES,
            Name = Items.NECESSITIES,
            Description = Descriptions.NECESSITIES,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(necessities);
        
        var personalSettings = new Item()
        {
            Key = Keys.PERSONAL_SETTINGS,
            Name = Items.PERSONAL_SETTINGS,
            Description = Descriptions.PERSONAL_SETTINGS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(personalSettings);
    }
    
    private static void AddAfterLookEvents(Location bridge, EventProvider eventProvider)
    {
        bridge.AfterLook += eventProvider.LookAtControlPanel;
        eventProvider.RegisterScore(nameof(eventProvider.LookAtControlPanel), 1);
    }

    private static Item GetPilotSeat(EventProvider eventProvider)
    {
        var pilotSeat = new Item()
        {
            Key = Keys.PILOT_SEAT,
            Name = Items.PILOT_SEAT,
            Description = Descriptions.PILOT_SEAT,
            IsPickable = false,
            IsSeatable = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
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
            IsUnveilable = false
        };
        
        return stickyNote;
    }
}