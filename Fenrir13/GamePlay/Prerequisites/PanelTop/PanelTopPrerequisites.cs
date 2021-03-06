using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.PanelTop;

internal static class PanelTopPrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var panelTop = new Location()
        {
            Key = Keys.PANEL_TOP,
            Name = Locations.PANEL_TOP,
            Description = Descriptions.PANEL_TOP
        };

        AddSurroundings(panelTop);
        
        panelTop.Items.Add(GetLever(eventProvider));
        return panelTop;
    }

    private static Item GetLever(EventProvider eventProvider)
    {
        var lever = new Item
        {
            Key = Keys.PANEL_TOP_LEVER,
            Name = Items.PANEL_TOP_LEVER,
            Description = Descriptions.PANEL_TOP_LEVER,
            ContainmentDescription = Descriptions.PANEL_TOP_LEVER_CONTAINMENT,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };

        AddPullEvents(lever, eventProvider);
        AddUseEvents(lever, eventProvider);
        
        return lever;
    }
    
    private static void AddSurroundings(Location location)
    {
        var lines = new Item()
        {
            Key = Keys.PANEL_TOP_SUPPLYLINES,
            Name = Items.PANEL_TOP_SUPPLYLINES,
            Description = Descriptions.PANEL_TOP_SUPPLYLINES,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(lines);
        
        var wolf = new Item()
        {
            Key = Keys.PANEL_TOP_WOLF,
            Name = Items.PANEL_TOP_WOLF,
            Description = Descriptions.PANEL_TOP_WOLF,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(Genders.Male)
        };
        location.Items.Add(wolf);
        
        var lamps = new Item()
        {
            Key = Keys.PANEL_TOP_LAMPS,
            Name = Items.PANEL_TOP_LAMPS,
            Description = Descriptions.PANEL_TOP_LAMPS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(lamps);
        
        var windows = new Item()
        {
            Key = Keys.PANEL_TOP_WINDOWS,
            Name = Items.PANEL_TOP_WINDOWS,
            Description = Descriptions.PANEL_TOP_WINDOWS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(windows);
        
        var panels = new Item()
        {
            Key = Keys.PANEL_TOP_PANELS,
            Name = Items.PANEL_TOP_PANELS,
            Description = Descriptions.PANEL_TOP_PANELS,
            IsSurrounding = true,
            IsPickAble = false,
            Grammar = new Grammars(isSingular: false)
        };
        location.Items.Add(panels);
        
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
            Grammar = new Grammars(Genders.Neutrum)
        };
        location.Items.Add(universe);
    }
    
    private static void AddPullEvents(Item item, EventProvider eventProvider)
    {
        item.Pull += eventProvider.PullLever;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseDumbbellBarWithLever;
    }
}