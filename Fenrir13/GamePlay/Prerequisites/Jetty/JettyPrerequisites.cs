using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Jetty;

internal static class JettyPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var jetty = new Location()
        {
            Key = Keys.JETTY,
            Name = Locations.JETTY,
            Description = Descriptions.JETTY,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };

        AddSurroundings(jetty);
        
        return jetty;
    }
    
    private static void AddSurroundings(Location location)
    {
        var spaceLadder = new Item()
        {
            Key = Keys.SPACE_LADDER,
            Name = Items.SPACE_LADDER,
            Description = Descriptions.SPACE_LADDER,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(spaceLadder);
        
        var hull = new Item()
        {
            Key = Keys.JETTY_HULL,
            Name = Items.JETTY_HULL,
            Description = Descriptions.JETTY_HULL,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(hull);
        
        var space = new Item()
        {
            Key = Keys.JETTY_SPACE,
            Name = Items.JETTY_SPACE,
            Description = Descriptions.JETTY_SPACE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };
        location.Items.Add(space);
        
        var stars = new Item()
        {
            Key = Keys.STARS,
            Name = Items.STARS,
            Description = Descriptions.STARS,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Male, isSingular: false)
        };
        location.Items.Add(stars);
        
        var alphaCentauri = new Item()
        {
            Key = Keys.JETTY_ALPHA,
            Name = Items.JETTY_ALPHA,
            Description = Descriptions.JETTY_ALPHA,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(alphaCentauri);
        
        var universe = new Item()
        {
            Key = Keys.UNIVERSE,
            Name = Items.UNIVERSE,
            Description = Descriptions.UNIVERSE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(universe);
        
        var engine = new Item()
        {
            Key = Keys.JETTY_ENGINE,
            Name = Items.JETTY_ENGINE,
            Description = Descriptions.JETTY_ENGINE,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(engine);
    }
}