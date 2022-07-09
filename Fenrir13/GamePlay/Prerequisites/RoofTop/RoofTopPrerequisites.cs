using Fenrir13.Events;
using Fenrir13.Resources;
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
            Grammar = new Grammars(Genders.Neutrum)
        };
        
        AddSurroundings(room);
        
        room.Items.Add(GetDroid(eventProvider));

        return room;
    }

    private static Item GetDroid(EventProvider eventProvider)
    {
        var droid = new Item
        {
            Key = Keys.DROID,
            Name = Items.DROID,
            Description = Descriptions.DROID,
            FirstLookDescription = Descriptions.DROID_FIRSTLOOK,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        
        AddUseEvents(droid, eventProvider);

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
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(space);
        
        var stars = new Item()
        {
            Key = Keys.STARS,
            Name = Items.STARS,
            Description = Descriptions.STARS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male, isSingular: false)
        };
        location.Items.Add(stars);
        
        var alphaCentauri = new Item()
        {
            Key = Keys.JETTY_ALPHA,
            Name = Items.JETTY_ALPHA,
            Description = Descriptions.JETTY_ALPHA,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(alphaCentauri);
        
        var universe = new Item()
        {
            Key = Keys.UNIVERSE,
            Name = Items.UNIVERSE,
            Description = Descriptions.UNIVERSE,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(universe);
        
        var ladder = new Item()
        {
            Key = Keys.SPACE_LADDER,
            Name = Items.SPACE_LADDER,
            Description = Descriptions.SPACE_LADDER,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars()
        };
        location.Items.Add(ladder);
        
        var proxima = new Item()
        {
            Key = Keys.PROXIMA_CENTAURI,
            Name = Items.PROXIMA_CENTAURI,
            Description = Descriptions.PROXIMA_CENTAURI,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum, isSingular: false)
        };
        location.Items.Add(proxima);
        
        var claw = new Item()
        {
            Key = Keys.ROOF_TOP_CLAW,
            Name = Items.ROOF_TOP_CLAW,
            Description = Descriptions.ROOF_TOP_CLAW,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(claw);
        
        var dock = new Item()
        {
            Key = Keys.ROOF_TOP_DOCK,
            Name = Items.ROOF_TOP_DOCK,
            Description = Descriptions.ROOF_TOP_DOCK,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(dock);
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.MountAntennaToDroid;
    }
}