using Fenrir13.Events;
using Fenrir13.Resources;
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
    
    private static void AddSurroundings(Location room)
    {
        room.Surroundings.Add(Keys.CEILING, () => Descriptions.CEILING);
        room.Surroundings.Add(Keys.CHAMBER_WALL, () => Descriptions.CHAMBER_WALL);
        room.Surroundings.Add(Keys.MAINTENANCE_ROOM_EAST_WALL, () => Descriptions.MAINTENANCE_ROOM_EAST_WALL);
        room.Surroundings.Add(Keys.MAINTENANCE_ROOM_SHELF, () => Descriptions.MAINTENANCE_ROOM_SHELF);
        room.Surroundings.Add(Keys.MAINTENANCE_ROOM_PLATE, () => Descriptions.MAINTENANCE_ROOM_PLATE);
        room.Surroundings.Add(Keys.SIGNS_OF_USE, () => Descriptions.SIGNS_OF_USE);
    }
    
    private static Item GetWorkbench(EventProvider eventProvider)
    {
        var result = new Item()
        {
            Key = Keys.MAINTENANCE_ROOM_WORKBENCH,
            Name = Items.MAINTENANCE_ROOM_WORKBENCH,
            Description = Descriptions.MAINTENANCE_ROOM_WORKBENCH,
            IsPickAble = false
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
            IsPickAble = false,
            IsCloseAble = true,
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
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        AddTakeEvents(result, eventProvider);
        AddUseEvents(result, eventProvider);

        return result;
    }
    
    private static void AddTakeEvents(Item item, EventProvider eventProvider)
    {
        item.AfterTake += eventProvider.TakeTool;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.TakeTool), 1);
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseToolWithAntennaInSocialRoom;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.UseToolWithAntennaInSocialRoom), 5);
        
        item.Use += eventProvider.MountAntennaToDroid;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.MountAntennaToDroid), 10);
    }
}