using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.MachineCorridor;

internal static class MachineCorridorMidPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.MACHINE_CORRIDOR_MID,
            Name = Locations.MACHINE_CORRIDOR_MID,
            Description = Descriptions.MACHINE_CORRIDOR_MID,
            IsLocked = true,
            LockDescription = Descriptions.MACHINE_CORRIDOR_MID_LOCKDESCRIPTION,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddSurroundings(corridor);
        
        return corridor;
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
            Grammar = new Grammars()
        };
        location.Items.Add(ceiling);
    }
}