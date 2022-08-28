using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Corridor;

internal static class CorridorMidWestPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var corridor = new Location()
        {
            Key = Keys.CORRIDOR_MIDWEST,
            Name = Locations.CORRIDOR_MIDWEST,
            Description = Descriptions.CORRIDOR_MIDWEST,
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
            Description = Descriptions.CORRIDOR_PAINTING_MIDWEST,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(painting);
        
        var bulkhead = new Item()
        {
            Key = Keys.BULKHEAD,
            Name = Items.BULKHEAD,
            Description = Descriptions.BULKHEAD,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(bulkhead);
        
        var bulkheadWindow = new Item()
        {
            Key = Keys.BULKHEAD_WINDOW,
            Name = Items.BULKHEAD_WINDOW,
            Description = Descriptions.BULKHEAD_WINDOW,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(bulkheadWindow);
        
        var hole = new Item()
        {
            Key = Keys.HOLE,
            Name = Items.HOLE,
            Description = Descriptions.HOLE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        corridor.Items.Add(hole);
        
        var sideWall = new Item()
        {
            Key = Keys.SIDEWALL,
            Name = Items.SIDEWALL,
            Description = Descriptions.SIDEWALL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars()
        };
        corridor.Items.Add(sideWall);
        
        var crewChambers = new Item()
        {
            Key = Keys.CREWCHAMBERS,
            Name = Items.CREWCHAMBERS,
            Description = Descriptions.CREWCHAMBERS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(isSingular: false)
        };
        corridor.Items.Add(crewChambers);
        
        var crewDoors = new Item()
        {
            Key = Keys.CREW_DOORS,
            Name = Items.CREW_DOORS,
            Description = Descriptions.CREW_DOORS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new Grammars(isSingular: false)
        };
        corridor.Items.Add(crewDoors);
    }
}