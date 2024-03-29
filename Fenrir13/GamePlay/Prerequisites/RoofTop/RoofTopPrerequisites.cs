using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.GamePlay;
using Heretic.InteractiveFiction.Grammars;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.RoofTop;

internal static class RoofTopPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var room = new Location()
        {
            Key = Keys.ROOF_TOP,
            Name = Locations.ROOF_TOP,
            Description = Descriptions.ROOF_TOP,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        
        AddSurroundings(room);
        
        room.Items.Add(GetDroid(eventProvider));

        AddNewVerbs(room);
        
        return room;
    }
    
    private static void AddNewVerbs(Location socialRoom)
    {
        socialRoom.AddOptionalVerb(VerbKey.CONNECT, OptionalVerbs.SCREW_ON, Descriptions.ITEM_NOT_SCREW_ON_ABLE);
    }

    private static Item GetDroid(EventProvider eventProvider)
    {
        var droid = new Item
        {
            Key = Keys.DROID,
            Name = Items.DROID,
            Description = Descriptions.DROID,
            FirstLookDescription = Descriptions.DROID_FIRSTLOOK,
            IsPickable = false,
            IsLinkable = true,
            Grammar = new IndividualObjectGrammar(Genders.Male)
        };

        return droid;
    }
    
    private static void AddSurroundings(Location location)
    {
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
        
        var ladder = new Item()
        {
            Key = Keys.SPACE_LADDER,
            Name = Items.SPACE_LADDER,
            Description = Descriptions.SPACE_LADDER,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar()
        };
        location.Items.Add(ladder);
        
        var proxima = new Item()
        {
            Key = Keys.PROXIMA_CENTAURI,
            Name = Items.PROXIMA_CENTAURI,
            Description = Descriptions.PROXIMA_CENTAURI,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(proxima);
        
        var claw = new Item()
        {
            Key = Keys.ROOF_TOP_CLAW,
            Name = Items.ROOF_TOP_CLAW,
            Description = Descriptions.ROOF_TOP_CLAW,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(isSingular: false)
        };
        location.Items.Add(claw);
        
        var dock = new Item()
        {
            Key = Keys.ROOF_TOP_DOCK,
            Name = Items.ROOF_TOP_DOCK,
            Description = Descriptions.ROOF_TOP_DOCK,
            IsSurrounding = true,
            IsPickable = false,
            Grammar = new IndividualObjectGrammar(Genders.Neutrum)
        };
        location.Items.Add(dock);
    }
}