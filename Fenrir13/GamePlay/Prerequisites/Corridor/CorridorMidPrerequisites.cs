using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MID,
            Name = Locations.CORRIDOR_MID,
            Description = Descriptions.CORRIDOR_MID,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        
        AddSurroundings(corridor);
        corridor.Items.Add(GetPortrait(eventProvider));

        return corridor;
    }
    
    private static Item GetPortrait(EventProvider eventProvider)
    {
        var portrait = new Item()
        {
            Key = Keys.PORTRAIT,
            Name = Items.PORTRAIT,
            Description = Descriptions.PORTRAIT,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };

        return portrait;
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
        
        var door = new Item()
        {
            Key = Keys.ROOM_DOOR,
            Name = Items.ROOM_DOOR,
            Description = Descriptions.ROOM_DOOR,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(door);
        
        var stairs = new Item()
        {
            Key = Keys.STAIRS,
            Name = Items.STAIRS,
            Description = Descriptions.STAIRS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(stairs);
        
        var roundStairs = new Item()
        {
            Key = Keys.ROUND_STAIRS,
            Name = Items.ROUND_STAIRS,
            Description = Descriptions.ROUND_STAIRS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        corridor.Items.Add(roundStairs);
    }
}