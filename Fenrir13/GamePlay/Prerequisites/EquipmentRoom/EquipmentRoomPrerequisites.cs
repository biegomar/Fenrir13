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

        return equipmentRoom;
    }
    
    private static void AddSurroundings(Location equipmentRoom)
    {
        equipmentRoom.Surroundings.Add(Keys.CHAMBER_WALL, Descriptions.CHAMBER_WALL);
        equipmentRoom.Surroundings.Add(Keys.EQUIPMENT_ROOM_BENCH, Descriptions.EQUIPMENT_ROOM_BENCH);
        equipmentRoom.Surroundings.Add(Keys.EQUIPMENT_ROOM_NICHE, Descriptions.EQUIPMENT_ROOM_NICHE);
        equipmentRoom.Surroundings.Add(Keys.EQUIPMENT_ROOM_TRASHBIN, Descriptions.EQUIPMENT_ROOM_TRASHBIN);
        equipmentRoom.Surroundings.Add(Keys.EQUIPMENT_ROOM_CLOTH, Descriptions.EQUIPMENT_ROOM_CLOTH);
        equipmentRoom.Surroundings.Add(Keys.EYELET, Descriptions.EYELET);
        equipmentRoom.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
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
            CloseDescription = Descriptions.EQUIPMENT_BOX_CLOSED,
            IsClosed = true,
            IsLocked = true,
            IsCloseAble = true,
            IsPickAble = false
        };
        
        box.LinkedTo.Add(GetBoxLock(eventProvider));
        box.Items.Add(GetHelmet(eventProvider));
        box.Items.Add(GetGloves(eventProvider));
        box.Items.Add(GetBoots(eventProvider));
        box.Items.Add(GetBelt(eventProvider));
        
        AddOpenEvents(box, eventProvider);

        return box;
    }
    
    private static Item GetBoxLock(EventProvider eventProvider)
    {
        var boxLock = new Item()
        {
            Key = Keys.EQUIPMENT_BOX_LOCK,
            Name = Items.EQUIPMENT_BOX_LOCK,
            Description = Descriptions.EQUIPMENT_BOX_LOCK,
            ContainmentDescription = Descriptions.EQUIPMENT_BOX_LOCK_CONTAINMENT,
            IsPickAble = false,
            IsHidden = true,
            IsBreakable = true
        };
        
        AddBreakEvents(boxLock, eventProvider);

        return boxLock;
    }
    
    private static Item GetHelmet(EventProvider eventProvider)
    {
        var helmet = new Item()
        {
            Key = Keys.HELMET,
            Name = Items.HELMET,
            Description = Descriptions.HELMET,
            FirstLookDescription = Descriptions.HELMET_FIRSTLOOK,
            Weight = ItemWeights.HELMET
        };

        return helmet;
    }
    
    private static Item GetGloves(EventProvider eventProvider)
    {
        var gloves = new Item()
        {
            Key = Keys.GLOVES,
            Name = Items.GLOVES,
            Description = Descriptions.GLOVES,
            Weight = ItemWeights.GLOVES
        };

        return gloves;
    }
    
    private static Item GetBoots(EventProvider eventProvider)
    {
        var boots = new Item()
        {
            Key = Keys.BOOTS,
            Name = Items.BOOTS,
            Description = Descriptions.BOOTS,
            Weight = ItemWeights.BOOTS
        };

        return boots;
    }
    
    private static Item GetBelt(EventProvider eventProvider)
    {
        var belt = new Item()
        {
            Key = Keys.BELT,
            Name = Items.BELT,
            Description = Descriptions.BELT,
            Weight = ItemWeights.BELT
        };
        
        belt.Items.Add(GetEyelet(eventProvider));

        return belt;
    }
    
    private static Item GetEyelet(EventProvider eventProvider)
    {
        var eyelet = new Item()
        {
            Key = Keys.EYELET,
            Name = Items.EYELET,
            Description = Descriptions.EYELET,
            ContainmentDescription = Descriptions.EYELET_CONTAINMENT,
            IsPickAble = false
        };

        return eyelet;
    }
    

    private static void AddBreakEvents(Item box, EventProvider eventProvider)
    {
        box.Break += eventProvider.BreakEquipmentBoxLock;
        eventProvider.ScoreBoard.Add(nameof(eventProvider.BreakEquipmentBoxLock), 5);
    }
    
    private static void AddOpenEvents(Item box, EventProvider eventProvider)
    {
        box.AfterOpen += eventProvider.OpenEquipmentBox;
    }
}