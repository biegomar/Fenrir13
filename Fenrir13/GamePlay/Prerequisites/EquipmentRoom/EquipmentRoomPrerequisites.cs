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

        AddSurroundings(equipmentRoom);
        
        return equipmentRoom;
    }
    
    private static void AddSurroundings(Location engine)
    {
        
    } 
}