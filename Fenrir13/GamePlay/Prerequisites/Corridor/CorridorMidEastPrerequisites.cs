using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidEastPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDEAST,
            Name = Locations.CORRIDOR_MIDEAST,
            Description = Descriptions.CORRIDOR_MIDEAST,
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
            Key = Keys.CORRIDOR_PAINTING,
            Name = Items.CORRIDOR_PAINTING,
            Description = Descriptions.CORRIDOR_PAINTING_MIDEAST,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        corridor.Items.Add(painting);
    }
}