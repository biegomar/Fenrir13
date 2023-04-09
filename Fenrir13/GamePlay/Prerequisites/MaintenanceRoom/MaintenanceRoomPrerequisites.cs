using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.MaintenanceRoom;

internal static class MaintenanceRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var room = new Location()
        {
            Key = Keys.MAINTENANCE_ROOM,
            Name = Locations.MAINTENANCE_ROOM,
            Description = Descriptions.MAINTENANCE_ROOM
        };

        room.Items.Add(GetWorkbench(eventProvider));
        
        AddSurroundings(room);
        
        return room;
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
        
        var eastWall = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_EAST_WALL,
            Name = Items.MAINTENANCE_ROOM_EAST_WALL,
            Description = Descriptions.MAINTENANCE_ROOM_EAST_WALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(eastWall);
        
        var shelf = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_SHELF,
            Name = Items.MAINTENANCE_ROOM_SHELF,
            Description = Descriptions.MAINTENANCE_ROOM_SHELF,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(shelf);
        
        var utils = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_UTILS,
            Name = Items.MAINTENANCE_ROOM_UTILS,
            Description = Descriptions.MAINTENANCE_ROOM_UTILS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(utils);
        
        var plate = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_PLATE,
            Name = Items.MAINTENANCE_ROOM_PLATE,
            Description = Descriptions.MAINTENANCE_ROOM_PLATE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(plate);
        
        var signs = new Item()
        {
            Key = Keys.SIGNS_OF_USE,
            Name = Items.SIGNS_OF_USE,
            Description = Descriptions.SIGNS_OF_USE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(signs);
        
        var handle = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_DRAWER_HANDLE,
            Name = Items.MAINTENANCE_ROOM_DRAWER_HANDLE,
            Description = Descriptions.MAINTENANCE_ROOM_DRAWER_HANDLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(handle);
    }
    
    private static Item GetWorkbench(EventProvider eventProvider)
    {
        var result = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_WORKBENCH,
            Name = Items.MAINTENANCE_ROOM_WORKBENCH,
            Description = Descriptions.MAINTENANCE_ROOM_WORKBENCH,
            IsPickable = false
        };

        result.Items.Add(GetDrawer(eventProvider));

        return result;
    }
    
    private static Item GetDrawer(EventProvider eventProvider)
    {
        var result = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_DRAWER,
            Name = Items.MAINTENANCE_ROOM_DRAWER,
            Description = Descriptions.MAINTENANCE_ROOM_DRAWER,
            ContainmentDescription = Descriptions.MAINTENANCE_ROOM_DRAWER_CONTAINMENT,
            CloseDescription = Descriptions.MAINTENANCE_ROOM_DRAWER_CLOSE,
            OpenDescription = Descriptions.MAINTENANCE_ROOM_DRAWER_OPEN,
            IsPickable = false,
            IsCloseable = true,
            IsClosed = true
        };

        result.Items.Add(GetTool(eventProvider));

        return result;
    }
    
    private static Item GetTool(EventProvider eventProvider)
    {
        var result = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_TOOL,
            Name = Items.MAINTENANCE_ROOM_TOOL,
            Description = Descriptions.MAINTENANCE_ROOM_TOOL,
            ContainmentDescription = Descriptions.MAINTENANCE_ROOM_TOOL_CONTAINMENT,
            IsHidden = true,
            Weight = ItemWeights.MAINTENANCE_ROOM_TOOL,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        
        AddTakeEvents(result, eventProvider);
        AddUseEvents(result, eventProvider);

        return result;
    }
    
    private static void AddTakeEvents(Item item, EventProvider eventProvider)
    {
        item.AfterTake += eventProvider.TakeTool;
        eventProvider.RegisterScore(nameof(eventProvider.TakeTool), 1);
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseToolWithAntennaInSocialRoom;
        eventProvider.RegisterScore(nameof(eventProvider.UseToolWithAntennaInSocialRoom), 5);
    }
}