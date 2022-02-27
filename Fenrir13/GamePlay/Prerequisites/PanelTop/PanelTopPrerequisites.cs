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
            IsPickAble = false
        };

        AddPullEvents(lever, eventProvider);
        AddUseEvents(lever, eventProvider);
        
        return lever;
    }
    
    private static void AddPullEvents(AContainerObject item, EventProvider eventProvider)
    {
        item.Pull += eventProvider.PullLever;
    }
    
    private static void AddUseEvents(AContainerObject item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseDumbbellBarWithLever;
    }
}