using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.EquipmentRoom;

internal static class EquipmentRoomPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var equipmentRoom = new Location()
        {
            Key = Keys.EQUIPMENT_ROOM,
            Name = Locations.EQUIPMENT_ROOM,
            Description = Descriptions.EQUIPMENT_ROOM
        };
        
        equipmentRoom.Items.Add(GetBox(eventProvider));

        AddSurroundings(equipmentRoom);
        
        AddBreakEvents(equipmentRoom, eventProvider);
        
        return equipmentRoom;
    }
    
    private static void AddSurroundings(Location equipmentRoom)
    {
        equipmentRoom.Surroundings.Add(Keys.EQUIPMENT_BOX_LOCK, Descriptions.EQUIPMENT_BOX_LOCK);
    }

    private static Item GetBox(EventProvider eventProvider)
    {
        var box = new Item()
        {
            Key = Keys.EQUIPMENT_BOX,
            Name = Items.EQUIPMENT_BOX,
            Description = Descriptions.EQUIPMENT_BOX,
            ContainmentDescription = Descriptions.EQUIPMENT_BOX_CONTAINMENT,
            LockDescription = Descriptions.EQUIPMENT_BOX_LOCKDESCRIPTION,
            IsClosed = true,
            IsLocked = true,
            IsCloseAble = true
        };
        
        box.Items.Add(GetHelmet(eventProvider));
        box.Items.Add(GetGloves(eventProvider));
        box.Items.Add(GetBoots(eventProvider));
        
        AddOpenEvents(box, eventProvider);

        return box;
    }
    
    private static Item GetHelmet(EventProvider eventProvider)
    {
        var helmet = new Item()
        {
            Key = Keys.HELMET,
            Name = Items.HELMET,
            Description = Descriptions.HELMET,
            FirstLookDescription = Descriptions.HELMET_FIRSTLOOK
        };

        return helmet;
    }
    
    private static Item GetGloves(EventProvider eventProvider)
    {
        var gloves = new Item()
        {
            Key = Keys.GLOVES,
            Name = Items.GLOVES,
            Description = Descriptions.GLOVES
        };

        return gloves;
    }
    
    private static Item GetBoots(EventProvider eventProvider)
    {
        var boots = new Item()
        {
            Key = Keys.BOOTS,
            Name = Items.BOOTS,
            Description = Descriptions.BOOTS
        };

        return boots;
    }
    
    private static void AddBreakEvents(Location equipmentRoom, EventProvider eventProvider)
    {
        equipmentRoom.Break += eventProvider.BreakEquipmentBoxLock;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.BreakEquipmentBoxLock), 5);
    }
    
    private static void AddOpenEvents(Item box, EventProvider eventProvider)
    {
        box.AfterOpen += eventProvider.OpenEquipmentBox;
    }
}