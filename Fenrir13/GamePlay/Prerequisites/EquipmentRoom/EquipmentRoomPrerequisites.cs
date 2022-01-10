using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.EquipmentRoom;

internal class EquipmentRoomPrerequisites
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
    
    private static void AddSurroundings(Location engine)
    {
        
    }

    private static Item GetBox(EventProvider eventProvider)
    {
        var box = new Item()
        {
            Key = Keys.EQUIPMENT_BOX,
            Name = Items.EQUIPMENT_BOX,
            Description = Descriptions.EQUIPMENT_BOX,
            ContainmentDescription = Descriptions.EQUIPMENT_BOX_CONTAINMENT
        };

        return box;
    }
}