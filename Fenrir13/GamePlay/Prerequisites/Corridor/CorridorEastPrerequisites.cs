using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_EAST,
            Name = Locations.CORRIDOR_EAST,
            Description = Descriptions.CORRIDOR_EAST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_EAST_LOCKDESCRIPTION,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddSurroundings(corridor);

        return corridor;
    }

    private static void AddSurroundings(Location corridor)
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
        corridor.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CORRIDOR_WALLS,
            Name = Items.CORRIDOR_WALLS,
            Description = Descriptions.CORRIDOR_WALLS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        corridor.Items.Add(wall);
        
        var painting = new Item()
        {
            Key = Keys.CORRIDOR_PAINTING,
            Name = Items.CORRIDOR_PAINTING,
            Description = Descriptions.CORRIDOR_PAINTING_EAST,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(painting);
        
        var chamberOne = new Item()
        {
            Key = Keys.EMPTYCREWCHAMBERONE,
            Name = Items.EMPTYCREWCHAMBERONE,
            Description = Descriptions.EMPTYCREWCHAMBERONE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(chamberOne);
        
        var chamberTwo = new Item()
        {
            Key = Keys.EMPTYCREWCHAMBERTWO,
            Name = Items.EMPTYCREWCHAMBERTWO,
            Description = Descriptions.EMPTYCREWCHAMBERTWO,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(chamberTwo);
        
        var redDot = new Item()
        {
            Key = Keys.RED_DOT,
            Name = Items.RED_DOT,
            Description = Descriptions.RED_DOT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Male)
        };
        corridor.Items.Add(redDot);
        
        var location = new Item()
        {
            Key = Keys.LOCATION,
            Name = Items.LOCATION,
            Description = Descriptions.LOCATION,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Male)
        };
        corridor.Items.Add(location);
        
        var greenArrows = new Item()
        {
            Key = Keys.GREEN_ARROWS,
            Name = Items.GREEN_ARROWS,
            Description = Descriptions.GREEN_ARROWS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Male, isSingular: false)
        };
        corridor.Items.Add(greenArrows);
        
        var emergencyLight = new Item()
        {
            Key = Keys.EMERGENCY_LIGHT,
            Name = Items.EMERGENCY_LIGHT,
            Description = Descriptions.EMERGENCY_LIGHT,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        corridor.Items.Add(emergencyLight);
    }
}