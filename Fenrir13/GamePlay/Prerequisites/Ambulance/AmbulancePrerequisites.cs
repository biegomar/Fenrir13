using System.Runtime.CompilerServices;
using Fenrir13.Events;
using Fenrir13.Resources;
using Heretic.InteractiveFiction.Objects;

namespace Fenrir13.GamePlay.Prerequisites.Ambulance;

internal static class AmbulancePrerequisites
{
    internal static Location Get(EventProvider eventProvider)
    {
        var ambulance = new Location()
        {
            Key = Keys.AMBULANCE,
            Name = Locations.AMBULANCE,
            Description = Descriptions.AMBULANCE
        };
        
        ambulance.Items.Add(GetRespirator(eventProvider));
        
        AddSurroundings(ambulance);
        
        AddAfterLookEvents(ambulance, eventProvider);
        
        return ambulance;
    }
    
    private static Item GetRespirator(EventProvider eventProvider)
    {
        var respirator = new Item
        {
            Key = Keys.AMBULANCE_RESPIRATOR,
            Name = Items.AMBULANCE_RESPIRATOR,
            Description = Descriptions.AMBULANCE_RESPIRATOR,
            IsPickAble = false,
            IsHidden = true,
            IsUnveilAble = false
        };
        
        respirator.Items.Add(GetFlap(eventProvider));
        respirator.Items.Add(GetOxygenBottle(eventProvider));
        
        return respirator;
    }

    private static Item GetOxygenBottle(EventProvider eventProvider)
    {
        var oxygenBottle = new Item
        {
            Key = Keys.OXYGEN_BOTTLE,
            Name = Items.OXYGEN_BOTTLE,
            Description = Descriptions.OXYGEN_BOTTLE,
            Weight = ItemWeights.OXYGEN_BOTTLE,
            IsHidden = true,
            IsUnveilAble = false
        };
        
        AddUseEvents(oxygenBottle, eventProvider);

        return oxygenBottle;
    }
    
    private static Item GetFlap(EventProvider eventProvider)
    {
        var flap = new Item
        {
            Key = Keys.AMBULANCE_RESPIRATOR_FLAP,
            Name = Items.AMBULANCE_RESPIRATOR_FLAP,
            Description = Descriptions.AMBULANCE_RESPIRATOR_FLAP,
            FirstLookDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_FIRSTLOOK,
            ContainmentDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_CONTAINMENT,
            IsPickAble = false,
            IsLockAble = true,
            IsLocked = true,
            IsCloseAble = true,
            IsClosed = true,
            IsBreakable = true,
            LockDescription = Descriptions.AMBULANCE_RESPIRATOR_FLAP_LOCK
        };
        
        AddBreakEvents(flap, eventProvider);
        AddOpenCloseEvents(flap, eventProvider);
        
        return flap;
    }
    
    private static void AddSurroundings(Location location)
    {
        location.Surroundings.Add(Keys.AMBULANCE_OP_TABLE, Descriptions.AMBULANCE_OP_TABLE);
        location.Surroundings.Add(Keys.AMBULANCE_OP_ROBOTER, Descriptions.AMBULANCE_OP_ROBOTER);
        location.Surroundings.Add(Keys.CEILING, Descriptions.CEILING);
        location.Surroundings.Add(Keys.CHAMBER_WALL, Descriptions.CHAMBER_WALL);
        location.Surroundings.Add(Keys.AMBULANCE_BED, Descriptions.AMBULANCE_BED);
        location.Surroundings.Add(Keys.AMBULANCE_OP_ITEMS, Descriptions.AMBULANCE_OP_ITEMS);
        location.Surroundings.Add(Keys.AMBULANCE_OP_ITEMS_DETAIL, Descriptions.AMBULANCE_OP_ITEMS_DETAIL);
        location.Surroundings.Add(Keys.AMBULANCE_MEDIS, Descriptions.AMBULANCE_MEDIS);
        location.Surroundings.Add(Keys.AMBULANCE_RESPIRATOR, Descriptions.AMBULANCE_RESPIRATOR);
    }
    
    private static void AddAfterLookEvents(Location ambulance, EventProvider eventProvider)
    {
        ambulance.AfterLook += eventProvider.LookAtRespirator;
    }
    
    private static void AddUseEvents(Item item, EventProvider eventProvider)
    {
        item.Use += eventProvider.UseOxygenBottleWithHelmet;
    }
    
    private static void AddBreakEvents(Item item, EventProvider eventProvider)
    {
        item.Break += eventProvider.BreakFlapWithDumbbellBar;
    }
    
    private static void AddOpenCloseEvents(Item item, EventProvider eventProvider)
    {
        item.BeforeOpen += eventProvider.OpenFlap;
        item.BeforeClose += eventProvider.CloseFlap;
    }
}