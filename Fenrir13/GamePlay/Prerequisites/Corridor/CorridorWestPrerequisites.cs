using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_WEST,
            Name = Locations.CORRIDOR_WEST,
            Description = Descriptions.CORRIDOR_WEST,
            IsLocked = true,
            LockDescription = Descriptions.CORRIDOR_WEST_LOCKDESCRIPTION,
            FirstLookDescription = Descriptions.CORRIDOR_WEST_FIRSTLOOK,
            Grammar = new IndividualObjectGrammar(Genders.Male)
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
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(ceiling);
        
        var wall = new Item()
        {
            Key = Keys.CORRIDOR_WALLS,
            Name = Items.CORRIDOR_WALLS,
            Description = Descriptions.CORRIDOR_WALLS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(wall);
        
        var painting = new Item()
        {
            Key = Keys.CORRIDOR_PAINTING_WEST,
            Name = Items.CORRIDOR_PAINTING_WEST,
            Description = Descriptions.CORRIDOR_PAINTING_WEST,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        corridor.Items.Add(painting);
        
        var hole = new Item()
        {
            Key = Keys.CORRIDOR_WEST_HOLE,
            Name = Items.CORRIDOR_WEST_HOLE,
            Description = Descriptions.CORRIDOR_WEST_HOLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        corridor.Items.Add(hole);
        
        var seam = new Item()
        {
            Key = Keys.CORRIDOR_WEST_WELD_SEAM,
            Name = Items.CORRIDOR_WEST_WELD_SEAM,
            Description = Descriptions.CORRIDOR_WEST_WELD_SEAM,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(seam);
        
        var bullEye = new Item()
        {
            Key = Keys.CORRIDOR_WEST_BULLEYE,
            Name = Items.CORRIDOR_WEST_BULLEYE,
            Description = Descriptions.CORRIDOR_WEST_BULLEYE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(gender: Genders.Neutrum)
        };
        corridor.Items.Add(bullEye);
    }
}